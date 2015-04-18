using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileIO
{

    
    public partial class Create : Form
    {
        
        public Create()
        {
            InitializeComponent();
        }

        private void RichTextBoxIntro_TextChanged(object sender, EventArgs e)
        {

        }

        private void Create_Load(object sender, EventArgs e)
        {
            PannelNum2.Visible = false;
            PannelNum3.Visible = false;
        }

        private void PannelNum2_VisibleChanged(object sender, EventArgs e)
        {
         
        }

        [STAThread]
        private void ButtonStart_Click(object sender, EventArgs e)
        {


            Thread OpenThread = new Thread(OpenFileLoc);
            OpenThread.SetApartmentState(ApartmentState.STA);
            OpenThread.Start();

            PannelNum2.Visible = true;
            PannelNum1.Visible = false;
            this.PannelNum2.Location = new System.Drawing.Point(0, 100);
            this.PannelNum2.Size = new System.Drawing.Size(1010, 503);
           
        }
        [STAThread]
        private void OpenFileLoc()
        {
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.Filter = "Robot Files (*.robotfile)|*.robotfile";
            saveFileDialog2.Title = "Save location";
            saveFileDialog2.ShowDialog();
            Properties.Settings.Default.SaveRobotLoacation = saveFileDialog2.FileName;
            //Clear the file down
            WriteToFile("", false);//This blanks the text file
            
        }

        private void PannelNum2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonBoardUNO_Click(object sender, EventArgs e)
        {
            if (CheckSaveLocCompleate())
            {
                WriteToFile("5^0");//Write that it is an arduino UNO, NB it is predefined by me in 
                //outher documents that an Aduino UNO is defied by variable 5 and 0.
                DisplayPanNum3();
            }
        }

        private Boolean CheckSaveLocCompleate()
        {
            //This function ensures that the save location has been compleated.
            if (Properties.Settings.Default.SaveRobotLoacation == "" || Properties.Settings.Default.SaveRobotLoacation == "N/A")
            {
                MessageBox.Show("Please Select a Save location in the Save Dialogue that is Open");
                return false;//Save file not compleated.
                   
            }else
            {
            return true;//Save file location compleate.
            }

        }

        private void WriteToFile(string LineToWrite, Boolean Append = true)
        {
            string WriteToRobotFile = Properties.Settings.Default.SaveRobotLoacation;
            //This method allows writing to the file.
            
            //Ensure that the File Location has been filled in.
            //Write to the file

                if (Append)
                { //append or over write

                    try
                    {
                        using (StreamWriter WriteHandle = new StreamWriter(WriteToRobotFile, true))
                        {
                            WriteHandle.WriteLine(LineToWrite);
                            }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be Written to:");
                        Console.WriteLine(e.Message);
                        }

                }
                else
                {

                    try
                    {
                        using (StreamWriter WriteHandle = new StreamWriter(WriteToRobotFile, false)) //Overwrite the file
                        {
                            WriteHandle.WriteLine(LineToWrite);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be Written to:");
                        Console.WriteLine(e.Message);
                    }


                
            }
        }

        private void button2_Click(object sender, EventArgs e)//Yes, I forgot to rename the button.
        {
            if (CheckSaveLocCompleate())
            {
                WriteToFile("5^2");//Write that it is an arduino MINI, NB it is predefined by me in 
                //outher documents that an Aduino MINI is defied by variable 5 and 2.
                DisplayPanNum3();
            }
        }

        private void button3_Click(object sender, EventArgs e)//Yes, I forgot to rename the button.
        {
            if (CheckSaveLocCompleate())
            {
                WriteToFile("5^1");//Write that it is an arduino Mega, NB it is predefined by me in 
                //outher documents that an Aduino Mega is defied by variable 5 and 1.
                DisplayPanNum3();
            }
        }

        private void DisplayPanNum3()
        {
            PannelNum3.Visible = true;
            this.PannelNum3.Location = new System.Drawing.Point(0, 100);
            this.PannelNum3.Size = new System.Drawing.Size(1010, 503);
            PannelNum2.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void ButtonContinue_Click(object sender, EventArgs e)
        {
            //Submit the form changes as apptopiate

            //Compleate For A first:
            
           
            
        }

    }
}
