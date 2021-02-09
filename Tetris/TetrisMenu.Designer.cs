
namespace Tetris
{
    partial class TetrisMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlay = new Tetris.Models.NonFocusButton();
            this.menuLevel = new System.Windows.Forms.ComboBox();
            this.lstHighScores = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(88, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tetris";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Black;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(88, 297);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(144, 53);
            this.btnPlay.TabIndex = 13;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlayPressed);
            // 
            // menuLevel
            // 
            this.menuLevel.Font = new System.Drawing.Font("Bahnschrift", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuLevel.FormattingEnabled = true;
            this.menuLevel.Items.AddRange(new object[] {
            "Level 0",
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5",
            "Level 6",
            "Level 7",
            "Level 8",
            "Level 9"});
            this.menuLevel.Location = new System.Drawing.Point(88, 254);
            this.menuLevel.Name = "menuLevel";
            this.menuLevel.Size = new System.Drawing.Size(144, 26);
            this.menuLevel.TabIndex = 14;
            this.menuLevel.Text = "Level 0";
            this.menuLevel.SelectedIndexChanged += new System.EventHandler(this.MenuLevelSelection);
            // 
            // lstHighScores
            // 
            this.lstHighScores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstHighScores.Font = new System.Drawing.Font("Bahnschrift SemiLight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstHighScores.FormattingEnabled = true;
            this.lstHighScores.ItemHeight = 16;
            this.lstHighScores.Location = new System.Drawing.Point(88, 116);
            this.lstHighScores.Name = "lstHighScores";
            this.lstHighScores.Size = new System.Drawing.Size(144, 112);
            this.lstHighScores.TabIndex = 15;
            this.lstHighScores.UseTabStops = false;
            // 
            // TetrisMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(325, 373);
            this.Controls.Add(this.lstHighScores);
            this.Controls.Add(this.menuLevel);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label1);
            this.Name = "TetrisMenu";
            this.Text = "Tetris";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TetrisMenuClosed);
            this.Load += new System.EventHandler(this.TetrisMenu_Load);
            this.Shown += new System.EventHandler(this.TetrisMenuShown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Models.NonFocusButton btnPlay;
        private System.Windows.Forms.ComboBox menuLevel;
        private System.Windows.Forms.ListBox lstHighScores;
    }
}