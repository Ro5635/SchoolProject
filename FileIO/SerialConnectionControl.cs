using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    /*
     * This is the Serial Communication Control class, it uses the SerialControl Class to handle the reading and writing of the data.
     * */

    class SerialConnectionControl : SerialControl
    {
        SerialControl SerialDataHandle = new SerialControl();
        GlobalVar GlobalsAccessHandle = new GlobalVar();
        Boolean initialised = false;

        public void initialise(){
            //Open the primary Serial Port (the first port that was found, this will often be the only one.)
            SerialDataHandle.OpenSerialPort(GlobalsAccessHandle.PrimarySerialPortName, GlobalsAccessHandle.PrimarySerialPortBaud); //Get the Baud and port name from Global vars, Use the primary.
            initialised = true;
            Console.WriteLine("The Serial Port Initialization has begun");
        }

        #region ReadData
        //Read data
        public string[] ReadData()
        {
            if (initialised == false)
            {
                initialise();
            }
            String DataPacket =  SerialDataHandle.ReadSerialData(); //get the Data from the serial controller.
            //Split the data:
            string[] DataSplit = DataPacket.Split('$');
            if (CheckCheckSum(DataSplit))//Do the following only if check sum is correct.
                {
                    //Awk Successful receipt of uncorrupted data
                    SendSuccessfulReceiveAwk(DataSplit[0].ToString());
                    string[] ReturnPackage = {DataSplit[0],DataSplit[1]}; //Returns ID(0) and Data(1).
                    return ReturnPackage;
                }
            else
            {
                //Drop the packet
                string[] fail = {"",""};
                return fail ;  
            }
        }

        private void SendSuccessfulReceiveAwk(string ID)
        {
            //Reply "I got That, don't send again"...
            // 111 is the Awk ID
            SerialControl SerialHandler = new SerialControl();
            int CheckSum = ( 111 * 2 ) + ID.Length; 
            SerialHandler.WriteSerialData("111$" + ID + "$"+ CheckSum);
        }

        private Boolean CheckCheckSum(string[] Packet ){
            //This Function Checks the check sum in the data packet
            try
            {
                //The check sum is ID * 2 + Number of characters in the Data.
                if (Convert.ToInt32(Packet[2]) == (Convert.ToInt32(Packet[0]) * 2) + Convert.ToInt32(Packet[1].Length))
                { //note chars are only counted not Unicode IE o6tw = len 3
                    return true;
                }
                else
                {
                    return false;
                }
            } //End Try
            catch {
                return false;
            }
        }

        #endregion


        #region Transmit Data

        public void TransmitData(int ID, string Data)
        {
            //This handles function handles the creation of the packet structure and then hands the packet down the serial
            //class to handle the actual transmission.
            //ID$Data$Check sum

            //Ensure that everything is initiated correctly.
            if (initialised == false)
            {
                initialise();
            }

            //The check sum is ID * 2 + Number of characters in the Data.
            int CheckSum = ((ID * 2) + Data.Length); //Calculate the check sum value.
            //Call Serial Data handle to transmit the fully formed packet.
            SerialDataHandle.WriteSerialData(ID + "$" + Data + "$" + CheckSum); //NB "^" are added later.

        }


        #endregion Transmit data


    }
}
