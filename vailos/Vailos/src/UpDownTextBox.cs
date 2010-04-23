using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vailos
{
    class UpDownTextBox : TextBox
    {
        // grab main window so we can call its functions
        VailosMainWin callerWin;
        public void setCaller(VailosMainWin caller)
        {
            callerWin = caller;
        }
        // this grabs the key when it comes in so OnKeyPress can do something with it
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }
        // handle incoming keys
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // up key gets next file in listView
            if (e.KeyData == Keys.Up)
            {
                callerWin.searchBoxUpPressed();
                e.Handled = true;
            }
            // down key gets previous file in listView
            else if (e.KeyData == Keys.Down)
            {
                callerWin.searchBoxDownPressed();
                e.Handled = true;
            }
            // i don't care about this key, keep for default handler
            else
            {
                base.OnKeyDown(e);
            }
        }
    }
}
