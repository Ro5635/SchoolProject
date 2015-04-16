using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileIO
{
    public partial class TaskSelection : Form
    {
        
        public TaskSelection()
        {
            InitializeComponent();
        }

        private void ButtonControl_Click(object sender, EventArgs e)
        {
            Thread OpenThread = new Thread(OpenControlForm);
            OpenThread.Start();

            this.Close();
        }


        private void OpenControlForm()
        {
            Application.Run(new Control());
            
        }

        private void OpenCreateForm()
        {
            Application.Run(new Create());

        }


        private void ButtonCreateOpt_Click(object sender, EventArgs e)
        {
            Thread OpenThread2 = new Thread(OpenCreateForm);
            OpenThread2.Start();
            this.Close();
        }

        private void ButtonExtraHelp_Click(object sender, EventArgs e)
        {
            FormExtendTimmer.Enabled = true;
        }

        private void WindowExtend(int Extension)
        {
            if (Height < Extension)
            { // if the window is less that the stop extending point.
                Height++;
            }
            }

        private void FormExtendTimmer_Tick(object sender, EventArgs e)
        {
            WindowExtend(360);
        }

        private void TaskSelection_Load(object sender, EventArgs e)
        {
            Height = 200; //Set the Height at the form on load.
        }

        }
    
}
