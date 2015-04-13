using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Application.Run(new Control());
            this.Close();
        }

        private void ButtonCreateOpt_Click(object sender, EventArgs e)
        {
            Application.Run(new Create());
            this.Close();
        }
    }
}
