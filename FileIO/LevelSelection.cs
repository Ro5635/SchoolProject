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
    public partial class LevelSelection : Form
    {
        public LevelSelection()
        {
            InitializeComponent();
        }

        private void ButtonBasicOpt_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserLevelSetting = 0;
            this.Close(); //CLose the window
        }

        private void ButtonAdvancedOpt_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserLevelSetting = 1;
            this.Close(); //CLose the window
        }
    }
}
