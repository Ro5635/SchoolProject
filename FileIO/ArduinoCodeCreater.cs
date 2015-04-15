using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class ArduinoCodeCreater
    {
        int[] PWMPins = new int[25];
        int[] DigitalPins = new int[25];
        int[] AnalogPins = new int[25];

        //Variables to hold the current position in the pins array.
        int PWMPinsPoint = 0;
        int DigitalPinsPoint = 0;
        int AnaloguePinsPoint = 0;

        int SerialBaudRate;
        int CurentBorad;
        string welcomeWebLink = "www.example.co.uk";


        //Servo and Motors
        Boolean ServosUsed = false;

        Boolean ServoMic1 = false;
        int ServoMic1Pin;
        Boolean ServoMic2 = false;
        int ServoMic2Pin;


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

            //Write the loop
            ArdCodeCreater.WriteToArduinoFile("void loop(){");// start the arduino main loop.

            ArdCodeCreater.WriteToArduinoFile("}//End Loop");// End the loop

            //Write the serial Event void:
            ArdCodeCreater.CreateSerialEvenMethord();

            if (ArdCodeCreater.ServoMic1)
            {
                ArdCodeCreater.PositionRoutineServoMic1();
            }
            if (ArdCodeCreater.ServoMic2)
            {
                ArdCodeCreater.PositionRoutineServoMic2();

            }
            
           

    }

        static void Main(string[] args)
        {
           





        }


        private void FileHeader()
        {
            WriteToArduinoFile("//This was created with the arduino code creation Tool.");
            WriteToArduinoFile("//Please paste the bellow text into the arduino compiler and upload to your selected Arduino board.");
            string BoradName = "Failure";
            if(CurentBorad == 0){
                BoradName = "Arduino UNO";
            } else if (CurentBorad == 1){
                BoradName = "Arduino Mega";
            }
            WriteToArduinoFile("//This was designed for the: " + BoradName);
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
            ServoMic1 = AccessData.ServoMic1;
            ServoMic2 = AccessData.ServoMic2;

            //Are servos used?
            if (ServoMic1 || ServoMic2){
                ServosUsed = true;
            }

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

            //Create the neccasary Arduino variables:

            if (ServosUsed)
            {
                WriteToArduinoFile("#include <Servo.h>//Include the Servo Lib");
            }

            if (ServoMic1)
            {
                WriteToArduinoFile("Servo ServoMic1;  // create servo object");
            }
            if (ServoMic2)
            {
                WriteToArduinoFile("Servo ServoMic2;  // create servo object ");

            }


            //Start the Setup void
            WriteToArduinoFile("void setup() {//Code to be ran one at power on");
            SetupSerial(SerialBaudRate);
            
            //Determine The Bord Type
            SetBorad();

            //Need Servo Lib?
            if (ServosUsed)
            {
               
                if (ServoMic1)
                {
                    ServoMic1Pin = DigitalPins[DigitalPinsPoint++];
                    WriteToArduinoFile("ServoMic1.attach(" + ServoMic1Pin + "); ");
                }if(ServoMic2){
                    ServoMic2Pin = DigitalPins[DigitalPinsPoint++];
                    WriteToArduinoFile("ServoMic2.attach(" + ServoMic2Pin + "); ");

                }
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
            WriteToArduinoFile("//print the welcome message ");
            WriteToArduinoFile("Serial.println(\"Arduino Robotic System\");");
            WriteToArduinoFile("Serial.println(\"" + welcomeWebLink + "\");");

        }


        private void CreateSerialEvenMethord()
        {

            WriteToArduinoFile("void serialEvent(){" );
            WriteToArduinoFile(" while (Serial.available()){");
            WriteToArduinoFile("  char recivedchar = (char)Serial.read();");
            WriteToArduinoFile(" if (recivedchar == '^') {");
            WriteToArduinoFile(" FinishedInput = true;");
            WriteToArduinoFile(" }");
            WriteToArduinoFile("if (recivedchar != '^'){");
            WriteToArduinoFile(" recivedtring += recivedchar;" + '\n' + " }" + '\n' + " }" );
            WriteToArduinoFile("if (FinishedInput) {");
            WriteToArduinoFile(" coreCommunication();");
            WriteToArduinoFile(" if(PCAccessMode){");
            WriteToArduinoFile(" //PC Access mode enabled");
            WriteToArduinoFile("//this is used because there will be a different command set to listen for compared to the standard set");
            WriteToArduinoFile(" PCAccessCommunication();");
            WriteToArduinoFile(" } else {");
            WriteToArduinoFile(" runTimeCommunication(); // standard runtime communicaation");
            WriteToArduinoFile("  }" + '\n' + "  recivedtring = \"\";" + '\n' + "  recivedtring = \"\";" + '\n' + "  FinishedInput = false;" + '\n' + "  } " + '\n' + "}");
            
            //Set up Core Communication:

            WriteToArduinoFile("void coreCommunication(){");
            WriteToArduinoFile("if(recivedtring == \"Robot?\"){");
            WriteToArduinoFile("//Start Up Communication from Program");
            WriteToArduinoFile("Serial.println(\"^Arduino_Robot^\");");
            WriteToArduinoFile("Serial.println(\"Entering PC Access Mode\");");
            WriteToArduinoFile("PCAccessMode = true;");
            WriteToArduinoFile(" } ");
            WriteToArduinoFile("}");
            WriteToArduinoFile("void  runTimeCommunication(){");
            WriteToArduinoFile("}");
            WriteToArduinoFile("void PCAccessCommunication(){");
            WriteToArduinoFile("}");
            WriteToArduinoFile("}");

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



                private void PositionRoutineServoMic1(){
        //This void writes out the Position routine for ServoMic1.

        WriteToArduinoFile("int PositionServoMic1(int Pos){//This is a position function for servoMic1");
        WriteToArduinoFile("//The value for the servo should be between 0 an 180");
        WriteToArduinoFile(" if (Pos > 179){");
        WriteToArduinoFile(" servoMic1.write(180);");
        WriteToArduinoFile(" } else if(Pos < 0){");
        WriteToArduinoFile("  servoMic1.write(0);");
        WriteToArduinoFile("}else{");
        WriteToArduinoFile(" servoMic1.write(Pos); }");
        WriteToArduinoFile("}");
        }

        private void PositionRoutineServoMic2(){
        //This void writes out the Position routine for ServoMic1.
        WriteToArduinoFile("int PositionServoMic2(int Pos){//This is a position function for servoMic2");
        WriteToArduinoFile("//The value for the servo should be between 0 an 180");
        WriteToArduinoFile(" if (Pos > 179){");
        WriteToArduinoFile(" servoMic2.write(180);");
        WriteToArduinoFile(" } else if(Pos < 0){");
        WriteToArduinoFile("  servoMic2.write(0);");
        WriteToArduinoFile("}else{");
        WriteToArduinoFile(" servoMic2.write(Pos); }");
        WriteToArduinoFile("}");

        }


    }
}
