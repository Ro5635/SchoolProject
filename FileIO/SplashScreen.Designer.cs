namespace FileIO
{
    partial class SplashScreen
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
            this.components = new System.ComponentModel.Container();
            this.LogHolder = new System.Windows.Forms.PictureBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.SplashScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.ExtendFormAnimationTimmer = new System.Windows.Forms.Timer(this.components);
            this.LoadProgressBar = new System.Windows.Forms.ProgressBar();
            this.SerialListenDelay = new System.Windows.Forms.Timer(this.components);
            this.ProggressUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LogHolder)).BeginInit();
            this.SuspendLayout();
            // 
            // LogHolder
            // 
            this.LogHolder.Image = global::FileIO.Properties.Resources.ArduinoControlLogo;
            this.LogHolder.Location = new System.Drawing.Point(0, 0);
            this.LogHolder.Name = "LogHolder";
            this.LogHolder.Size = new System.Drawing.Size(419, 129);
            this.LogHolder.TabIndex = 0;
            this.LogHolder.TabStop = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(105, 132);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(200, 24);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Loading Please Wait";
            // 
            // SplashScreenTimer
            // 
            this.SplashScreenTimer.Enabled = true;
            this.SplashScreenTimer.Tick += new System.EventHandler(this.SplashScreenTimer_Tick);
            // 
            // ExtendFormAnimationTimmer
            // 
            this.ExtendFormAnimationTimmer.Interval = 10;
            this.ExtendFormAnimationTimmer.Tick += new System.EventHandler(this.ExtendFormAnimationTimmer_Tick);
            // 
            // LoadProgressBar
            // 
            this.LoadProgressBar.Location = new System.Drawing.Point(54, 159);
            this.LoadProgressBar.MarqueeAnimationSpeed = 500;
            this.LoadProgressBar.Maximum = 10;
            this.LoadProgressBar.Name = "LoadProgressBar";
            this.LoadProgressBar.Size = new System.Drawing.Size(296, 23);
            this.LoadProgressBar.Step = 50;
            this.LoadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.LoadProgressBar.TabIndex = 2;
            // 
            // SerialListenDelay
            // 
            this.SerialListenDelay.Tick += new System.EventHandler(this.SerialListenDelay_Tick);
            // 
            // ProggressUpdate
            // 
            this.ProggressUpdate.Interval = 800;
            this.ProggressUpdate.Tick += new System.EventHandler(this.ProggressUpdate_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(417, 325);
            this.ControlBox = false;
            this.Controls.Add(this.LoadProgressBar);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.LogHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arduino Control System";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogHolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LogHolder;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Timer SplashScreenTimer;
        private System.Windows.Forms.Timer ExtendFormAnimationTimmer;
        private System.Windows.Forms.ProgressBar LoadProgressBar;
        private System.Windows.Forms.Timer SerialListenDelay;
        private System.Windows.Forms.Timer ProggressUpdate;
    }
}