using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tetris.Interfaces;

namespace Tetris.Models
{
    class BlockJ : IPlayerBlock
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
        private static BlockJ _instance = new BlockJ();
        private BlockJ()
        {
            XPos = new int[4];
            YPos = new int[4];
            YOffset = 1;
            XOffset = 0;
            XDisplacement = 0;
            YDisplacement = 0;
            XPosBounds = new int[4];
            YPosBounds = new int[4];
            BlockColor = Color.BlueViolet;
            Mode = 1;
            Reset();
        }
        public static BlockJ GetInstance()
        {
            return _instance;
        }
        public void SetMode()
        {
            Mode++;
            if (Mode>=5)
            {
                Mode = 1;
            }
            switch (Mode)
            {
                case 1:
                    XPos[0] = 3 + XDisplacement;
                    YPos[0] = 1 + YDisplacement;

                    XPos[1] = 4 + XDisplacement;
                    YPos[1] = 1 + YDisplacement;

                    XPos[2] = 5 + XDisplacement;
                    YPos[2] = 1 + YDisplacement;

                    XPos[3] = 5 + XDisplacement;
                    YPos[3] = 2 + YDisplacement;

                    XPosBounds[0] = 3 + XDisplacement;
                    YPosBounds[0] = 2 + YDisplacement;

                    XPosBounds[1] = 4 + XDisplacement;
                    YPosBounds[1] = 0 + YDisplacement;

                    XPosBounds[2] = 4 + XDisplacement;
                    YPosBounds[2] = 1 + YDisplacement;

                    XPosBounds[3] = 4 + XDisplacement;
                    YPosBounds[3] = 2 + YDisplacement;
                    break;
                case 2:
                    XPos[0] = 3 + XDisplacement;
                    YPos[0] = 2 + YDisplacement;

                    XPos[1] = 4 + XDisplacement;
                    YPos[1] = 0 + YDisplacement;

                    XPos[2] = 4 + XDisplacement;
                    YPos[2] = 1 + YDisplacement;

                    XPos[3] = 4 + XDisplacement;
                    YPos[3] = 2 + YDisplacement;

                    XPosBounds[0] = 3 + XDisplacement;
                    YPosBounds[0] = 0 + YDisplacement;

                    XPosBounds[1] = 3 + XDisplacement;
                    YPosBounds[1] = 1 + YDisplacement;

                    XPosBounds[2] = 4 + XDisplacement;
                    YPosBounds[2] = 1 + YDisplacement;

                    XPosBounds[3] = 5 + XDisplacement;
                    YPosBounds[3] = 1 + YDisplacement;

                    break;
                case 3:
                    XPos[0] = 3 + XDisplacement;
                    YPos[0] = 0 + YDisplacement;

                    XPos[1] = 3 + XDisplacement;
                    YPos[1] = 1 + YDisplacement;

                    XPos[2] = 4 + XDisplacement;
                    YPos[2] = 1 + YDisplacement;

                    XPos[3] = 5 + XDisplacement;
                    YPos[3] = 1 + YDisplacement;

                    XPosBounds[0] = 4 + XDisplacement;
                    YPosBounds[0] = 0 + YDisplacement;

                    XPosBounds[1] = 4 + XDisplacement;
                    YPosBounds[1] = 1 + YDisplacement;

                    XPosBounds[2] = 4 + XDisplacement;
                    YPosBounds[2] = 2 + YDisplacement;

                    XPosBounds[3] = 5 + XDisplacement;
                    YPosBounds[3] = 0 + YDisplacement;
                    break;
                case 4:
                    XPos[0] = 4 + XDisplacement;
                    YPos[0] = 0 + YDisplacement;

                    XPos[1] = 4 + XDisplacement;
                    YPos[1] = 1 + YDisplacement;

                    XPos[2] = 4 + XDisplacement;
                    YPos[2] = 2 + YDisplacement;

                    XPos[3] = 5 + XDisplacement;
                    YPos[3] = 0 + YDisplacement;

                    XPosBounds[0] = 3 + XDisplacement;
                    YPosBounds[0] = 1 + YDisplacement;

                    XPosBounds[1] = 4 + XDisplacement;
                    YPosBounds[1] = 1 + YDisplacement;

                    XPosBounds[2] = 5 + XDisplacement;
                    YPosBounds[2] = 1 + YDisplacement;

                    XPosBounds[3] = 5 + XDisplacement;
                    YPosBounds[3] = 2 + YDisplacement;
                    break;
                default:
                    throw new Exception("BlockJ supports 4 modes of rotation only");
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
            XPos[0] = 3 + XDisplacement;
            YPos[0] = 1 + YDisplacement;

            XPos[1] = 4 + XDisplacement;
            YPos[1] = 1 + YDisplacement;

            XPos[2] = 5 + XDisplacement;
            YPos[2] = 1 + YDisplacement;

            XPos[3] = 5 + XDisplacement;
            YPos[3] = 2 + YDisplacement;

            XPosBounds[0] = 3 + XDisplacement;
            YPosBounds[0] = 2 + YDisplacement;

            XPosBounds[1] = 4 + XDisplacement;
            YPosBounds[1] = 0 + YDisplacement;

            XPosBounds[2] = 4 + XDisplacement;
            YPosBounds[2] = 1 + YDisplacement;

            XPosBounds[3] = 4 + XDisplacement;
            YPosBounds[3] = 2 + YDisplacement;
        }
    }
}
