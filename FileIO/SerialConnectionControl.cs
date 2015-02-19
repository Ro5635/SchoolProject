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
        #region ReadData
        //Read data
        public string[] ReadData()
        {
            String DataPacket =  SerialDataHandle.ReadSerialData(); //get the Data from the serial controler.
            //Split the data:
            string[] DataSplit = DataPacket.Split('$');
            if (CheckCheckSum(DataSplit))//Do the following only if cheack sum is correct.
                {
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

        private Boolean CheckCheckSum(string[] Packet ){
            //This Function Cheaks the check sum in the data packet
            if (Convert.ToInt32(Packet[2]) == (Convert.ToInt32(Packet[0]) * 2) + Convert.ToInt32(Packet[1].Length))
            { //note chars are only counted not unicode ie o6tw = len 3
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region Transmit Data
        
        void


        #endregion Transmit data


    }
}
