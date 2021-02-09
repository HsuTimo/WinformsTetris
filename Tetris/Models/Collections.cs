using System;
using System.Collections.Generic;
using System.IO;
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
            string fileName = "scores.txt";
            string filePath = saveDir + fileName;
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                sr = new StreamReader(fs);
                string content;
                while (!sr.EndOfStream)
                {
                    content = sr.ReadLine();
                    if (!String.IsNullOrEmpty(content))
                    {
                        string[] entry = content.Split(',');
                        Playerscores.Add(new Playerscore(Int32.Parse(entry[0]),Int32.Parse(entry[1]),entry[2]));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public static void SaveScoresToFile()
        {
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/My Games/Winforms Tetris Tim Hsu/";
            string fileName = "scores.txt";
            string filePath = saveDir + fileName;
            FileStream fs = null;
            StreamWriter sr = null;
            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                sr = new StreamWriter(fs);
                foreach (var item in Playerscores)
                {
                    sr.WriteLine($"{item.StartLevel},{item.Score},{item.Name}");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }
}
