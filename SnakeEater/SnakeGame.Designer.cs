namespace SnakeEater
{
    partial class SnakeGame
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.txtFoodCount = new System.Windows.Forms.TextBox();
            this.lblFoodCount = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlGameZone = new System.Windows.Forms.Panel();
            this.pboxGameZone = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenu_Game = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_Pause = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_UpdLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrSpeedCtrl = new SnakeEater.MyControl.UTimer(this.components);
            this.tmrForward = new SnakeEater.MyControl.UTimer(this.components);
            this.tmrCostTime = new SnakeEater.MyControl.UTimer(this.components);
            this.grpInfo.SuspendLayout();
            this.pnlGameZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxGameZone)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.txtScore);
            this.grpInfo.Controls.Add(this.lblScore);
            this.grpInfo.Controls.Add(this.txtFoodCount);
            this.grpInfo.Controls.Add(this.lblFoodCount);
            this.grpInfo.Controls.Add(this.txtTime);
            this.grpInfo.Controls.Add(this.lblTime);
            this.grpInfo.Location = new System.Drawing.Point(15, 33);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(640, 45);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            // 
            // txtScore
            // 
            this.txtScore.Enabled = false;
            this.txtScore.Location = new System.Drawing.Point(411, 17);
            this.txtScore.Name = "txtScore";
            this.txtScore.ReadOnly = true;
            this.txtScore.Size = new System.Drawing.Size(115, 21);
            this.txtScore.TabIndex = 1;
            this.txtScore.Text = "0";
            this.txtScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(370, 21);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(35, 12);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "Score";
            // 
            // txtFoodCount
            // 
            this.txtFoodCount.Enabled = false;
            this.txtFoodCount.Location = new System.Drawing.Point(285, 17);
            this.txtFoodCount.Name = "txtFoodCount";
            this.txtFoodCount.ReadOnly = true;
            this.txtFoodCount.Size = new System.Drawing.Size(79, 21);
            this.txtFoodCount.TabIndex = 1;
            this.txtFoodCount.Text = "0";
            this.txtFoodCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFoodCount
            // 
            this.lblFoodCount.AutoSize = true;
            this.lblFoodCount.Location = new System.Drawing.Point(208, 21);
            this.lblFoodCount.Name = "lblFoodCount";
            this.lblFoodCount.Size = new System.Drawing.Size(71, 12);
            this.lblFoodCount.TabIndex = 0;
            this.lblFoodCount.Text = "Foods Eaten";
            // 
            // txtTime
            // 
            this.txtTime.Enabled = false;
            this.txtTime.Location = new System.Drawing.Point(41, 17);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(161, 21);
            this.txtTime.TabIndex = 1;
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(6, 21);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(29, 12);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time";
            // 
            // pnlGameZone
            // 
            this.pnlGameZone.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlGameZone.Controls.Add(this.pboxGameZone);
            this.pnlGameZone.Location = new System.Drawing.Point(15, 85);
            this.pnlGameZone.Name = "pnlGameZone";
            this.pnlGameZone.Size = new System.Drawing.Size(640, 400);
            this.pnlGameZone.TabIndex = 1;
            // 
            // pboxGameZone
            // 
            this.pboxGameZone.BackColor = System.Drawing.SystemColors.GrayText;
            this.pboxGameZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pboxGameZone.Location = new System.Drawing.Point(0, 0);
            this.pboxGameZone.Name = "pboxGameZone";
            this.pboxGameZone.Size = new System.Drawing.Size(640, 400);
            this.pboxGameZone.TabIndex = 0;
            this.pboxGameZone.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_Game,
            this.ToolStripMenu_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenu_Game
            // 
            this.toolStripMenu_Game.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripMenu_Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_NewGame,
            this.toolStripMenu_Pause,
            this.toolStripMenu_Save,
            this.toolStripMenu_Exit});
            this.toolStripMenu_Game.Name = "toolStripMenu_Game";
            this.toolStripMenu_Game.ShortcutKeyDisplayString = "";
            this.toolStripMenu_Game.ShowShortcutKeys = false;
            this.toolStripMenu_Game.Size = new System.Drawing.Size(71, 21);
            this.toolStripMenu_Game.Text = "Game(&G)";
            // 
            // toolStripMenu_NewGame
            // 
            this.toolStripMenu_NewGame.Name = "toolStripMenu_NewGame";
            this.toolStripMenu_NewGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenu_NewGame.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenu_NewGame.Text = "Start a new Game";
            this.toolStripMenu_NewGame.Click += new System.EventHandler(this.toolStripMenu_NewGame_Click);
            // 
            // toolStripMenu_Pause
            // 
            this.toolStripMenu_Pause.Enabled = false;
            this.toolStripMenu_Pause.Name = "toolStripMenu_Pause";
            this.toolStripMenu_Pause.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenu_Pause.Text = "Play";
            this.toolStripMenu_Pause.Click += new System.EventHandler(this.toolStripMenu_Pause_Click);
            // 
            // toolStripMenu_Save
            // 
            this.toolStripMenu_Save.Name = "toolStripMenu_Save";
            this.toolStripMenu_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenu_Save.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenu_Save.Text = "Save";
            this.toolStripMenu_Save.Click += new System.EventHandler(this.toolStripMenu_Save_Click);
            // 
            // toolStripMenu_Exit
            // 
            this.toolStripMenu_Exit.Name = "toolStripMenu_Exit";
            this.toolStripMenu_Exit.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenu_Exit.Text = "Exit";
            this.toolStripMenu_Exit.Click += new System.EventHandler(this.toolStripMenu_Exit_Click);
            // 
            // ToolStripMenu_Help
            // 
            this.ToolStripMenu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_UpdLog,
            this.toolStripMenu_About});
            this.ToolStripMenu_Help.Name = "ToolStripMenu_Help";
            this.ToolStripMenu_Help.Size = new System.Drawing.Size(64, 21);
            this.ToolStripMenu_Help.Text = "Help(&H)";
            // 
            // toolStripMenu_UpdLog
            // 
            this.toolStripMenu_UpdLog.Name = "toolStripMenu_UpdLog";
            this.toolStripMenu_UpdLog.Size = new System.Drawing.Size(227, 22);
            this.toolStripMenu_UpdLog.Text = "Update Log";
            // 
            // toolStripMenu_About
            // 
            this.toolStripMenu_About.Name = "toolStripMenu_About";
            this.toolStripMenu_About.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenu_About.Size = new System.Drawing.Size(227, 22);
            this.toolStripMenu_About.Text = "About My Snake Eater";
            this.toolStripMenu_About.Click += new System.EventHandler(this.toolStripMenu_About_Click);
            // 
            // tmrSpeedCtrl
            // 
            this.tmrSpeedCtrl.Interval = 30000;
            this.tmrSpeedCtrl.Tick += new System.EventHandler(this.TmrSpeedCtrl_Tick);
            // 
            // tmrForward
            // 
            this.tmrForward.Interval = 500;
            this.tmrForward.Tick += new System.EventHandler(this.TmrForward_Tick);
            // 
            // tmrCostTime
            // 
            this.tmrCostTime.Tick += new System.EventHandler(this.TmrCostTime_Tick);
            // 
            // SnakeGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(670, 497);
            this.Controls.Add(this.pnlGameZone);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SnakeGame";
            this.Text = "My Snake Eater";
            this.Load += new System.EventHandler(this.SnakeGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnakeGame_KeyDown);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.pnlGameZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxGameZone)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.TextBox txtFoodCount;
        private System.Windows.Forms.Label lblFoodCount;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlGameZone;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Game;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_NewGame;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Pause;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Save;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Exit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenu_Help;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_UpdLog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_About;
        private MyControl.UTimer tmrSpeedCtrl;
        private MyControl.UTimer tmrForward;
        private System.Windows.Forms.PictureBox pboxGameZone;
        private MyControl.UTimer tmrCostTime;

    }
}

