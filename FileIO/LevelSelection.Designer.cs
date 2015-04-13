namespace FileIO
{
    partial class LevelSelection
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
            this.ButtonBasicOpt = new System.Windows.Forms.Button();
            this.ButtonAdvancedOpt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonBasicOpt
            // 
            this.ButtonBasicOpt.Location = new System.Drawing.Point(99, 56);
            this.ButtonBasicOpt.Name = "ButtonBasicOpt";
            this.ButtonBasicOpt.Size = new System.Drawing.Size(123, 109);
            this.ButtonBasicOpt.TabIndex = 0;
            this.ButtonBasicOpt.Text = "Basic";
            this.ButtonBasicOpt.UseVisualStyleBackColor = true;
            this.ButtonBasicOpt.Click += new System.EventHandler(this.ButtonBasicOpt_Click);
            // 
            // ButtonAdvancedOpt
            // 
            this.ButtonAdvancedOpt.Location = new System.Drawing.Point(292, 56);
            this.ButtonAdvancedOpt.Name = "ButtonAdvancedOpt";
            this.ButtonAdvancedOpt.Size = new System.Drawing.Size(123, 109);
            this.ButtonAdvancedOpt.TabIndex = 1;
            this.ButtonAdvancedOpt.Text = "Advanced";
            this.ButtonAdvancedOpt.UseVisualStyleBackColor = true;
            this.ButtonAdvancedOpt.Click += new System.EventHandler(this.ButtonAdvancedOpt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(101, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please Select an level to use the system at:";
            // 
            // LevelSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonAdvancedOpt);
            this.Controls.Add(this.ButtonBasicOpt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(500, 200);
            this.Name = "LevelSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonBasicOpt;
        private System.Windows.Forms.Button ButtonAdvancedOpt;
        private System.Windows.Forms.Label label1;
    }
}