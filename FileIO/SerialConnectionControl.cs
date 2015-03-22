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

    class SerialConnectionControl
    {
        SerialControl SerialDataHandle = new SerialControl();
        GlobalVar GlobalsAccessHandle = new GlobalVar();
        Boolean initialised = false;

        public void initialise(){
            //Open the primary Serial Port (the first port that was found, this will often be the only one.)
            SerialDataHandle.OpenSerialPort(GlobalsAccessHandle.PrimarySerialPortName, GlobalsAccessHandle.PrimarySerialPortBaud); //Get the Baud and port name from Global vars, Use the primary.
            initialised = true;
        }

        #region ReadData
        //Read data
        public string[] ReadData()
        {
            if (initialised == false)
            {
                initialise();
            }
            String DataPacket =  SerialDataHandle.ReadSerialData(); //get the Data from the serial controler.
            //Split the data:
            string[] DataSplit = DataPacket.Split('$');
            if (CheckCheckSum(DataSplit))//Do the following only if cheack sum is correct.
                {
                    //Awk Successful recepit of uncurupted data
                    SendSuccessfulReceiveAwk(DataSplit[0].ToString());
                    string[] ReturnPackage = {DataSplit[0],DataSplit[1]};
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
            //Reply "I got That, dont send again"...
            // 111 is the Awk ID
            SerialControl SerialHandler = new SerialControl();
            int CheckSum = ( 111 * 2 ) + ID.Length; 
            SerialHandler.WriteSerialData("111$" + ID + "$"+ CheckSum);
        }

        private Boolean CheckCheckSum(string[] Packet ){
            //This Function Cheaks the check sum in the data packet
            try
            {
                if (Convert.ToInt32(Packet[2]) == (Convert.ToInt32(Packet[0]) * 2) + Convert.ToInt32(Packet[1].Length))
                { //note chars are only counted not unicode ie o6tw = len 3
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
        
        


        #endregion Transmit data


    }
}
