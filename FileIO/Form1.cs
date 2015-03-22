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
    public partial class Form1 : Form  
    {
        
        LogAndErrorFiles LogErrorHandle = new LogAndErrorFiles();
        SerialControl SerialPortA = new SerialControl();
        SerialControl SerialPortB = new SerialControl();

        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
















            //LogErrorHandle.InitialiseFiles("FormA");
           // MessageBox.Show(Properties.Settings.Default.RunID.ToString());

            SerialPortA.OpenSerialPort("COM6", 115200);
           // SerialPortB.OpenSerialPort("COM4", 9600);
            
            


            GlobalVar test = new GlobalVar();
           //MessageBox.Show(test.FolderName.ToString());
           //test.FolderName = "hi";

            
            
            string FreePorts = SerialPortA.SerialPortScan();
            string[] FreeportsArray = FreePorts.Split('#');
            foreach (string x in FreeportsArray)
            {
                comboBox1.Items.Add(x);
            }

            

            



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string hold = SerialPortA.ReadSerialData();
            //if (hold.Length > 0){
              //  richTextBox1.Text = hold;
            //}
            //string hold2 = SerialPortB.ReadSerialData();
            //if (hold2.Length > 0)
            //{
             //   richTextBox2.Text = hold2;
            //}

            



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog  opentext = new OpenFileDialog();
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
            SerialPortA.WriteSerialData(richTextBox3.Text.ToString());
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GlobalVar GlobalVaribles = new GlobalVar();
            if (GlobalVaribles.Name.ToString() != null)
            {
                textBox1.Text = GlobalVaribles.Age.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TestForm2 show = new TestForm2();
            show.Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            GlobalVar GLobals = new GlobalVar();
            
            Console.WriteLine("Break ");


        }

        private void button6_Click(object sender, EventArgs e)
        {


            SerialConnectionControl SerialConn = new SerialConnectionControl();

            String[] PacketRecived = SerialConn.ReadData();
            label2.Text = PacketRecived[0];
            label3.Text = PacketRecived[1];

        }

    
    
    }
}
