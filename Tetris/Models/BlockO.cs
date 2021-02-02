using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tetris.Interfaces;

namespace Tetris.Models
{
    class BlockO : IPlayerBlock
    {
        public int[] XPos { get ; set; }
        public int[] YPos { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int[] XPosBounds { get; set; }
        public int[] YPosBounds { get; set; }
        public int XDisplacement { get; set; }
        public int YDisplacement { get; set; }
        public Color BlockColor { get; set; }
        public byte Mode { get; set; }
        private static BlockO _instance = new BlockO();
        private BlockO()
        {
            XPos = new int[4];
            YPos = new int[4];
            YOffset = 1;
            XOffset = 0;
            XDisplacement = 0;
            YDisplacement = 0;
            XPosBounds = XPos;
            YPosBounds = YPos;
            BlockColor = Color.CornflowerBlue;
            Mode = 1;
            Reset();
        }
        public static BlockO GetInstance()
        {
            return _instance;
        }
        public void SetMode()
        {
            //Block O has no rotation modes 
        }
        public void UpdateYOffset()
        {
            YDisplacement+=YOffset;
            for (int i = 0; i < XPos.Length; i++)
            {
                YPos[i] += YOffset;
            }
        }
        public void UpdateXOffset()
        {
            XDisplacement+=XOffset;
            for (int i = 0; i < XPos.Length; i++)
            {
                XPos[i] += XOffset;
            }
        }
        public void Reset()
        {
            XDisplacement = 0;
            YDisplacement = 0;
            Mode = 1;
            YOffset = 1;
            XPos[0] = 4;
            YPos[0] = 0;

            XPos[1] = 4;
            YPos[1] = 1;

            XPos[2] = 5;
            YPos[2] = 0;

            XPos[3] = 5;
            YPos[3] = 1;
        }
    }
}
