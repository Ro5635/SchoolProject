using System;
//using System.IO;
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
        SerialConnectionControl Serialcontroller = new SerialConnectionControl();


        //const int MaxvarID = 21;
        //The array that stores the ID's that have requested updates, these need transmitting to the arduino.
        int[,] RequestedIDs = new int[MaxVars, 2];

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
            
            //Perform a liner search to find the last value in the array to fill in from.
            //If no last value is found 0 will be used. The last value will have the ID 111.
            int LastPosition = -5635; //If this still has this value after liner search then start array fill from 0.
            for(int i = 0; i < MaxVars; i++){
                if(RequestedIDs[i , 0] == 111){
                    LastPosition = i;
                }
            }
            //If Position was not gound make last position = to 0.
            if (LastPosition == - 5635){
                LastPosition = 0;
            }

            //Go thoruth each ID to be updated and append it to the Array, starting at the last position.
            for (int i = LastPosition; i < RequestIDs.Length ; i++){
                RequestedIDs[i, 0] = RequestIDs[i];
                //Now need to look up the priority of each ID and append that to the second "row".
                RequestedIDs[i, 1] = PriorityLookUpData[RequestIDs[i]];
                }
                //Now have ID and Array, now this "table" needs sorting into oder of prioritys so the 
                //Variables can be transmitted in the correct order.
                //To do this call the sorting algorithm.
                //NB, currently this is not OOP because it is only needed here.
                BubbleSortRequestedIDs(); // This will result in a queue of ID's waiting for transmitt in the correct order.
                //Now set the status of each ID to requested, this is too track the length of time each has been waiting.
                for (int i = 0; i < MaxVars; i++){
                    VariableStatus[ RequestedIDs[i,0] ] = 1; //Each ID that has been requested be set with a status of 1.
                }

        }

        private void BubbleSortRequestedIDs()
        {
            //This sorts the array, this can be updated later to quicksort.
            int SwapTracking = 0;
            do
            {
                int index = 0;
                SwapTracking = 0; //Used to know when the array is ordered.
                int HoldingCellIsleA = 0; //Holding location for use during swaps.
                int HoldingCellIsleB = 0; //Holding Location for use during swaps.

                while (index < (MaxVars - 1))
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

        public void TransmitDataNow(int NumberOfPacketsTOSend)
        {
            //This function is called when you wish to transmit data, it is possible to define the number of packets that you wish to send
            //concecetivly.
            //This will work down the transmit requested array that has been left in the correct order transmitting the desiered number of packets.
            //It will then sort the array again to remove the sapce at the front.

            //The Current position in the array, will transmit from this point.
            int CurrentPosition = 0; //Start At the front of the queue.
            //RequestedIDs[CurrentPosition,0] gives ID of the variable to transmit.

            do{
                Serialcontroller.TransmitData(RequestedIDs[CurrentPosition, 0], VariableData[ RequestedIDs[CurrentPosition, 0] ]);
                CurrentPosition++;//Increment the value of the current position.
            } while (CurrentPosition < NumberOfPacketsTOSend && CurrentPosition < MaxVars && RequestedIDs[CurrentPosition, 0] != 0);
            //Ensure that currentpos has incremented from 0 less than the number of packets to send AND
            //that current position is less than max, inaddition ensure that there is a variable present at that location.

            //Next resort the arry, a number of leading items have been removed so shift all outher up.
            //Could change to a circular queue at at a later point.

            //Create a tempary Array to hold the data for the re-suffle.
            int[,] RequestedIDsTMPHold = new int[(MaxVars * 2), 2];
            //Copy the data accross from current position in array
            for (int i = CurrentPosition; i < MaxVars; i++)
            {
                RequestedIDsTMPHold[i, 0] = RequestedIDs[i, 0];
                RequestedIDsTMPHold[i, 1] = RequestedIDs[i, 1];
            }
            //Now clear the Array and copy it all back.
            RequestedIDs = new int[MaxVars, 2];
            for (int i = 0; i < MaxVars; i++)
            {
                RequestedIDs[i, 0] = RequestedIDsTMPHold[i, 0];
                RequestedIDs[i, 1] = RequestedIDsTMPHold[i, 1];
            }
            //The array is now sorted again.
        }

        public void StandardTimmedSerialActuate()
        {

        }


    }
}
