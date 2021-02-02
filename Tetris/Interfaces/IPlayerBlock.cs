using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Models;

namespace Tetris.Interfaces
{
    public interface IPlayerBlock:ITetromino
    {
        int[] XPosBounds { get; set; }
        int[] YPosBounds { get; set; }
        int XOffset { get; set; }
        int YOffset { get; set; }
        int XDisplacement { get; set; }
        int YDisplacement { get; set; }
        void UpdateYOffset();
        void UpdateXOffset();
        void Reset();
    }
}
