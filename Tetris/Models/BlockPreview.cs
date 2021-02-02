using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tetris.Interfaces;

namespace Tetris.Models
{
    class BlockPreview:ITetromino
    {
        public int[] XPos { get; set; }
        public int[] YPos { get; set; }
        public Color BlockColor { get; set; }
        public byte Mode { get; set; }
        private EnumBlockType _enumBlockType { get; set; }

        private static BlockPreview _instance = new BlockPreview();
        public static BlockPreview GetInstance 
        {
            get 
            {
                return _instance;
            }
        }
        BlockPreview()
        {
            XPos = new int[4];
            YPos = new int[4];
            _enumBlockType = EnumBlockType.I;
            BlockColor = Color.Black;
        }
        public void SetPreview(EnumBlockType enumBlockType)
        {
            _enumBlockType = enumBlockType;
        }
        public void SetMode()
        {
            switch (_enumBlockType)
            {
                case EnumBlockType.I:
                    for (int i = 0; i < XPos.Length; i++)
                    {
                        XPos[i] = 0 + i;
                        YPos[i] = 1;
                    }
                    BlockColor = Color.Red;
                    break;
                case EnumBlockType.J:
                    XPos[0] = 0;
                    YPos[0] = 1;

                    XPos[1] = 1;
                    YPos[1] = 1;

                    XPos[2] = 2;
                    YPos[2] = 1;

                    XPos[3] = 2;
                    YPos[3] = 2;
                    BlockColor = Color.BlueViolet;
                    break;
                case EnumBlockType.L:
                    XPos[0] = 0;
                    YPos[0] = 1;

                    XPos[1] = 0;
                    YPos[1] = 2;

                    XPos[2] = 1;
                    YPos[2] = 1;

                    XPos[3] = 2;
                    YPos[3] = 1;
                    BlockColor = Color.Orange;
                    break;
                case EnumBlockType.O:
                    XPos[0] = 1;
                    YPos[0] = 1;

                    XPos[1] = 1;
                    YPos[1] = 2;

                    XPos[2] = 2;
                    YPos[2] = 1;

                    XPos[3] = 2;
                    YPos[3] = 2;
                    BlockColor = Color.CornflowerBlue;
                    break;
                case EnumBlockType.S:
                    XPos[0] = 0;
                    YPos[0] = 2;

                    XPos[1] = 1;
                    YPos[1] = 1;

                    XPos[2] = 1;
                    YPos[2] = 2;

                    XPos[3] = 2;
                    YPos[3] = 1;
                    BlockColor = Color.Yellow;
                    break;
                case EnumBlockType.T:
                    XPos[0] = 0;
                    YPos[0] = 1;

                    XPos[1] = 1;
                    YPos[1] = 1;

                    XPos[2] = 1;
                    YPos[2] = 2;

                    XPos[3] = 2;
                    YPos[3] = 1;
                    BlockColor = Color.LightGreen;
                    break;
                case EnumBlockType.Z:
                    XPos[0] = 0;
                    YPos[0] = 1;

                    XPos[1] = 1;
                    YPos[1] = 1;

                    XPos[2] = 1;
                    YPos[2] = 2;

                    XPos[3] = 2;
                    YPos[3] = 2;
                    BlockColor = Color.Cyan;
                    break;
                default:
                    throw new Exception("Block type preview does not exist!");
            }
        }
    }
}
