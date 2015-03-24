using System;
using System.IO;
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


        const int MaxvarID = 21;
        //The array that stores the ID's that have requested updates, these need transmitting to the arduino.
        int[,] RequestedIDs = new int[MaxvarID, 2];

        //The array that stores the given priority of each of the varable ID's.
        int[] PriorityLookUpData = new int[MaxVars];

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

            //Load in the array that holds the differnt prioritys of each variable ID.
            FileHandaling SettingsRead = new FileHandaling(); //File handler for reading the file.
            //Read the file using the file handler.
            string VariableLookUpRaw = SettingsRead.ReadText("PriorityData.txt");

            int i2 = 0;
            foreach (string line in VariableLookUpRaw.Split('\n'))
            {
                PriorityLookUpData[i2] = Convert.ToInt32(line); //Convert the priority to a number
                i2++; //increment the index.
            }
            //Example, if wanted the priority of variable ID 2 then PriorityLookUpData[2] would give its priority value.





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
            //this function is passed an arry of the varables that are requested to be updated.
            
            //Go thoruth each ID to be updated and append it to the Array.
            for (int i = 0; i < RequestIDs.Length ; i++){
                RequestedIDs[i, 0] = RequestIDs[i];
            }

            //Now need to look up the priority of each ID and append that to the second "row".




        }


        public void BubbleSortRequestedIDs()
        {
            //This sorts the array
            int SwapTracking = 0;
            do
            {
                int index = 0;
                SwapTracking = 0; //Used to know when the array is ordered.
                int HoldingCellIsleA = 0; //Holding location for use during swaps.
                int HoldingCellIsleB = 0; //Holding Location for use during swaps.

                while (index < (MaxvarID - 1))
                {

                    if (RequestedIDs[index, 1] > RequestedIDs[(index + 1), 1])
                    {
                        //Fill The golding Cells so not to losse value during the swap
                        HoldingCellIsleA = RequestedIDs[(index + 1), 0];
                        HoldingCellIsleB = RequestedIDs[(index + 1), 1];

                        //Swap The value
                        RequestedIDs[(index + 1), 0] = RequestedIDs[index, 0];
                        RequestedIDs[(index + 1), 1] = RequestedIDs[index, 1];
                        RequestedIDs[index, 0] = HoldingCellIsleA;
                        RequestedIDs[index, 1] = HoldingCellIsleB;

                        SwapTracking++; //Increment the swap tracker by 1.
                    }
                    index++; //Increment the index by one, vital step this if you want to go anywhere, moved to next value for comparison.
                }

            } while (SwapTracking > 0);//Keep looping until array is sorted, no swaps = in order.

            /*
            The bellow block is for testing and debugging pourposes only.
            It prints the array to the console.
            Useful when not using IDE.
            for (int i = 0; i <= 20 ; i++){
                Console.WriteLine(RequestedIDs[i,1]);
                }
            */
        }



    }
}
