using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class ArduinoCommunications
    {
        /*
         *This class holds all of the Variables that are being sent to and from the remote devices, the data is held in tables (multi dimension array), there are 3 columns ID Data and Update. 
         *ID is the Variable ID which is an intger and the Variables unique name that applys for it throuth out the solution. He data is held in a string form. The update column is an integer
         *the update column will display a 1 is a update has beemn requested. At each parse of the table the number will increment one. 
         * 
         */

        //The data Tables:

        //Stored as two seperate arrays not multidimensioinal , objects for simplicity.
        const int MaxVars = 500; //Max number of Variables
        int[] VariableStatus = new int[MaxVars]; // Status : 0 = set, posative = tallying parses throuth whilst waiting for data, -1: Not initialised this session.
        string[] VariableData = new string[MaxVars]; // this contains the actual data as a string.

        //Create Object SerialConnectionControl.
        SerialConnectionControl SerialControler = new SerialConnectionControl();


        //Class initilised?
        Boolean Initialised = false;


        private void initialisation()
        {
            //The pourpose of this class is to prepare the class for active function.
            //this should be called when this class is first used automaticaly.

            //Set all status to -1:
            for (int i = 0; i < MaxVars; i++)
            {
                VariableStatus[i] = -1;
            }

                Initialised = true; //ensure that this is not run again.
        }

        public string RequestData(int ID)
        {
            //Function returns the current data value in the array for given ID
            //Ensure that the data is present AND is updated with in reasanable time.
            if (VariableStatus[ID] >= 0 && VariableStatus[ID] <= 50)
            {
                return VariableData[ID];
            }else if(VariableStatus[ID] > 50){
                //Variable has not beeen updated in a reasnable amount of time

                return "^^DataUnupdated^^";
            }
            else
            {
                //There is not currently a data value avaliable for that ID

                return "^^NoDataPresent^^";
            }
            
        }

        public void RequestUpdate(int[] RequestIDs)
        {
            //This function allows for the calling of updates to the variables stored in the table.
            //this will start the chain of events that see the variable updated with the most recent data.
            //This function is passed an arry of the varables that are requested to be updated.

        }

    }
}
