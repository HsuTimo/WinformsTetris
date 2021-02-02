using System;
using System.Windows.Forms;
using Tetris.Models;

namespace Tetris
{
    public partial class HighscoreForm : Form
    {
        private string _playerName { get; set; }
        public HighscoreForm()
        {
            InitializeComponent();
            _playerName = "Player";
        }

        private void AddNewHighscore()
        {
            Collections.Playerscores.Add(new Playerscore(FormInstances.tetrisForm.StartLevel, FormInstances.tetrisForm.Score, _playerName));
            Collections.Playerscores.Sort();
            Collections.MakeTopFive(FormInstances.tetrisForm.StartLevel);
            Collections.SaveScoresToFile();
            this.Close();
        }

        private void BtnQuitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                _playerName = "Player";
            }
            else
            {
                _playerName = txtName.Text;
            }
            AddNewHighscore();
        }
    }
}
