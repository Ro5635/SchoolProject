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
    public partial class SplashScreen : Form
    {
       int timmercounts = 0;
       int TimeSerialListenDelayCounts = 0;
       int LastPortsScannedCount = 0;

        public SplashScreen()
        {
            InitializeComponent();
            //Set initial size of the form
            Height = 127;
            
            // increment the run ID by one.
            int SettingRunID = Properties.Settings.Default.RunID;
            SettingRunID = SettingRunID + 1;
            Properties.Settings.Default.RunID = SettingRunID;
            //Now Run ID is correct create the new logs
            LogAndErrorFiles ProgramFiles = new LogAndErrorFiles();

            //Create Files for start routine:
            ProgramFiles.InitialiseFiles("SplashScreenStartEvents");
            ProgramFiles.WriteLogFile("Files Created Successfully");

            // increment the process bar by one. 
            //LoadProgressBar.Increment(1);

            //Initilise the serial handler




            //The bellow block finds the number of increments needed in the progress bar.
                //Initialize the serial handler
                SerialControl SerialSplashHandle = new SerialControl();
                //Scan the ports available
                string SerialPortsActive = SerialSplashHandle.SerialPortScan();
                //Find the number available
                int NumberOfPorts = SerialPortsActive.Split('#').Length;
                //calculate number of scans necessary
                int ScansRequired = NumberOfPorts * 9;
                LoadProgressBar.Maximum = ScansRequired + 1;
            //The Reason for the plus one is a work around to allow serial scan to finish before being closed, saves having to add a close condition.

                



        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            SplashScreenTimer.Enabled = true;
            
            

        }

        private void ExtendFormAnimationTimmer_Tick(object sender, EventArgs e)
        {
            if (Height < 200)
            {
                Height  = Height  + 1;
            }
            else if (Height >= 200)
            {
                //start a new thread for the robot search so not to freeze the GUI
                Thread FindRobotThread = new Thread(NewThreadFindRobot);
                FindRobotThread.Start();
                ProggressUpdate.Enabled = true;
                
                ExtendFormAnimationTimmer.Enabled = false;
            }

        }



        static void NewThreadFindRobot()
        {
            //set scanned ports to 0 (used to update progress bar
            Properties.Settings.Default.PortsScanned = 0;
            SerialControl SerialSplashHandle = new SerialControl();
            SerialSplashHandle.FindSerialRobot();
        }

        private void ProggressUpdate_Tick(object sender, EventArgs e)
        {
            //Keep the progress bar updated
            if (LastPortsScannedCount < Properties.Settings.Default.PortsScanned)
            {
                LastPortsScannedCount = Properties.Settings.Default.PortsScanned;
                LoadProgressBar.Increment(1);
                //Bellow closes form and opens next if all scans complete
                if (LoadProgressBar.Value == LoadProgressBar.Maximum)
                {
                    this.Close();
                    ExtendFormAnimationTimmer.Enabled = false;
                    SplashScreenTimer.Enabled = false;
                    ProggressUpdate.Enabled = false;
                }
            }
        }


        private void SplashScreenTimer_Tick(object sender, EventArgs e)
        {
            timmercounts = timmercounts +  1;
            if (timmercounts == 10)
            {
                ExtendFormAnimationTimmer.Enabled = true;
                SplashScreenTimer.Enabled = false;
            }
            
            
        }

        private void SerialListenDelay_Tick(object sender, EventArgs e)
        {
            TimeSerialListenDelayCounts = TimeSerialListenDelayCounts + 1;
        }


    }
}
