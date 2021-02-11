using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tetris.Models
{
    [Serializable]
    public class Playerscore:IComparable<Playerscore>
    {
        public int StartLevel { get; private set; }
        public int Score { get; private set; }
        public string Name { get; private set; }
        public Playerscore(int startLevel, int score, string name)
        {
            StartLevel = startLevel;
            Score = score;
            Name = name;
        }

        public int CompareTo(Playerscore other)
        {
            return Score.CompareTo(other.Score)*-1;
        }
    }
}
