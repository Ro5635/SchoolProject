namespace FileIO
{
    partial class TaskSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskSelection));
            this.ButtonControl = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonCreateOpt = new System.Windows.Forms.Button();
            this.ButtonExtraHelp = new System.Windows.Forms.Button();
            this.FormExtendTimmer = new System.Windows.Forms.Timer(this.components);
            this.AdditionHelpBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ButtonControl
            // 
            this.ButtonControl.Location = new System.Drawing.Point(295, 65);
            this.ButtonControl.Name = "ButtonControl";
            this.ButtonControl.Size = new System.Drawing.Size(123, 109);
            this.ButtonControl.TabIndex = 1;
            this.ButtonControl.Text = "Control";
            this.ButtonControl.UseVisualStyleBackColor = true;
            this.ButtonControl.Click += new System.EventHandler(this.ButtonControl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Task Selection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select the function that you wish to compleate.";
            // 
            // ButtonCreateOpt
            // 
            this.ButtonCreateOpt.Location = new System.Drawing.Point(90, 65);
            this.ButtonCreateOpt.Name = "ButtonCreateOpt";
            this.ButtonCreateOpt.Size = new System.Drawing.Size(123, 109);
            this.ButtonCreateOpt.TabIndex = 4;
            this.ButtonCreateOpt.Text = "Create";
            this.ButtonCreateOpt.UseVisualStyleBackColor = true;
            this.ButtonCreateOpt.Click += new System.EventHandler(this.ButtonCreateOpt_Click);
            // 
            // ButtonExtraHelp
            // 
            this.ButtonExtraHelp.Location = new System.Drawing.Point(242, 162);
            this.ButtonExtraHelp.Name = "ButtonExtraHelp";
            this.ButtonExtraHelp.Size = new System.Drawing.Size(24, 26);
            this.ButtonExtraHelp.TabIndex = 5;
            this.ButtonExtraHelp.Text = "?";
            this.ButtonExtraHelp.UseVisualStyleBackColor = true;
            this.ButtonExtraHelp.Click += new System.EventHandler(this.ButtonExtraHelp_Click);
            // 
            // FormExtendTimmer
            // 
            this.FormExtendTimmer.Interval = 2;
            this.FormExtendTimmer.Tick += new System.EventHandler(this.FormExtendTimmer_Tick);
            // 
            // AdditionHelpBox
            // 
            this.AdditionHelpBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.AdditionHelpBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AdditionHelpBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdditionHelpBox.Location = new System.Drawing.Point(44, 204);
            this.AdditionHelpBox.Name = "AdditionHelpBox";
            this.AdditionHelpBox.Size = new System.Drawing.Size(412, 163);
            this.AdditionHelpBox.TabIndex = 6;
            this.AdditionHelpBox.Text = resources.GetString("AdditionHelpBox.Text");
            // 
            // TaskSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(500, 288);
            this.ControlBox = false;
            this.Controls.Add(this.AdditionHelpBox);
            this.Controls.Add(this.ButtonExtraHelp);
            this.Controls.Add(this.ButtonCreateOpt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "TaskSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskSelection";
            this.Load += new System.EventHandler(this.TaskSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonCreateOpt;
        private System.Windows.Forms.Button ButtonExtraHelp;
        private System.Windows.Forms.Timer FormExtendTimmer;
        private System.Windows.Forms.RichTextBox AdditionHelpBox;
    }
}