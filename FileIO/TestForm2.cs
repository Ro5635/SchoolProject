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
    public partial class TestForm2 : Form
    {

        


        public TestForm2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opentext = new OpenFileDialog();
            opentext.Multiselect = false;
            opentext.Filter = "Robot File|*.robot";
            opentext.ShowDialog();
            string x = opentext.FileName;
            if (x != "Error")
            {
                UserSettingsHandle LoadRobot = new UserSettingsHandle();
                LoadRobot.LoadUserSettings(x);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVar GlobalVarablesHandle = new GlobalVar();
            if (GlobalVarablesHandle.Age.ToString() != null)
            {
                label2.Text = GlobalVarablesHandle.Age.ToString();
            }

        }

        private void TestForm2_Load(object sender, EventArgs e)
        {

        }
        GlobalVar GLobalsAccess = new GlobalVar();
        private void button3_Click(object sender, EventArgs e)
        {
            GLobalsAccess.PrimarySerialPortName = "Apple";
            label3.Text = GLobalsAccess.PrimarySerialPortName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GLobalsAccess.PrimarySerialPortName = "Pineapples";
            label3.Text = GLobalsAccess.PrimarySerialPortName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label3.Text = GLobalsAccess.PrimarySerialPortName;
        }
    }
}
