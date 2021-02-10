using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris
{
    public partial class TetrisForm : Form
    {
        private IPlayerBlock _playerBlock;
        private readonly BlockPreview _blockPreview = BlockPreview.GetInstance;
        private readonly Random _random = new Random();
        private EnumBlockType _blockType = new EnumBlockType();
        private EnumBlockType _nextBlockType = new EnumBlockType();
        private int _totalLines = 0;
        private int _tenLineCounter = 0;
        public int StartLevel { get; private set; }
        private int _level = 0;
        private int _gameSpeed;
        private int _frameCounter = 0;
        private int _lineCombo = 0;
        public int Score { get; private set; }
        private const int _BOXINTSIZE = 25;
        private const int _BOXMARGIN = 2;
        private bool _paused;
        private bool _gameOver;
        private Size boxSize = new Size(_BOXINTSIZE - _BOXMARGIN, _BOXINTSIZE - _BOXMARGIN);
        private PictureBox[,] _matrix = new PictureBox[10, 20];
        private PictureBox[,] _previewMatrix = new PictureBox[4, 4];
        private PictureBox[] _lineIndicators = new PictureBox[20];
        private bool[,] _settledMatrix = new bool[10, 20];
        private Color[,] _settledColorMatrix = new Color[10, 20];
        public TetrisForm()
        {
            InitializeComponent();
            Reset();
            InitializeBlocks(_BOXINTSIZE, boxSize);
            ResetSettledMatrix();
            AddControls();
        }
        private void Reset()
        {
            _level = 0;
            Score = 0;
            _lineCombo = 0;
            _frameCounter = 0;
            _paused = false;
            _gameOver = false;
            _totalLines = 0;
            _tenLineCounter = 0;
            _blockType = GetRandomBlockType();
            _nextBlockType = GetRandomBlockType();
            SetGameSpeed();
            SetBlockType();
            _blockPreview.SetPreview(_nextBlockType);
            lblLevel.Text = _level.ToString();
            lblScore.Text = Score.ToString();
            lblPaused.Visible = false;
            lblGameOver.Visible = false;
        }
        private void InitializeBlocks(int boxIntSize, Size boxSize)
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = new PictureBox();
                    _matrix[i, j].Visible = false;
                    _matrix[i, j].Size = boxSize;
                    _matrix[i, j].Location = new Point(30 + _BOXMARGIN + (i * boxIntSize), 30 + _BOXMARGIN + (j * boxIntSize));
                    
                }
            }
            InitializePreview(boxIntSize,boxSize);
            InitializeLineIndicators(boxIntSize);
            DrawPlayerBlock();
        }
        private void InitializePreview(int boxIntSize, Size boxSize)
        {
            for (int i = 0; i < _previewMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _previewMatrix.GetLength(1); j++)
                {
                    _previewMatrix[i, j] = new PictureBox();
                    _previewMatrix[i, j].Size = boxSize;
                    _previewMatrix[i,j].Location = new Point(picPreviewFrame.Location.X + 10 + (i * boxIntSize), picPreviewFrame.Location.Y + 10 + (j * boxIntSize));
                    _previewMatrix[i, j].BackColor = Color.Black;
                    _previewMatrix[i, j].Visible = false;
                }
            }
        }
        private void InitializeLineIndicators(int boxIntSize)
        {
            for (int i = 0; i < _lineIndicators.Length; i++)
            {
                _lineIndicators[i] = new PictureBox();
                _lineIndicators[i].Size = new Size(boxIntSize*10,boxIntSize);
                _lineIndicators[i].Location = new Point(30,30+((i * boxIntSize)));
                _lineIndicators[i].BackColor = Color.White;
                _lineIndicators[i].Visible = false;
                this.Controls.Add(_lineIndicators[i]);
                _lineIndicators[i].BringToFront();
            }
        }
        private void ResetSettledMatrix()
        {
            for (int i = 0; i < _settledMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _settledMatrix.GetLength(1); j++)
                {

                    
                    _settledMatrix[i, j] = false;
                    
                }
            }
        }
        private void ResetMatrix()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j].Visible = false;
                }
            }
        }
        private void AddControls()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    this.Controls.Add(_matrix[i, j]);
                    _matrix[i, j].BringToFront();
                }
            }
            for (int i = 0; i < _previewMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _previewMatrix.GetLength(1); j++)
                {
                    this.Controls.Add(_previewMatrix[i, j]);
                    _previewMatrix[i, j].BringToFront();
                }
            }
        }
        private void GameTick(object sender, EventArgs e)
        {
            if (!_paused&&!_gameOver)
            {
                _frameCounter++;
                if (_frameCounter >= _gameSpeed)
                {
                    DrawPreview();
                    DrawPlayerBlock();
                    if (!IsBottomWallCollided() && !IsMatrixAndBlockBottomCollided())
                    {
                        _playerBlock.YOffset = 1;
                    }
                    else
                    {
                        _playerBlock.YOffset = 0;
                    }
                    _playerBlock.UpdateYOffset();
                    CheckBlockSettled();
                    DrawSettledMatrix();
                    LineDetector();
                    lblLines.Text = _totalLines.ToString();
                    CheckLevelUp();
                    _frameCounter = 0;
                }
            } 
        }
        private void GameStart(object sender, EventArgs e)
        {
            FormInstances.tetrisMenu.Show();
            _paused = true;
            
        }
        private void ClearField()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j].Visible = false;
                }
            }
        }
        private void ClearPreview()
        {
            for (int i = 0; i < _previewMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _previewMatrix.GetLength(1); j++)
                {
                    _previewMatrix[i, j].Visible = false;
                }
            }
        }
        private void GameKeyDown(object sender, KeyEventArgs e)
        {
            if (!_paused&&!_gameOver)
            {
                if (e.KeyCode == Keys.Left && !IsLeftWallCollided() && !IsMatrixAndBlockLeftCollided())
                {
                    _playerBlock.XOffset = -1;
                    if (IsMatrixAndBlockLeftCollided() || IsLeftWallCollided())
                    {
                        _playerBlock.XOffset = 0;
                    }
                    _playerBlock.UpdateXOffset();
                    DrawPlayerBlock();
                    DrawSettledMatrix();
                    _playerBlock.XOffset = 0;
                }
                if (e.KeyCode == Keys.Right && !IsRightWallCollided() && !IsMatrixAndBlockRightCollided())
                {
                    _playerBlock.XOffset = 1;
                    if (IsMatrixAndBlockRightCollided() || IsRightWallCollided())
                    {
                        _playerBlock.XOffset = 0;
                    }
                    _playerBlock.UpdateXOffset();
                    DrawPlayerBlock();
                    DrawSettledMatrix();
                    _playerBlock.XOffset = 0;
                }
                if (e.KeyCode == Keys.Down && !IsBottomWallCollided())
                {
                    if (!IsMatrixAndBlockBottomCollided())
                    {
                        _gameSpeed = 3;
                    }
                }
                if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Up) && IsRotationPossible())
                {
                    _playerBlock.SetMode();
                    DrawPlayerBlock();
                    DrawSettledMatrix();
                }
            }
            if (!_gameOver)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                {
                    TogglePaused();
                    TogglePausedScreen();
                }
            }
        }
        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (!_paused)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (!IsMatrixAndBlockBottomCollided())
                    {
                        SetGameSpeed();
                    }
                }
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    _playerBlock.XOffset = 0;
                }
            }
            
        }
        private void DrawPlayerBlock()
        {
            ClearField();
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                _matrix[_playerBlock.XPos[i], _playerBlock.YPos[i]].BackColor = _playerBlock.BlockColor;
                _matrix[_playerBlock.XPos[i], _playerBlock.YPos[i]].Visible = true;
            }
        }
        private void DrawPreview()
        {
            ClearPreview();
            _blockPreview.SetPreview(_nextBlockType);
            _blockPreview.SetMode();
            for (int i = 0; i < _blockPreview.XPos.Length; i++)
            {
                _previewMatrix[_blockPreview.XPos[i], _blockPreview.YPos[i]].BackColor = _blockPreview.BlockColor;
                _previewMatrix[_blockPreview.XPos[i], _blockPreview.YPos[i]].Visible = true;
            }

        }
        private void SetBlockType()
        {
            _playerBlock = Collections.PlayerBlocks[(int)_blockType];
        }
        private bool IsLeftWallCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_playerBlock.XPos[i]==0)
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsRightWallCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_playerBlock.XPos[i] == 9)
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsBottomWallCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.YPos.Length; i++)
            {
                if (_playerBlock.YPos[i] == 19)
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsRotationPossible()
        {
            bool output = true;
            for (int i = 0; i < _playerBlock.XPosBounds.Length; i++)
            {
                if (_playerBlock.XPosBounds[i] <= -1|| _playerBlock.XPosBounds[i] >= 10)
                {
                    output = false;
                }
            }
            for (int i = 0; i < _playerBlock.YPosBounds.Length; i++)
            {
                if (_playerBlock.YPosBounds[i]>=20)
                {
                    output = false;
                }
            }
            for (int i = 0; i < _playerBlock.XPosBounds.Length; i++)
            {
                for (int j = 0; j < _playerBlock.YPosBounds.Length; j++)
                {
                    if (_playerBlock.YPosBounds[j] < 20 && _playerBlock.XPosBounds[i] > -1 && _playerBlock.XPosBounds[i] < 10)
                    {
                        if (_settledMatrix[_playerBlock.XPosBounds[i], _playerBlock.YPosBounds[j]])
                        {
                            output = false;
                        }
                    }
                }
            }
            return output;
        }
        private bool IsMatrixAndBlockLeftCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_settledMatrix[_playerBlock.XPos[i]-1,_playerBlock.YPos[i]])
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsMatrixAndBlockRightCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_settledMatrix[_playerBlock.XPos[i] + 1, _playerBlock.YPos[i]])
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsMatrixAndBlockBottomCollided()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_playerBlock.YPos[i]!=19&&_settledMatrix[_playerBlock.XPos[i], _playerBlock.YPos[i]+1])
                {
                    output = true;
                }
            }
            return output;
        }
        private bool IsMatrixAndBlockOverlapping()
        {
            bool output = false;
            for (int i = 0; i < _playerBlock.XPos.Length; i++)
            {
                if (_playerBlock.YPos[i] != 19 && _settledMatrix[_playerBlock.XPos[i], _playerBlock.YPos[i]])
                {
                    output = true;
                }
            }
            return output;
        }
        private void CheckBlockSettled()
        {
            if (_playerBlock.YOffset == 0 && _playerBlock.XOffset == 0 &&!_paused)
            {
                AddBlockToMatrix();
                SpawnNewBlock();
            }
        }
        private void AddBlockToMatrix()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_matrix[i, j].Visible == true)
                    {
                        _settledMatrix[i, j] = true;
                        _settledColorMatrix[i, j] = _matrix[i, j].BackColor;
                    }
                }
            }
        }
        private void SpawnNewBlock()
        {
            _blockType = _nextBlockType;
            _nextBlockType = GetRandomBlockType();
            SetBlockType();
            SetGameSpeed();
            _playerBlock.Reset();
            CheckGameOver();
        }
        private void DrawSettledMatrix()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_settledMatrix[i,j] == true)
                    {
                        _matrix[i, j].Visible = true;
                        _matrix[i, j].BackColor = _settledColorMatrix[i, j];
                    }
                }
            }
        }  
        private void LineDetector()
        {
            int counter = 0;
            for (int i = 0; i < _settledMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < _settledMatrix.GetLength(0); j++)
                {
                    if (_settledMatrix[j,i])
                    {
                        counter++;
                    }
                }
                if (counter == 10)
                {
                    _totalLines++;
                    _tenLineCounter++;
                    _lineCombo++;
                    RemoveLine(i);
                }
                counter = 0;
            }
            SetScore();
            _lineCombo = 0;
        }
        private void RemoveLine(int index)
        {
            for (int i = 0; i < _settledMatrix.GetLength(0); i++)
            {
                for (int j = index; j > 1; j--)
                {
                    _settledMatrix[i, j] = _settledMatrix[i, j - 1];
                    _settledColorMatrix[i, j] = _settledColorMatrix[i, j - 1];
                }
            }
        }
        private EnumBlockType GetRandomBlockType()
        {
            return (EnumBlockType)_random.Next(Enum.GetNames(typeof(EnumBlockType)).Length);
        }
        private void CheckLevelUp()
        {
            if (_tenLineCounter>=10)
            {
                _level++;
                lblLevel.Text = _level.ToString();
                _tenLineCounter -= 10;
                SetGameSpeed();
            }
        }
        private void SetGameSpeed()
        {
            if (_level<=20)
            {
                _gameSpeed = Collections.GameSpeed[_level];
            }
            else
            {
                _gameSpeed = Collections.GameSpeed[20];
            }
            
        }
        private void SetScore()
        {
            switch (_lineCombo)
            {
                case 1:
                    Score += 40 * (_level + 1);
                    break;
                case 2:
                    Score += 100 * (_level + 1);
                    break;
                case 3:
                    Score += 300 * (_level + 1);
                    break;
                case 4:
                    Score += 1200 * (_level + 1);
                    break;
                default:
                    break;
            }
            lblScore.Text = Score.ToString();
        }
        public void TogglePaused()
        {
            if (!_paused)
            {
                _paused = true;
                _playerBlock.YOffset = 0;
            }
            else
            {
                _paused = false;
                _playerBlock.YOffset = 1;
            }
        }
        private void TogglePausedScreen()
        {
            if (lblPaused.Visible)
            {
                lblPaused.Visible = false;
            }
            else
            {
                lblPaused.Visible = true;
                lblPaused.BringToFront();
            }
            if (btnPause.Text == "Pause")
            {
                btnPause.Text = "Resume";
            }
            else
            {
                btnPause.Text = "Pause";
            }
        }
        private void CheckGameOver()
        {
            if (IsMatrixAndBlockOverlapping())
            {
                _gameOver = true;
                lblPaused.Visible = false;
                lblGameOver.Visible = true;
                lblGameOver.BringToFront();
                if (IsHighscore())
                {
                    HighscoreForm highscoreForm = new HighscoreForm();
                    highscoreForm.ShowDialog();
                } 
            }
        }
        private void BtnPausePressed(object sender, EventArgs e)
        {
            if (!_gameOver)
            {
                TogglePaused();
                TogglePausedScreen();
            } 
        }
        private void BtnResetPressed(object sender, EventArgs e)
        {
            ClearField();
            Reset();
            ResetMatrix();
            ResetSettledMatrix();
            SpawnNewBlock();
        }
        private void BtnExitClicked(object sender, EventArgs e)
        {
            FormInstances.tetrisMenu.Show();
            FormInstances.tetrisMenu.UpdateHighscoreList();
            FormInstances.tetrisForm.Hide();
        }
        private void TetrisFormClosed(object sender, FormClosedEventArgs e)
        {
            FormInstances.tetrisMenu.Close();
        }
        public void SetLevel(int level)
        {
            _level = level;
            StartLevel = level;
            SetGameSpeed();
            lblLevel.Text = _level.ToString();
        }
        private void TetrisFormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                _paused = true;
                lblPaused.Visible = true;
                btnPause.Text = "Resume";
            }
        }
        private bool IsHighscore()
        {
            int count = 0;
            bool output = false;
            for (int i = 0; i < Collections.Playerscores.Count; i++)
            {
                if (Collections.Playerscores[i].StartLevel == StartLevel)
                {
                    count++;
                    if (count <= 5 && Score > Collections.Playerscores[i].Score)
                    {
                        output = true;
                    }
                }
            }
            if (count<5&&Score>0)
            {
                output = true;
            }
            return output;
        }
    }
}
