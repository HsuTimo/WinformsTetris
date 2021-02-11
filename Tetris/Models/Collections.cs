using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Tetris.Interfaces;

namespace Tetris.Models
{
    static public class Collections
    {
        public static IPlayerBlock[] PlayerBlocks = new IPlayerBlock[7];
        public static int[] GameSpeed = new int[21];
        public static List<Playerscore> Playerscores = new List<Playerscore>();
        static Collections()
        {
            #region Adding blocks
            PlayerBlocks[0] = BlockI.GetInstance();
            PlayerBlocks[1] = BlockJ.GetInstance();
            PlayerBlocks[2] = BlockL.GetInstance();
            PlayerBlocks[3] = BlockO.GetInstance();
            PlayerBlocks[4] = BlockS.GetInstance();
            PlayerBlocks[5] = BlockT.GetInstance();
            PlayerBlocks[6] = BlockZ.GetInstance();
            #endregion
            #region Adding game speed table
            GameSpeed[0] = 53;
            GameSpeed[1] = 49;
            GameSpeed[2] = 45;
            GameSpeed[3] = 41;
            GameSpeed[4] = 37;
            GameSpeed[5] = 33;
            GameSpeed[6] = 28;
            GameSpeed[7] = 22;
            GameSpeed[8] = 17;
            GameSpeed[9] = 11;
            GameSpeed[10] = 10;
            GameSpeed[11] = 9;
            GameSpeed[12] = 8;
            GameSpeed[13] = 7;
            GameSpeed[14] = 6;
            GameSpeed[15] = 6;
            GameSpeed[16] = 5;
            GameSpeed[17] = 5;
            GameSpeed[18] = 4;
            GameSpeed[19] = 4;
            GameSpeed[20] = 3;
            #endregion
        }
        public static void MakeTopFive(int level)
        {
            Playerscores.Sort();
            int counter = 0;
            List<int> indexToDelete = new List<int>();
            for (int i = 0; i < Playerscores.Count; i++)
            {
                if (Playerscores[i].StartLevel == level)
                {
                    counter++;
                    if (counter>5)
                    {
                        indexToDelete.Add(i);
                    }
                }
            }
            foreach (var item in indexToDelete)
            {
                Playerscores.RemoveAt(item);
            }
        }
        public static void LoadScoresFromFile()
        {
            Playerscores.Clear();
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/My Games/Winforms Tetris Tim Hsu/";
            string fileName = "scores.bin";
            string filePath = saveDir + fileName;
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            using (Stream stream = File.Open(filePath,FileMode.OpenOrCreate))
            {
                BinaryFormatter bin = new BinaryFormatter();
                if (stream.Length != 0)
                {
                    Playerscores = (List<Playerscore>)bin.Deserialize(stream);
                }
            }
        }
        public static void SaveScoresToFile()
        {
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/My Games/Winforms Tetris Tim Hsu/";
            string fileName = "scores.bin";
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            string filePath = saveDir + fileName;
            using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, Playerscores);
            }
        }
    }
}
