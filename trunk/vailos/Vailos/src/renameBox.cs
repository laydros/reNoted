using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vailos
{
    /// <summary>
    /// Form to handle renaming files
    /// </summary>
    public partial class renameBox : Form
    {
        string startFileName;
        VailosMainWin caller;
        /// <summary>
        /// Constructor for renameBox
        /// </summary>
        public renameBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// function to give renameBox the calling window
        /// </summary>
        /// <param name="callingWin"></param>
        /// <returns></returns>
        public bool setCaller(VailosMainWin callingWin)
        {
            caller = callingWin;
            startFileName = caller.getSelectedFileName();
            if (startFileName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void renameBoxRenameButton_Click(object sender, EventArgs e)
        {
            if (renameFileTextBox.Text != "")
            {
                caller.setSelectedFileName(renameFileTextBox.Text);
                Close();
            }
        }

        private void renameBox_Load(object sender, EventArgs e)
        {
            renameFileTextBox.Text = startFileName;
        }

        private void renameFileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                renameBoxRenameButton.PerformClick();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                renameBoxCancelButton.PerformClick();
            }
        }
    }
}
