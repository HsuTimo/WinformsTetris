using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tetris.Models;

namespace Tetris.Interfaces
{
    public interface ITetromino
    {
        int[] XPos { get; set; }
        int[] YPos { get; set; }
        Color BlockColor { get; set; }
        byte Mode { get; set; }
        void SetMode();
    }
}
