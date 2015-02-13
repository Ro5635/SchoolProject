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

        string SerialPortsOpenData = Properties.Settings.Default.SerialPortsOpenData;
        string[,] SerialPortsActiveRobotsData = new string[10,3];//= Properties.Settings.Default.SerialPortsActiveRobotsData;

        int FavNumData;
        int DOBMonthData;


        // Non User Dependent, System Defined

        public string SerialPortsOpen
        {
            get { return SerialPortsOpenData; }
            set { SerialPortsOpenData = value; }
        }

        public string SerialPortsActiveRobots(int X,int Y)
        {
              return SerialPortsActiveRobotsData[X,Y]; 
        }

        public void SerialPortsActiveRobotsSetValue(int X, int Y, string value)
        {
             SerialPortsActiveRobotsData[X, Y] = value;
        }

        //End Non User Dependent, System Defined

        public static string FolderName
        {
            get { return FolderNameData; }
            set { FolderNameData = value; }
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
            Properties.Settings.Default.Age =  AgeData;
            Properties.Settings.Default.Name = NameData;
            Properties.Settings.Default.SName = SNameData;

        }

        public string[] DumpAllValues()
        {
            //Add all of the varibels to this
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
