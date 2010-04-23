using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Xml;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vailos
{
    public partial class VailosMainWin : Form
    {
        #region Global Variables
        // ##### Global variables #####
        private string currentFileName = null;      // should always contain file name to save when file is selected
        private int sortColumn = -1;                // so column sorting is acending or decending on each click

        #region Settings Variables
        // Settings variables
        private string fileDirPath;                 // directory with notes
        private string extEditor;
        private bool autoSaveOn;                    // will we use autoSave
        private int autoSaveTime;                   // duration in seconds for autoSave timer
        private bool autoSaveBackup;                // make a .bak when autoSaving
        private bool tabInEditBox;                  // if false tab in edit box will always jump to search
        private int panel1Height;                   // determines where splitter is
        private bool useCreatedDateColumn;
        private bool useModifiedDateColumn;

        #endregion

        #endregion

        #region Vailos Constructor
        /// <summary>
        /// Constructor. Calls settings, loads list view, gives timer interval, and hands
        /// upDownTextBox and editTextBox itself.
        /// </summary>
        public VailosMainWin()
        {
            // Needed by Visual Studio designer
            InitializeComponent();
            // get and implement the settings - should happen this early
            grabSettings();
            implementSettings();
            // settings are in place, let list view load up the files
            vailosListView1.loadFileList(fileDirPath);
            // give components a handle to this
            upDownTextBox1.setCaller(this);
            editTextBox1.setCaller(this);
            vailosListView1.setCaller(this);
        }
        #endregion

        #region General
        // ############################# General ##############################
        private void VailosMainWin_Load(object sender, EventArgs e)
        {
            // used for shortcut keys implemented in VailosMainWin_ShortCutKeys
            this.KeyPreview = true;
            // set to false by default incase nothing is selected
            // search string text is filled after this if present, in which case
            // box will become enabled
            editTextBox1.Enabled = false;
        }
        
        private void grabSettings()
        {
            fileDirPath = Properties.Settings.Default.dirPath;
            extEditor = Properties.Settings.Default.externalEditorPath;
            autoSaveOn = Properties.Settings.Default.autoSaveOn;
            autoSaveTime = Properties.Settings.Default.autoSaveTime;
            autoSaveBackup = Properties.Settings.Default.autoSaveBackup;
            tabInEditBox = Properties.Settings.Default.tabInEditBox;
            panel1Height = Properties.Settings.Default.splitterHeight;
            useCreatedDateColumn = Properties.Settings.Default.useCreationDateColumn;
            useModifiedDateColumn = Properties.Settings.Default.useModifiedDateColumn;
        }
        
        private void implementSettings()
        {
            // autoSave settings - autoSaveBackup is already handled
            autoSaveTimer.Enabled = autoSaveOn;
            autoSaveTimer.Interval = autoSaveTime;
            autoSaveTime = autoSaveTime * 1000;
            if (fileDirPath == "")
            {
                bool result = getNotesDirectoryFromUser();
                if (result == false)
                {
                    Close();
                }
            }
            this.Width = Properties.Settings.Default.VailosMainWinWidth;
            this.Height = Properties.Settings.Default.VailosMainWinHeight;
            panel1.Height = panel1Height;
            if (Properties.Settings.Default.searchBoxString != "")
            {
                upDownTextBox1.Text = Properties.Settings.Default.searchBoxString;
            }
            editTextBox1.setAcceptsTab(tabInEditBox);
            userSelectedToolStripMenuItem.Text = Path.GetFileName(extEditor);
            dateCreatedToolStripMenuItem.Checked = useCreatedDateColumn;
            dateModifiedToolStripMenuItem.Checked = useModifiedDateColumn;
        }
        
        private void saveOutSettings()
        {
            if (this.Height > 100 && this.Width > 100)
            {
                Properties.Settings.Default.VailosMainWinHeight = this.Height;
                Properties.Settings.Default.VailosMainWinWidth = this.Width;
                Properties.Settings.Default.splitterHeight = panel1.Height;
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.searchBoxString = upDownTextBox1.Text;
            Properties.Settings.Default.Save();
        }

        private bool getNotesDirectoryFromUser()
        {
            MessageBox.Show("Folder for notes not found, please select" +
                " a directory for your notes");
            DialogResult dirChoose = folderBrowserDialog1.ShowDialog();
            if (dirChoose == DialogResult.OK)
            {
                fileDirPath = folderBrowserDialog1.SelectedPath;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (autoSaveBackup && vailosListView1.SelectedItems.Count != 0 && currentFileName != null)
            {
                try
                {
                    File.Copy(currentFileName, currentFileName + ".bak", true);
                }
                catch { /* ignore error*/ }
            }
            saveFile();
        }
        
        private void clearButton_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        
        private void clearAll()
        {
            saveFile();
            upDownTextBox1.Clear();
            vailosListView1.SelectedItems.Clear();
            editTextBox1.Clear();
            upDownTextBox1.Focus();
            currentFileName = null;
        }
        
        private void VailosMainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveOutSettings();
            saveFile();
        }
        
        private void VailosMainWin_KeyDown(object sender, KeyEventArgs e)
        {
            VailosMainWin_ShortCutKeys(sender, e);
        }
        
        private void VailosMainWin_Resize(object sender, EventArgs e)
        {
            //int columnCount = vailosListView1.Columns.Count - 1;
            //vailosListView1.Columns[0].Width = vailosListView1.Width 
            //    - vailosListView1.Columns[columnCount].Width;
        }
        
        private void VailosMainWin_ShortCutKeys(object sender, KeyEventArgs e)
        {
            {
                // Ctrl-J - previous item
                if (e.Control == true && e.KeyCode == Keys.J)
                {
                    searchBoxDownPressed();
                    e.Handled = true;
                }
                // Ctrl-K - next item
                else if (e.Control == true && e.KeyCode == Keys.K)
                {
                    searchBoxUpPressed();
                    e.Handled = true;
                }
                // Escape - clear all
                else if (e.KeyCode == Keys.Escape)
                {
                    clearAll();
                }
            }

        }

        #endregion

        #region Menu Functions
        // ########################### Menu Functions ##############################
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VailosAbout newAbout = new VailosAbout();
            newAbout.ShowDialog();
        }
        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelectedItem();
        }
        
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renameSelectedItem();
        }
        
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            giveSearchBoxFocus();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editTextBox1.SelectAll();
        }

        private void nextNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchBoxDownPressed();
        }

        private void previousNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchBoxUpPressed();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveOutSettings();
            Preferences p = new Preferences();
            p.ShowDialog();
            grabSettings();
            implementSettings();
            vailosListView1.Items.Clear();
            vailosListView1.loadFileList(fileDirPath);
        }

        #endregion

        #region upDownTextBox functions
        // ###################### upDownTextBox (search box) functions #######################
        
        private void upDownTextBox1_Enter(object sender, EventArgs e)
        {
            upDownTextBox1.SelectAll();
        }
        
        /// <title>giveSearchBoxFocus</title>
        /// <summary>
        /// Used to hop focus up to search box, called by both the editTextBox when you press tab
        /// in the first spot, and by the "Search or Create" menu command.
        /// </summary>
        
        public void giveSearchBoxFocus()
        {
            upDownTextBox1.Focus();
        }
        
        private void upDownTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (currentFileName != null)
            {
                File.WriteAllText(currentFileName, editTextBox1.Text);
            }
            vailosListView1.Items.Clear();
            currentFileName = null;
            editTextBox1.Clear();
            vailosListView1.BeginUpdate();
            vailosListView1.loadFileList(fileDirPath);
            for (int i = vailosListView1.Items.Count - 1; i >= 0; i--)
            {
                ListViewItem currentItem = vailosListView1.Items[i];
                string lowerCasedSearchString = currentItem.SubItems[3].Text.ToLower();
                if (!lowerCasedSearchString.Contains(upDownTextBox1.Text.ToLower()))
                {
                    currentItem.Remove();
                }
            }
            if (vailosListView1.Items.Count > 0)
            {
                vailosListView1.Items[0].Selected = true;
            }
            vailosListView1.EndUpdate();
        }
        
        private void upDownTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int selCount = vailosListView1.SelectedItems.Count;

                if (selCount == 0 ||
                    vailosListView1.SelectedItems[0].Text != upDownTextBox1.Text + ".txt")
                {
                    string newFileName = upDownTextBox1.Text + ".txt";
                    newFileName = fileDirPath + newFileName;
                    File.WriteAllText(newFileName, "");
                    ListViewItem newFileItem = vailosListView1.addNewItem(fileDirPath, Path.GetFileName(newFileName));
                    newFileItem.Selected = true;
                    currentFileName = newFileName;
                    saveFile();
                    editTextBox1.Focus();
                }
                else if (selCount == 1)
                {
                    editTextBox1.Focus();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                clearAll();
            }
        }
        
        /// <summary>
        /// If there is an item in the list view above the currently selected item, move up
        /// </summary>
        public void searchBoxUpPressed()
        {
            if (vailosListView1 != null && vailosListView1.SelectedItems != null && vailosListView1.SelectedItems.Count != 0)
            {
                int currentSelectedIndex = vailosListView1.SelectedIndices[0];
                if (currentSelectedIndex > 0)
                {
                    vailosListView1.Items[currentSelectedIndex - 1].Selected = true;
                }
            }
        }
        
        /// <summary>
        /// If there is an item in the list view below the current item, move down
        /// </summary>
        public void searchBoxDownPressed()
        {
            // protect this stuff
            if (vailosListView1 != null && vailosListView1.Items.Count > 0)
            {
                // something is selected, we want to move down if it isn't the last item
                if (vailosListView1.SelectedItems != null && vailosListView1.SelectedItems.Count > 0)
                {
                    int currentSelectedIndex = vailosListView1.SelectedIndices[0];
                    if (currentSelectedIndex < vailosListView1.Items.Count - 1)
                    {
                        vailosListView1.Items[currentSelectedIndex + 1].Selected = true;
                    }
                }
                else if (vailosListView1.SelectedItems != null && vailosListView1.SelectedItems.Count == 0)
                {
                    vailosListView1.Items[0].Selected = true;
                }
            }
        }

        #endregion

        #region listView and file handling
        // ##### listView and file handling functions #####
        private void vailosListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveFile();
            if (vailosListView1.SelectedItems.Count != 0)
            {
                string inputFile = vailosListView1.SelectedItems[0].SubItems[3].Text;
                editTextBox1.Text = File.ReadAllText(inputFile);
                editTextBox1.Enabled = true;
                currentFileName = inputFile;
                vailosListView1.SelectedItems[0].EnsureVisible();
            }
            else if (vailosListView1.SelectedItems.Count == 0)
            {
                if (currentFileName != null && editTextBox1.Text != "")
                    File.WriteAllText(currentFileName, editTextBox1.Text);
                currentFileName = null;
                editTextBox1.Clear();
                editTextBox1.Enabled = false;
            }
        }
        
        private void vailosListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                vailosListView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (vailosListView1.Sorting == SortOrder.Ascending)
                {
                    vailosListView1.Sorting = SortOrder.Descending;
                }
                else
                {
                    vailosListView1.Sorting = SortOrder.Ascending;
                }
            }

            // Call the sort method to manually sort.
            vailosListView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            vailosListView1.ListViewItemSorter = new ListViewItemComparer(e.Column, vailosListView1.Sorting);
        }
        
        private void vailosListView1_Enter(object sender, EventArgs e)
        {
            upDownTextBox1.Focus();
        }
        
        private void columnResizeTimer_Tick(object sender, EventArgs e)
        {
            columnTitle.Width = vailosListView1.Width - columnCreated.Width;
        }
        
        private void saveFile()
        {
            if (vailosListView1.SelectedItems.Count != 0 && currentFileName != null)
                File.WriteAllText(currentFileName, editTextBox1.Text);
        }
        
        private void vailosListView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {

        }
        
        private void vailosListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //int allColumnWidth = 0;
            //int columnCount = vailosListView1.Columns.Count - 1;
            //foreach (ColumnHeader current in vailosListView1.Columns)
            //{
            //    allColumnWidth = allColumnWidth + current.Width;
            //}
            //if (allColumnWidth != vailosListView1.Width && _skipOnce)
            //{
            //    _skipOnce = false;
            //    vailosListView1.Columns[columnCount].Width = -2;
            //}
            //else
            //{
            //    _skipOnce = true;
            //}
        }
        
        private void deleteSelectedItem()
        {
            if (currentFileName != null)
            {
                if (Properties.Settings.Default.confirmNoteDeletion)
                {
                    switch (MessageBox.Show("Do you really want to delete this file?\n" + currentFileName,
                        "Delete " + currentFileName + "?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            File.Delete(currentFileName);
                            vailosListView1.Items.Clear();
                            currentFileName = null;
                            editTextBox1.Clear();
                            vailosListView1.loadFileList(fileDirPath);
                            break;
                    }
                }
                else if (Properties.Settings.Default.confirmNoteDeletion == false)
                {
                    File.Delete(currentFileName);
                    vailosListView1.Items.Clear();
                    currentFileName = null;
                    editTextBox1.Clear();
                    vailosListView1.loadFileList(fileDirPath);
                }
                else
                {
                    MessageBox.Show("Configuration Error",
                        "Configuration File Error - Missing \"confirmNoteDeletion\" entry.",
                        MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("No File Selected, unable to delete", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void renameSelectedItem()
        {
            renameBox rb = new renameBox();
            if (rb.setCaller(this) == false)
            {
                MessageBox.Show("No file Selected");
            }
            else
            {
                rb.ShowDialog();
            }
        }
        /// <summary>
        /// Getter for name of current file
        /// </summary>
        /// <returns></returns>
        public string getSelectedFileName()
        {
            if (vailosListView1.itemSelected())
            {
                return vailosListView1.getSelectedFileName();
            }
            return "";
        }
        /// <summary>
        /// Setter for name of selected file
        /// </summary>
        /// <param name="inFileName"></param>
        public void setSelectedFileName(string inFileName)
        {
            bool alreadyExists = false;
            for (int i = 0; i < vailosListView1.Items.Count; i++)
            {
                if (vailosListView1.Items[i].Text == inFileName)
                {
                    alreadyExists = true;
                }
            }
            if (!alreadyExists)
            {
                if (vailosListView1.renameSelectedFile(inFileName, fileDirPath))
                {
                    vailosListView1.Items.Clear();
                    vailosListView1.loadFileList(fileDirPath);
                    return;
                }
                else
                {
                    MessageBox.Show("Error renaming file");
                }
            }
            else
            {
                MessageBox.Show("A file with the new name already exists");
            }
        }

        #endregion

        #region Context Menu
        // ######### Context Menu Functions #########
        private void renameContextMenuItem1_Click(object sender, EventArgs e)
        {
            renameSelectedItem();
        }
        
        private void editInExtAppContextMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteContextMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelectedItem();
        }
        #endregion

        #region editTextBox functions

        private void editTextBox1_Enter(object sender, EventArgs e)
        {
            editTextBox1.SelectionStart = 0;
        }
        /// <summary>
        /// Grab the editbox text to write to file
        /// </summary>
        /// <returns></returns>
        public string getEditBoxText()
        {
            return editTextBox1.Text;
        }
        #endregion

        private void userSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vailosListView1.itemSelected())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = extEditor;
                startInfo.Arguments = fileDirPath + vailosListView1.getSelectedFileName();
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("No file selected to edit");
            }
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vailosListView1.itemSelected())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "notepad.exe";
                startInfo.Arguments = fileDirPath + vailosListView1.getSelectedFileName();
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("No file selected to edit");
            }
        }

        private void pasteAsNewNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText() == false)
            {
                return;
            }
            int numOfLetters = 60;
            int numOfWords = 5;
            string blank = " ";
            char[] delim = blank.ToCharArray();
            string bodyText = Clipboard.GetText();
            string firstSixty = bodyText.Substring(0, numOfLetters);
            string[] titleHolder = firstSixty.Split(delim, numOfWords);
            string title = titleHolder[0];
            for (int i = 1; i < titleHolder.Count(); i++)
            {
                title = title + " " + titleHolder[i];
            }

            MessageBox.Show("Title: " + title + ".txt" + " Text: " + title + bodyText);
        }

    }

    #region ListViewItemComparer Class
    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y) 
        {
            int returnVal= -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                    ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }

    }
    #endregion
}
