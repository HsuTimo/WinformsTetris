using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tetris.Interfaces;

namespace Tetris.Models
{
    class BlockI : IPlayerBlock
    {
        public int[] XPos { get ; set; }
        public int[] YPos { get; set; }
        public int[] XPosBounds { get; set; }
        public int[] YPosBounds { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int XDisplacement { get; set; }
        public int YDisplacement { get; set; }
        public Color BlockColor { get; set; }
        public byte Mode { get; set; }
        private static BlockI _instance = new BlockI();
        private BlockI()
        {
            XPos = new int[4];
            YPos = new int[4];
            YOffset = 1;
            XOffset = 0;
            XDisplacement = 0;
            YDisplacement = 0;
            XPosBounds = new int[4];
            YPosBounds = new int[4];
            BlockColor = Color.Red;
            Mode = 1;
            Reset();
        }
        public static BlockI GetInstance()
        {
            return _instance;
        }
        public void SetMode()
        {
            Mode++;
            if (Mode>=3)
            {
                Mode = 1;
            }
            switch (Mode)
            {
                case 1:
                    for (int i = 0; i < XPos.Length; i++)
                    {
                        XPos[i] = 3 + i + XDisplacement;
                        YPos[i] = 1 + YDisplacement;
                        XPosBounds[i] = 4 + XDisplacement;
                        YPosBounds[i] = 0 + i + YDisplacement;
                    }
                    break;
                case 2:
                    for (int i = 0; i < XPos.Length; i++)
                    {
                        XPos[i] = 4 + XDisplacement;
                        YPos[i] = 0 + i + YDisplacement;
                        XPosBounds[i] = 3 + i + XDisplacement;
                        YPosBounds[i] = 1 + YDisplacement;
                    }
                    break;
                default:
                    throw new Exception("BlockI supports 2 modes of rotation only");
            }
        }
        public void UpdateYOffset()
        {
            YDisplacement+=YOffset;
            for (int i = 0; i < XPos.Length; i++)
            {
                YPos[i] += YOffset;
                YPosBounds[i] += YOffset;
            }
        }
        public void UpdateXOffset()
        {
            XDisplacement+=XOffset;
            for (int i = 0; i < XPos.Length; i++)
            {
                XPos[i] += XOffset;
                XPosBounds[i] += XOffset;
            }
        }
        public void Reset()
        {
            XDisplacement = 0;
            YDisplacement = 0;
            YOffset = 1;
            Mode = 1;
            for (int i = 0; i < XPos.Length; i++)
            {
                XPos[i] = 3 + i;
                YPos[i] = 1;
                XPosBounds[i] = 4 + XDisplacement;
                YPosBounds[i] = 0 + i + YDisplacement;
            }  
        }
    }
}
