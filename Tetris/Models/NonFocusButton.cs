using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Tetris.Models
{
    public class NonFocusButton : Button
    {
        public NonFocusButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
