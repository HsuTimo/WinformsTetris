using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tetris.Models;

namespace Tetris
{
    public partial class TetrisMenu : Form
    {
        private int _selectedLevel { get; set; }
        public TetrisMenu()
        {
            InitializeComponent();
        }

        private void TetrisMenu_Load(object sender, EventArgs e)
        {
            _selectedLevel = 0;
            UpdateHighscoreList();
        }

        private void BtnPlayPressed(object sender, EventArgs e)
        {
            if (FormInstances.tetrisForm!=null)
            {
                FormInstances.tetrisForm.Dispose();
            }
            FormInstances.tetrisForm = new TetrisForm();
            FormInstances.tetrisForm.Show();
            FormInstances.tetrisMenu.Hide();
            FormInstances.tetrisForm.SetLevel(_selectedLevel);
            FormInstances.tetrisForm.TogglePaused();
        }

        private void TetrisMenuShown(object sender, EventArgs e)
        {
            if (FormInstances.tetrisForm != null)
            {
                if (FormInstances.tetrisForm.Visible == true)
                {
                    FormInstances.tetrisForm.Hide();
                }
            }
        }

        private void TetrisMenuClosed(object sender, FormClosedEventArgs e)
        {
            if (FormInstances.tetrisForm != null)
            {
                FormInstances.tetrisForm.Dispose();
            }
        }

        private void MenuLevelSelection(object sender, EventArgs e)
        {
            _selectedLevel = Int32.Parse(menuLevel.Text[menuLevel.Text.Length - 1].ToString());
            UpdateHighscoreList();
        }
        public void UpdateHighscoreList()
        {
            lstHighScores.Items.Clear();
            int counter = 1;
            foreach (var item in Collections.Playerscores)
            {
                if (item.StartLevel == _selectedLevel)
                {
                    lstHighScores.Items.Add($"{counter}.{item.Name} : {item.Score}");
                    counter++;
                }
            }
        }
    }
}
