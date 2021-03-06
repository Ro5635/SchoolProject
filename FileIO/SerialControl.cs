﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FileIO
{
    /*
     * This class will control all serial IO in the system, create an object instance of this class for each serial port.
     * Note, this requires the filehandaling.cs in order for the error reporting to operate as expected. filehandaling.cs is called with automatic
     * write to c/Deleateme/
     * */

    class SerialControl 
    {


        //Are the Logs Created (need initializing?)?
        // Create the Error Reporter object
        LogAndErrorFiles ErrorReporter = new LogAndErrorFiles();
        //Create the Serial port
        System.IO.Ports.SerialPort NewSerialPort ;

        #region SerialPortScan
        // returns the list of serial ports separated by '#'
        public string SerialPortScan(){
        
            try
        {
            string PortsAvalible = "";
        foreach (string port in System.IO.Ports.SerialPort.GetPortNames()) {
            PortsAvalible = PortsAvalible + "#" +  port;
        }//End For each
        //strip the first '#' from the string so not to get port "" later.
        PortsAvalible = PortsAvalible.TrimStart('#');
                
        return PortsAvalible;

            }

            catch (Exception ErrorText)

            {

                ErrorReporter.ErrorHandaling("Failed to Scan the available Serial Ports", ErrorText.ToString(),"SerialControl");

            }//End Try catch group

            return "Error";


        }//End SerialPortScan

        #endregion SerialPortScan


        #region Serial Port Open
        public void OpenSerialPort(string PortName,int BaudRate)
        {
                try
                {
                    CloseSerialPort();
                    NewSerialPort =  new System.IO.Ports.SerialPort()
                    {
                        PortName = PortName,
                        BaudRate = BaudRate,
                        ReadTimeout = 5000,  //Time out to avoid hang when reviving constant data stream.
                    };
                    NewSerialPort.Open();
                    
                }
                catch (Exception ErrorText)
                {
                    //Write the error to the Error Log
                    ErrorReporter.ErrorHandaling("Error in creating the serial port, port:" + PortName + "      Baud Rate:" + BaudRate  +"  Error:" ,ErrorText.ToString(),"SerialControl");
                }//End Try Catch group


        }// End Open Serial Port method
        #endregion Serial Port Open
        
        #region Serial Port Close
        public void CloseSerialPort()
        {
            try
            {
                NewSerialPort.Close();
            }
            catch (Exception ErrorText)
            {
                ErrorReporter.ErrorHandaling("Failed to close the serial port, Possibly already closed?", ErrorText.ToString(),"SerialControl");
            }//End Try catch group
            
        }//End CloseSerialPort

        #endregion Serial Port Close

        #region Serial Port Write
        //THis will write data over the serial port
        public void WriteSerialData(string WriteThis)
        {
            try
            {
                NewSerialPort.WriteLine("^" + WriteThis + "^");
            }
            catch (Exception ErrorText)
            {
                //Write to the Error Log
                ErrorReporter.ErrorHandaling("Unable to write to the Serial Port, Note data to be written was: " + WriteThis, ErrorText.ToString(),"SerialControl");
            }//End Try catch group

        }//End WriteSerialData Method

        #endregion Serial Port Write

        #region Serial Port Read
        //execute read and then check the recived data, this will use the read raw serial data routine for the mechanics of aquiring the data. 
        GlobalVar GlobalsAccessHandle = new GlobalVar();
        public string ReadSerialData()
        {
            //read raw data
            
            
           
        
            

            Console.WriteLine("Data From the Global Vars: " + GlobalsAccessHandle.SerialPortsActiveRobots(0, 0) + " : " +  Convert.ToInt32(GlobalsAccessHandle.SerialPortsActiveRobots(0, 1)));
            String RawData = ReadSerialDataRaw();
            String TrimmedData = RawData.Trim(new Char[] { '^', '\r' });
            //check the data
            return TrimmedData;// this is the raw data with the enclosing ^ and new line charactors removed.
        }
        
        


        //read the port

        private string ReadSerialDataRaw()
        {
            try
            {
                if (NewSerialPort.BytesToRead > 0)
                {
                    return NewSerialPort.ReadLine();
                }
                else
                {
                    ErrorReporter.ErrorHandaling("Warning: Attempted to read an empty serial in buffer, port:", NewSerialPort.PortName.ToString(),"SerialControl");
                    return "";
                }

             }
            catch (Exception ErrorText)
            {
                //Write the error to the Error Log
                ErrorReporter.ErrorHandaling("Error in attempting to read the serial port",ErrorText.ToString(),"SerialControl");
                return "ReadError";
            }
        } //End ReadSerialData




        #endregion Serial Port Read

        #region Find Serial Robot
        public void FindSerialRobot()
        {
            LogAndErrorFiles ProgramFiles = new LogAndErrorFiles();

            int[] BaudRateAttempt = { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 230400 };
            string SerialPortsActive = SerialPortScan();

            if (SerialPortsActive != null || SerialPortsActive == "")
            {
                ProgramFiles.WriteLogFile("Serial Ports Scanned, Ports: " + SerialPortsActive + " avalable.");

                //Write the avalable ports to the var
                GlobalVar GlobalsAccess = new GlobalVar();
                GlobalsAccess.SetProperty("SerialPortsOpen", SerialPortsActive);
                ProgramFiles.WriteLogFile("Written to GlobalVars");
            }




            string[] PortsAvalableArray = SerialPortsActive.Split('#');
           

            GlobalVar GlobalsAccessHandle = new GlobalVar ();

            int FoundPortsArrayLocation = 0;

            foreach (string port in PortsAvalableArray)
            {

                foreach (int BaudScan in BaudRateAttempt)
                {
                    try
                    {
                        OpenSerialPort(port.ToString(), BaudScan);
                        // clear the Arduino's buffer from previus likley garbage with leading ^.
                        WriteSerialData("^Robot?^");

                        //Increment bellow so to update the progress bar on the splash screen
                        Properties.Settings.Default.PortsScanned = Properties.Settings.Default.PortsScanned + 1;

                        Thread.Sleep(1000); // nasty sleep delay, this actualy stops entire thread execution (this thread only, Splash GUI is in seperate thread)

                        string DataIn = ReadSerialDataRaw();
                        if (DataIn == "^Arduino_Robot^\r")
                        {
                            ProgramFiles.WriteLogFile("SUCCESS FOUND A Robot " + port.ToString() + "  " + BaudScan );
                            GlobalsAccessHandle.SerialPortsActiveRobotsSetValue(FoundPortsArrayLocation, 0 ,port.ToString() );
                            GlobalsAccessHandle.SerialPortsActiveRobotsSetValue(FoundPortsArrayLocation, 1, BaudScan.ToString());
                            //If it is the first Serial port foud save it to the primary serial, this is saved for the length of the program session.
                            if (FoundPortsArrayLocation == 0)
                            {
                                GlobalsAccessHandle.PrimarySerialPortBaud = BaudScan;
                                GlobalsAccessHandle.PrimarySerialPortName =  port.ToString();
                            }
                            FoundPortsArrayLocation++;
                            Console.WriteLine("Success Arduino found: " + BaudScan + " " + port.ToString() + "   " +  DataIn.ToString());
                            Console.WriteLine("Data from GlobalVars: " + GlobalsAccessHandle.SerialPortsActiveRobots(0, 0) + " 2: " + GlobalsAccessHandle.SerialPortsActiveRobots(0,1));
                            
                        }
                        CloseSerialPort();

                    }
                    catch (Exception ErrorText)
                    {
                        ProgramFiles.WriteLogFile("port failed scan: " + port + " " + BaudScan + ErrorText);
                        CloseSerialPort();
                    }
                }

                ProgramFiles.WriteLogFile("All ports scanned.");
                Console.WriteLine("All ports scanned. " + "The Primary Serial Port is: " + GlobalsAccessHandle.PrimarySerialPortName + " Baud: " + GlobalsAccessHandle.PrimarySerialPortBaud);
                //Now start chain of events to close the splash screen.
                Properties.Settings.Default.PortsScanned = Properties.Settings.Default.PortsScanned + 1;
            }
        }//End find serial robot
        #endregion Find Serial Robot


        #region Erorr Handaling
        //This section has been centralised

        #endregion Erorr Handaling

    }
}


