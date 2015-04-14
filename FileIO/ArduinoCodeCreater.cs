﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class ArduinoCodeCreater
    {
        int[] PWMPins = int[25];
        int[] DigitalPins = int[25];
        int[] AnalogPins = int[25];

        //Variables to hold the current position in the pins array.
        int PWMPinsPoint = 0;
        int DigitalPinsPoint = 0;
        int AnaloguePinsPoint = 0;

        int SerialBaudRate;
        int CurentBorad;
        string welcomeWebLink = "www.example.co.uk";


        //Servo and Motors
        Boolean ServosUsed = false;

        
        string ArduinoFileLocation = "NA";

        public void Run(){
            Console.WriteLine("This is a test of plans to make this class");
            //create the object if this class
            ArduinoCodeCreater ArdCodeCreater = new ArduinoCodeCreater();

            ArdCodeCreater.LoadData();
            ArdCodeCreater.WriteToArduinoFile("", false);
            ArdCodeCreater.FileHeader();

            //Setup the universal parameters to all arduino robots.
            ArdCodeCreater.UniversalSetup();

    }

        static void Main(string[] args)
        {
           





        }


        private void FileHeader()
        {
            WriteToArduinoFile("//This was created with the arduino code creation Tool.");
            WriteToArduinoFile("//Please paste the bellow text into the arduino compiler and upload to your selected Arduino board.");
        }

        private void LoadData()
        {
            GlobalVar AccessData = new GlobalVar();
            ArduinoFileLocation = @"C:\Deleateme\Example.ino";//@"/Users/robert/Documents/Example.ino";
            //Update all of these by using the arduino robot file.

            //Borad type
            CurentBorad = AccessData.ArduinoBorad;

            //Serial
            SerialBaudRate = 9600;

            //Micilanius Servos Knob Type
            Boolean ServoMic1 = false;
            Boolean ServoMic2 = false;

            //Micilanius servos Sweep Type:
            Boolean ServoSweep = false;
            Boolean ServoSweep2 = false;

            //Motors
            //the arduino motor controller board is assumed to be available


            //Servo as Drive
            Boolean ServoDriveAsDrive = false;

            //Sensors
        }

        private Boolean WriteToArduinoFile(string LineToWrite, Boolean Append = true)
        {
            //This method allows writing to the Arduino file.
            //Write is one line at a time.

            //Ensure that the File Location has been filled in.
            if (ArduinoFileLocation == "NA")
            {
                //The file location has not been completed, return write fail.
                return false;
            }
            else
            {
                //Write to the file

                if (Append)
                { //append or over write

                    try
                    {
                        using (StreamWriter WriteHandle = new StreamWriter(ArduinoFileLocation, true))
                        {
                            WriteHandle.WriteLine(LineToWrite);
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be Written to:");
                        Console.WriteLine(e.Message);
                        return false;
                    }

                }
                else
                {

                    try
                    {
                        using (StreamWriter WriteHandle = new StreamWriter(ArduinoFileLocation, false)) //Overwrite File
                        {
                            WriteHandle.WriteLine(LineToWrite);
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be Written to:");
                        Console.WriteLine(e.Message);
                        return false;
                    }


                }
            }
        }

        private void UniversalSetup()
        {
            //This function runs the setup necessary for all arduino types.

            //Start the Setup void
            WriteToArduinoFile("void setup() {//Code to be ran one at power on");
            SetupSerial(SerialBaudRate);
            
            //Determine The Bord Type
            SetBorad();


            //Need Servo Lib?
            if (ServosUsed)
            {
                WriteToArduinoFile("#include <Servo.h>//Include the Servo Lib"); 
            }
            WriteToArduinoFile("}//End Setup void");//End Arduino Setup function


        }
        private void SetupSerial(int BaudRate)
        {
            //This method creates the serial port for the arduino to use.

            WriteToArduinoFile("//The system is designed to operate at a selection of baud rates, these are:");
            WriteToArduinoFile("//1200,2400,4800,9600,19200,38400,57600,115200,230400");
            string LineToWrite = "int BaudRateSerial1 = " + BaudRate + "//The BaudRate to use for Communication";

            WriteToArduinoFile(LineToWrite);
            WriteToArduinoFile("Serial.begin(BaudRateSerial1);"); //Start the Serial interface
            WriteToArduinoFile("//print the welcome message " + '\n' + "Serial.println(\"Arduino Robotic System\");" + '\n' + "Serial.println(\"" + welcomeWebLink + "\");");


        }

        private void SetBorad(){
            //This finds the correct borad to use and sets the pin arrays as neccasary.
            if (CurentBorad == 0){
                //The borad is an UNO
                int[] ArdUNOPWMPins = {3,5,6,7,10,11};
                int[] ArdUNODigitalPins = {2,4,8,12,13};
                int[] ArdUNOAnaloguePins = {0,1,2,3};//Need 'A' in front when used.
                PWMPins = ArdUNOPWMPins;
                PWMPinsPoint = 0;
                DigitalPins = ArdUNODigitalPins;
                DigitalPinsPoint = 0;
                AnalogPins = ArdUNOAnaloguePins;
                AnaloguePinsPoint = 0;

            }else if(CurentBorad == 1){
                //The borad is an arduino Mega
                int[] ArdMEGAPWMPins = {2,3,4,5,6,7,8,9,10,11,12,13};
                int[] ArdMEGADigitalPins = {22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52};
                int[] ArdMEGAAnaloguePins = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};//Need 'A' in front when used.
                PWMPins = ArdMEGAPWMPins;
                PWMPinsPoint = 0;
                DigitalPins = ArdMEGADigitalPins;
                DigitalPinsPoint = 0;
                AnalogPins = ArdMEGAAnaloguePins;
                AnaloguePinsPoint = 0;

            }else if(CurentBorad == 3){
                //The borad is an Arduino mini
            }

        }

    }
}