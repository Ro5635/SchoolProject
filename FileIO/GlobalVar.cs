using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace FileIO
{

    public  class GlobalVar
    {

        
        static string FolderNameData =  "hi world";

        int AgeData = Properties.Settings.Default.Age;
        string NameData = Properties.Settings.Default.Name;
        string SNameData = Properties.Settings.Default.SName;

        Boolean ServosUsedData = Properties.Settings.Default.ServosUsed;
        Boolean ServoMic1Data = Properties.Settings.Default.ServoMic1;
        Boolean ServoMic2Data = Properties.Settings.Default.ServoMic2;
        int SerialBaudRateData = Properties.Settings.Default.SerialBaudRateHOLD;

        int ArduinoBoradData = Properties.Settings.Default.ArduinoBoradData;

        string SerialPortsOpenData = Properties.Settings.Default.SerialPortsOpenData;
        string[,] SerialPortsActiveRobotsData = new string[100, 2];//= Properties.Settings.Default.SerialPortsActiveRobotsData;

        //Primary Serial Port
        string PrimarySerialPortNameData = Properties.Settings.Default.PrimarySerialPortName;
        int PrimarySerialPortBaudData = Properties.Settings.Default.PrimaySerialPortBaud;


        int FavNumData;
        int DOBMonthData;


        // Non User Dependent, System Defined ----------------

        //Primary Serial:
        public string PrimarySerialPortName
        {
            get { return Properties.Settings.Default.PrimarySerialPortName;  }
            set { Properties.Settings.Default.PrimarySerialPortName = value; }
        }

        public int PrimarySerialPortBaud
        {
            get { return Properties.Settings.Default.PrimaySerialPortBaud; }
            set { Properties.Settings.Default.PrimaySerialPortBaud = value; }
        }

        //End Primary Serial


        public string SerialPortsOpen
        {
            get { return SerialPortsOpenData; }
            set { SerialPortsOpenData = value; }
        }

        public string SerialPortsActiveRobots(int X, int DataID)
        {
              return SerialPortsActiveRobotsData[X, DataID]; 
        }

        public void SerialPortsActiveRobotsSetValue(int X, int DataID ,string value)
        {
             SerialPortsActiveRobotsData[X, DataID] = value;
        }

        //End Non User Dependent, System Defined ----------------

        public static string FolderName
        {
            get { return FolderNameData; }
            set { FolderNameData = value; }
        }

        //New design from here
        public Boolean ServosUsed
        {
            get { return ServosUsedData; }
            set { ServosUsedData = value; }
        }

        public Boolean ServoMic1
        {
            get { return ServoMic1Data; }
            set { ServoMic1Data = value; }
        }

        public Boolean ServoMic2
        {
            get { return ServoMic2Data; }
            set { ServoMic2Data = value; }
        }

        public int SerialBaudRate
        {
            get { return SerialBaudRateData; }
            set { SerialBaudRateData = value; }
        }

        public int ArduinoBorad
        {
            get { return ArduinoBoradData; }
            set { ArduinoBoradData = value; }
        }

        





        public int Age
        {
            get { return AgeData; }
            set { AgeData = value; }
        }

        public string Name
        {
            get { return NameData; }
            set { NameData = value; }
        }

        public string SName
        {
            get { return SNameData; }
            set { SNameData = value; }
        }

        public int FavNum
        {
            get { return FavNumData; }
            set { FavNumData = value; }
        }

        public int DOBMonth
        {
            get { return DOBMonthData; }
            set { DOBMonthData = value; }
        }


        //Save the values to the Settings location
        public void SaveReadValues()
        {
            Properties.Settings.Default.SerialBaudRateHOLD = SerialBaudRateData;
            Properties.Settings.Default.ServosUsed = ServosUsedData;
            Properties.Settings.Default.ArduinoBoradData = ArduinoBoradData;
            Properties.Settings.Default.ServoMic1 = ServoMic1Data;
            Properties.Settings.Default.ServoMic2 = ServoMic2Data;


            Properties.Settings.Default.SName = SNameData;

        }

        public string[] DumpAllValues()
        {//This method has been mostly redacted.
            //Add all of the variables to this
          string[] ValueCompile = {AgeData.ToString() , NameData,SNameData                          
                                    } ;
          return ValueCompile;

        }
    
    }


    public static class Extensions
    {
        public static void SetProperty(this object obj, string propertyName, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return;
            propertyInfo.SetValue(obj, value);
        }

    }



}
