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
 

        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
















            //LogErrorHandle.InitialiseFiles("FormA");
           // MessageBox.Show(Properties.Settings.Default.RunID.ToString());

           // SerialPortB.OpenSerialPort("COM4", 9600);
            
            


            GlobalVar test = new GlobalVar();
           //MessageBox.Show(test.FolderName.ToString());
           //test.FolderName = "hi";

      


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
        SerialConnectionControl SerialConn = new SerialConnectionControl();
        String[] PacketRecived = new string[2];
        private void button6_Click(object sender, EventArgs e)
        {


            PacketRecived[0] = "" ;
            PacketRecived[1] = "" ;

            PacketRecived = SerialConn.ReadData();
            label2.Text = PacketRecived[0];
            label3.Text = PacketRecived[1];

        }

        GlobalVar GLobalsAccess = new GlobalVar();

        private void button7_Click(object sender, EventArgs e)
        {
            GLobalsAccess.PrimarySerialPortName = "Chease";
            label4.Text = GLobalsAccess.PrimarySerialPortName;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GLobalsAccess.PrimarySerialPortName = "Apple";
            label4.Text = GLobalsAccess.PrimarySerialPortName;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label4.Text = GLobalsAccess.PrimarySerialPortName;
        }

        SerialControl SerialTest = new SerialControl();
        private void button10_Click(object sender, EventArgs e)
        {
            SerialTest.OpenSerialPort("COM6", 9600);
            textBox2.Text = SerialTest.ReadSerialData();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
        }

    
    
    }
}
