using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vailos
{
    /// <summary>
    /// Preferences Dialog for Vailos
    /// </summary>
    public partial class Preferences : Form
    {
        #region Global Variables
        bool autoSaveEnabled;
        bool autoSaveBackup;
        int autoSaveIntervalInt;
        bool confirmNoteDelete;
        bool tabInEditBox;
        string dirPath;
        string userEditor;
        #endregion

        /// <summary>
        /// Constructor for Vailos Preferences
        /// </summary>
        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            readPerfs();
            setUpWindow();
        }

        private void setUpWindow()
        {
            fileDirNameLabel.Text = dirPath;
            userEditorNameLabel.Text = userEditor;
            deleteConfirmCheck.Checked = confirmNoteDelete;
            autoSaveEnabledCheck.Checked = autoSaveEnabled;
            autoSaveBackupCheck.Checked = autoSaveBackup;
            autoSaveInterval.Value = autoSaveIntervalInt;
            if (tabInEditBox)
            {
                insertTabRadioButton.Checked = true;
                navigateTabRadioButton.Checked = false;
            }
            else if (tabInEditBox == false)
            {
                insertTabRadioButton.Checked = false;
                navigateTabRadioButton.Checked = true;
            }
        }

        private void readPerfs()
        {
            autoSaveEnabled = Properties.Settings.Default.autoSaveOn;
            autoSaveBackup = Properties.Settings.Default.autoSaveBackup;
            autoSaveIntervalInt = Properties.Settings.Default.autoSaveTime;
            confirmNoteDelete = Properties.Settings.Default.confirmNoteDeletion;
            tabInEditBox = Properties.Settings.Default.tabInEditBox;
            dirPath = Properties.Settings.Default.dirPath;
            userEditor = Properties.Settings.Default.externalEditorPath;
        }

        private void writePerfs()
        {
            Properties.Settings.Default.autoSaveOn = autoSaveEnabledCheck.Checked;
            Properties.Settings.Default.autoSaveBackup = autoSaveBackupCheck.Checked;
            Properties.Settings.Default.autoSaveTime = (int)autoSaveInterval.Value;
            Properties.Settings.Default.confirmNoteDeletion = deleteConfirmCheck.Checked;
            Properties.Settings.Default.tabInEditBox = insertTabRadioButton.Checked;
            Properties.Settings.Default.dirPath = dirPath;
            Properties.Settings.Default.externalEditorPath = userEditor;
        }

        private void prefsCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void prefsSaveButton_Click(object sender, EventArgs e)
        {
            writePerfs();
            Close();
        }

        private void autoSaveEnabledCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSaveEnabledCheck.Checked == true)
            {
                autoSaveBackupCheck.Enabled = true;
                autoSaveInterval.Enabled = true;
                autoSaveIntervalLabel.Enabled = true;
            }
            else if (autoSaveEnabledCheck.Checked == false)
            {
                autoSaveBackupCheck.Enabled = false;
                autoSaveInterval.Enabled = false;
                autoSaveIntervalLabel.Enabled = false;
            }
        }

        private void dirChooseButton_Click(object sender, EventArgs e)
        {
            DialogResult dirChooserResult = folderBrowserDialog1.ShowDialog();
            if (dirChooserResult == DialogResult.OK)
            {
                dirPath = folderBrowserDialog1.SelectedPath + "\\";
                fileDirNameLabel.Text = dirPath;
            }
        }

        private void userEditorChooseButton_Click(object sender, EventArgs e)
        {
            DialogResult extEditorChooserResult = userEditorDialog.ShowDialog();
            if (extEditorChooserResult == DialogResult.OK)
            {
                userEditor = userEditorDialog.FileName;
                userEditorNameLabel.Text = userEditor;
            }
        }

    }
}
