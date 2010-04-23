using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vailos
{
    class editTextBox : TextBox
    {
        // grab main window to call functions in it
        VailosMainWin callerWin;
        bool acceptTab;
        public void setCaller(VailosMainWin caller)
        {
            callerWin = caller;
        }
        public void setAcceptsTab(bool inAcceptTab)
        {
            acceptTab = inAcceptTab;
        }
        // this grabs the key when it comes in so OnKeyPress can do something with it
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }
        // handle incoming keys
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // if user hits tab when cursor is in first position of edit text box,
            // jump back up to search box
            if (e.KeyChar == (char)Keys.Tab 
                && (this.SelectionStart == 0 || acceptTab == false))
            {
                callerWin.giveSearchBoxFocus();
                e.Handled = true;
            }
            // i don't care about this key, keep for default handler
            else
            {
                base.OnKeyPress(e);
            }
        }
        // select all text
    }
}
