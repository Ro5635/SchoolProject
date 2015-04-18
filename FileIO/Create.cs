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


    public partial class Create : Form
    {

        Boolean Pan1 = true;
        Boolean Pan2 = false;
    
        public Create()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                        if (Pan1) { 
                        panel1.Visible = false;
                        Pan1 = false;
                         }else{
                     panel1.Visible = true;
                     Pan1 = true;
                         }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Pan2)
            {
                panel2.Visible = false;
                Pan2 = false;
            }
            else
            {
                Pan2 = true;
                panel2.Visible = true;
            }
        }
    }
}
