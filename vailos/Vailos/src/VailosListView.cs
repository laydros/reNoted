using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vailos
{
    class VailosListView : ListView
    {
        VailosMainWin caller;
        public bool loadFileList(string inFilePath)
        {
            string filesDirPath = inFilePath;
            // if the directory doesn't exist, create one
            if (!Directory.Exists(@filesDirPath))
            {
                Directory.CreateDirectory(@filesDirPath);
            }
            // this loads all the txt files into filePaths array
            string[] filePaths = Directory.GetFiles(@filesDirPath, "*.txt");
            // tell list view to hang on while we update things
            this.BeginUpdate();
            // add each file to the list view
            foreach (string stringFile in filePaths)
            {
                addNewItem(filesDirPath, Path.GetFileName(stringFile));
            }
            // tell list view we are done, carry on
            this.EndUpdate();
            // in theory this will return false if there is an error,
            //  but this is not yet implemented
            return true;
        }
 
        public ListViewItem addNewItem(string fileDirPath, string newFileName)
        {
            string fullPathName = fileDirPath + newFileName;        // get a the full filename with path
            VailosTimeHandler vt = new VailosTimeHandler();         // to mangle time stuff
            FileInfo thisFilesInfo = new FileInfo(fullPathName);    // fileinfo can help grab dates
            DateTime dtCreationDate = thisFilesInfo.CreationTime;   // setup creation date and modified date
            DateTime dtLastModified = thisFilesInfo.LastWriteTime;
            string creationString = vt.convertDateTimeToSortableString(dtCreationDate, "yes");
            string modifiedString = vt.convertDateTimeToSortableString(dtLastModified, "yes");

            //Create actual list item
            ListViewItem lvItem = new ListViewItem(newFileName);
            lvItem.SubItems.Add(creationString);
            lvItem.SubItems.Add(modifiedString);
            lvItem.SubItems.Add(fullPathName);
            // stick newly created list item in listViews array
            this.Items.Add(lvItem);
            // return newly created item so it can be selected, etc. if needed
            return lvItem;
        }

        /// <summary>
        /// Grab caller to run some of its functions
        /// </summary>
        /// <param name="inWin"></param>
        public void setCaller(VailosMainWin inWin)
        {
            caller = inWin;
        }

        public string getSelectedFileName()
        {
            if (itemSelected() == true)
            {
                return getSelectedItem().Text;
            }
            return "";
        }

        public ListViewItem getSelectedItem()
        {
            if (itemSelected())
            {
                return this.SelectedItems[0];
            }
            else
            {
                return null;
            }
        }

        public bool renameSelectedFile(string newFileName, string dirPath)
        {
            if (itemSelected())
            {
                string oldFileName = getSelectedItem().Text;
                getSelectedItem().Text = newFileName;
                getSelectedItem().SubItems[3].Text = dirPath + newFileName;
                File.WriteAllText(dirPath + newFileName, caller.getEditBoxText());
                File.Delete(dirPath + oldFileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool itemSelected()
        {
            if (this.SelectedItems.Count == 0)
            {
                return false;
            }
            else if (this.SelectedItems.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}