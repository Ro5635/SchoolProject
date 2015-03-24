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
         *This class holds all of the varables that are being sent to and from the remote devices, the data is held in tables (multi dimension array), there are 3 columns ID Data and Update. 
         *ID is the varable ID which is an intger and the varables unique name that applys for it throuth out the solution. He data is held in a string form. The update column is an integer
         *the update column will display a 1 is a update has beemn requested. At each parse of the table the number will increment one. 
         * 
         */

        //The data Tables:

        //Stored as two seperate arrays not multidimensioinal , objects for simplicity.
        const int MaxVars = 500; //Max number of varables
        int[] VarableStatus = new int[MaxVars]; // Status : 0 = set, posative = tallying parses throuth whilst waiting for data, -1: Not initialised this session.
        string[] VarableData = new string[MaxVars]; // this contains the actual data as a string.

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
                VarableStatus[i] = -1;
            }



                Initialised = true; //ensure that this is not run again.
        }


 


    }
}
