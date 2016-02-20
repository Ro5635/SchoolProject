using System;
//using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class ArduinoCommunications : SerialConnectionControl
    {
        /*
         *This class holds all of the Variables that are being sent to and from the remote devices, the data is held in tables (multi dimension array), there are 3 columns ID Data and Update. 
         *ID is the Variable ID which is an integer and the Variables unique name that apply for it through out the solution. He data is held in a string form. The update column is an integer
         *the update column will display a 1 is a update has been requested. At each parse of the table the number will increment one. 
         * 
         */

        //The data Tables:

        //Stored as two separate arrays not multidimensional , objects for simplicity.
        const int MaxVars = 500; //Max number of Variables
        int[] VariableStatus = new int[MaxVars]; // Status : 0 = set, positive = tallying parses through whilst waiting for data, -1: Not initialized this session.
        string[] VariableData = new string[MaxVars]; // this contains the actual data as a string.

        //Create an array to hold the IDs that have been transmitted and are waiting for acknowledgment of receipt.
        int[] SentIDsWaitingAwk = new int[MaxVars];

        //Create Object SerialConnectionControl.
        SerialConnectionControl Serialcontroller = new SerialConnectionControl();

        //The array that stores the ID's that have requested updates, these need transmitting to the arduino.
        int[,] RequestedIDs = new int[MaxVars, 2];

        //Array That stores the IDs that should be transmitted to the remote device.
        int[,] RequestedToSendIDs = new int[MaxVars, 2];

        //The array that stores the given priority of each of the variable ID's.
        int[] PriorityLookUpData = new int[MaxVars];

        //Class initialized?
        Boolean Initialised = false;


        private void initialisation()
        {
            //The purpose of this class is to prepare the class for active function.
            //this should be called when this class is first used automatically.

            //Set all status to -1:
            for (int i = 0; i < MaxVars; i++)
            {
                VariableStatus[i] = -1;
            }

            //Load in the array that holds the different priority's of each variable ID.
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

        private void checkInitilisation()
        {
            if (Initialised == false)
            {
                initialisation();
            }
        }

        public string RequestData(int ID)
        {
            checkInitilisation();
            //Function returns the current data value in the array for given ID
            //Ensure that the data is present AND is updated with in reasonable time.
            if (VariableStatus[ID] >= 0 && VariableStatus[ID] <= 50)
            {
                return VariableData[ID];
            }else if(VariableStatus[ID] > 50){
                //Variable has not been updated in a reasonable amount of time

                return "^^DataUnupdated^^";
            }
            else
            {
                //There is not currently a data value available for that ID

                return "^^NoDataPresent^^";
            }
            
        }

        public void RequestUpdate(int[] RequestIDs)
        {
            checkInitilisation();
            //This function allows for the calling of updates to the variables stored in the table.
            //this will start the chain of events that see the variable updated with the most recent data.
            //this function is passed an array of the variables that are requested to be updated.
            
            //Perform a liner search to find the last value in the array to fill in from.
            //If no last value is found 0 will be used. The last value will have the ID 111.
            int LastPosition = -5635; //If this still has this value after liner search then start array fill from 0.
            for(int i = 0; i < MaxVars; i++){
                if(RequestedIDs[i , 0] == 111){
                    LastPosition = i;
                }
            }
            //If Position was not found make last position = to 0.
            if (LastPosition == - 5635){
                LastPosition = 0;
            }

            //Go thoruth each ID to be updated and append it to the Array, starting at the last position.
            for (int i = LastPosition; i < RequestIDs.Length ; i++){
                RequestedIDs[i, 0] = RequestIDs[i];
                //Now need to look up the priority of each ID and append that to the second "row".
                RequestedIDs[i, 1] = PriorityLookUpData[RequestIDs[i]];
                }
                //Now have ID and Array, now this "table" needs sorting into oder of priority's so the 
                //Variables can be transmitted in the correct order.
                //To do this call the sorting algorithm.
                //NB, currently this is not OOP because it is only needed here.
                 BubbleSortRequestedIDs(ref RequestedIDs); // This will result in a queue of ID's waiting for transmit in the correct order.
                //Now set the status of each ID to requested, this is too track the length of time each has been waiting.
                for (int i = 0; i < MaxVars; i++){
                    VariableStatus[ RequestedIDs[i,0] ] = 1; //Each ID that has been requested be set with a status of 1.
                }

        }

        private void BubbleSortRequestedIDs(ref int[,] refArrayTOSort)
        {
            //This sorts the array, this can be updated later to quick sort.
            int SwapTracking = 0;
            do
            {
                int index = 0;
                SwapTracking = 0; //Used to know when the array is ordered.
                int HoldingCellIsleA = 0; //Holding location for use during swaps.
                int HoldingCellIsleB = 0; //Holding Location for use during swaps.

                while (index < (MaxVars - 1))
                {

                    if (refArrayTOSort[index, 1] > refArrayTOSort[(index + 1), 1])
                    {
                        //Fill The golding Cells so not to loses value during the swap
                        HoldingCellIsleA = refArrayTOSort[(index + 1), 0];
                        HoldingCellIsleB = refArrayTOSort[(index + 1), 1];

                        //Swap The value
                        refArrayTOSort[(index + 1), 0] = refArrayTOSort[index, 0];
                        refArrayTOSort[(index + 1), 1] = refArrayTOSort[index, 1];
                        refArrayTOSort[index, 0] = HoldingCellIsleA;
                        refArrayTOSort[index, 1] = HoldingCellIsleB;

                        SwapTracking++; //Increment the swap tracker by 1.
                    }
                    index++; //Increment the index by one, vital step this if you want to go anywhere, moved to next value for comparison.
                }

            } while (SwapTracking > 0);//Keep looping until array is sorted, no swaps = in order.

            /*
            The bellow block is for testing and debugging purposes only.
            It prints the array to the console.
            Useful when not using IDE.
            for (int i = 0; i <= 20 ; i++){
                Console.WriteLine(refArrayTOSort[i,1]);
                }
            */
        }

        public void TransmitDataNow(int NumberOfPacketsTOSend)
        {
            checkInitilisation();
            //This function is called when you wish to transmit data, it is possible to define the number of packets that you wish to send
            //concecetivly.
            //This will work down the transmit requested array that has been left in the correct order transmitting the desired number of packets.
            //It will then sort the array again to remove the space at the front. The Ids in the transmit requested array are all Ids where 
            //the arduino will be asked to send the newest value.
            //Before sending the requests for updates to local variables it will send out the data that is to be sent to the arduino.

            //Create a temporary Array to hold the data for the re-shuffle, this will be used for both data and updates transmission.
            int[,] RequestedIDsTMPHold = new int[(MaxVars * 2), 2];

            //Hold the number of packets that have been requested to be sent so that the same number can be sent for updates and data.
            int HoldNumberOfPacketsToSend = NumberOfPacketsTOSend;
            ///////////////////////////////////////////////////Send Data To arduino:
            
            int DataSendArrayPosition = 0; //Start At the front of the queue.
            //RequestedToSendIDs[DataSendArrayPosition,0] gives ID of the variable to transmit.

            do
            {
                Serialcontroller.TransmitData(RequestedToSendIDs[DataSendArrayPosition, 0], VariableData[RequestedToSendIDs[DataSendArrayPosition, 0]]);
                //Set status to sent in data out table.
                SentIDsWaitingAwk[RequestedToSendIDs[DataSendArrayPosition, 0]] = 1;
                DataSendArrayPosition++;//Increment the value of the current position.
            } while (DataSendArrayPosition < NumberOfPacketsTOSend && DataSendArrayPosition < MaxVars && RequestedToSendIDs[DataSendArrayPosition, 0] != 0);
            //Ensure that currentpos has incremented from 0 less than the number of packets to send AND
            //that current position is less than max, in addition ensure that there is a variable present at that location.

            //Next resort the array, a number of leading items have been removed so shift all outer up.
            //Could change to a circular queue at a later point.
            
            //Ensure temporary holding array is clear
            RequestedIDsTMPHold = new int[(MaxVars * 2), 2];

            //Copy the data across from current position in array
            for (int i = DataSendArrayPosition; i < MaxVars; i++)
            {
                RequestedIDsTMPHold[i, 0] = RequestedToSendIDs[i, 0];
                RequestedIDsTMPHold[i, 1] = RequestedToSendIDs[i, 1];
            }
            //Now clear the Array and copy it all back.
            RequestedToSendIDs = new int[MaxVars, 2];
            for (int i = 0; i < MaxVars; i++)
            {
                RequestedToSendIDs[i, 0] = RequestedIDsTMPHold[i, 0];
                RequestedToSendIDs[i, 1] = RequestedIDsTMPHold[i, 1];
            }
            //The array is now sorted again.


            //////////////////////////////////////////////////////Send requests for updates from arduino to the arduino:

            NumberOfPacketsTOSend = HoldNumberOfPacketsToSend; //reset number of packets to send to original value.

            //The Current position in the array, will transmit from this point.
            int CurrentPosition = 0; //Start At the front of the queue.
            //RequestedIDs[CurrentPosition,0] gives ID of the variable to transmit.

            do{
                Serialcontroller.TransmitData( 222 , RequestedIDs[CurrentPosition, 0].ToString() );
                //the ID 222 means request, so above reads as request (222) ID no. RequestedIDs[CurrentPosition, 0] 
                CurrentPosition++;//Increment the value of the current position.
            } while (CurrentPosition < NumberOfPacketsTOSend && CurrentPosition < MaxVars && RequestedIDs[CurrentPosition, 0] != 0);
            //Ensure that current position has incremented from 0 less than the number of packets to send AND
            //that current position is less than max, in addition ensure that there is a variable present at that location.

            //Next resort the array, a number of leading items have been removed so shift all other up.
            //Could change to a circular queue at a later point.

            //Clear the temporary array of data.
            RequestedIDsTMPHold = new int[(MaxVars * 2), 2];
            //Copy the data across from current position in array
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
            checkInitilisation();
            //This will call a read and a write of the serial port.
            //it will update the status by completing the necessary action.
            //if the status is 26 it is in an error state, no data has been received 
            //for a considerable amount of time dispute it being requested, It will send the request again.

            //Read Data
            ReadInSerialData();

            //Send Data 10 packets

            TransmitDataNow(10);

            //Now increment the status where status is active (greater than one) 
            //as it will be a cycle where still waiting for data from arduino.

            ProccessDataOutWaitingAwkTable();//process the data out waiting for acknowledgment table.

            TickStatusPoint(1);
            

        }

        private void ReadInSerialData()
        {
            //This function calls for a read of the serial port and spends the data to the correct position in the table.
            //it then changes its status to 0.

            //Read in the data from the port using the serial connection control class.
            string[] DataIn = Serialcontroller.ReadData(); //gets ID and Data.

            //check that it is not a a acknowledgment:

            if (Convert.ToInt32(DataIn[0]) == 111)
            {
                //it is an acknowledgment, update data out table as necessary.
                SentIDsWaitingAwk[Convert.ToInt32(DataIn[1])] = 0;
                //table updated.

            }
            else
            {

                //Update the variable tables
                VariableStatus[Convert.ToInt32(DataIn[0])] = 0; //Status is ID is number 0.

                VariableData[Convert.ToInt32(DataIn[0])] = DataIn[1]; //Data of ID is now the received data.
            }
        }

        private void TickStatusPoint(int NumOfPointsForward)
        {
            //This function will increment the status of the relevant IDs by one
            //The ids that need incrementing are ones that have a active setting currently
            //and will have a value of greater than 0.

            for (int i = 0; i < MaxVars; i++) //go through all IDs
            {
                //is current status greater than 0.
                if (VariableStatus[i] > 0)
                {
                    //variable status greater than 0, increment by 1.
                    VariableStatus[i] = (VariableStatus[i] + NumOfPointsForward);
                        //If status is 40 or bigger send request again.
                        if(VariableStatus[i] >= 40)
                        {
                            //Send request again
                            int[] UpdateID = { VariableStatus[i] }; //Put ID to request into an array.
                            RequestUpdate(UpdateID); //Request Update on that ID.
                            VariableStatus[i] = 25;// put status back down so re-request is not issued net run.

                        }
                }
            }
        }

        public void SendDataToArduino(int ID , string Data)
        {
            checkInitilisation();
            //This method is tasked with allowing external classes to request data to be sent to the remote device
            //It takes the ID to be transmitted and the data.
            //With this it appends that data to the correct position in the data table and lists the ID for transmission.

            VariableData[ID] = Data;//Update the data in the data table to the new value.

            //Add this to the queue for sending.
            //In order to do this search for the end point of the array.

            int LastPosition = -5635; //If this still has this value after liner search then start array fill from 0.
            for (int i = 0; i < MaxVars; i++)
            {
                if (RequestedToSendIDs[i, 0] == 111) //NB 111 is used because this would not be an active ID as it is the request ID.
                {
                    LastPosition = i;
                }
            }
            //If Position was not found make last position = to 0.
            if (LastPosition == -5635)
            {
                LastPosition = 0;
            }

            //Add the requested ID to the array.
            LastPosition++;//increment last position to get first free position in array.
            RequestedToSendIDs[LastPosition, 0] = ID;

            //Now need to look up the priority of each ID and append that to the second "row".
            //This will be used during the sorting.
            RequestedToSendIDs[LastPosition, 1] = PriorityLookUpData[ID];

            //Now ensure that the array is in the correct order in terms of priority.
            //This means sorting it using the bubble sort method.
            BubbleSortRequestedIDs(ref RequestedToSendIDs);

            //The array is now sorted correctly ready for eventual transmission.


        }

        private void ProccessDataOutWaitingAwkTable()
        {
            //This method goes through the data out table that lists all of the IDs that have been sent and have not yet had 
            //an acknowledgment received. If the status has incremented above 25 the retransmission of the data should be
            //started, this is because the arduino may not have successfully received the packet.
            int ReTransmitLimit = 40;

            for(int i = 0; i < MaxVars; i++){
                if (SentIDsWaitingAwk[i] > 0){
                    //Ensure that status is not above re-transmit limit
                    if(SentIDsWaitingAwk[i] >= ReTransmitLimit){
                        //status is above limit, call re-transmission.
                        int[] ReSendMe = new int[] { i };
                        RequestUpdate(ReSendMe);
                        
                        SentIDsWaitingAwk[i] = 25;//Set status to 25 so not re-transmitted again next run.

                    }
                    SentIDsWaitingAwk[i]++; //Increment status by 1 
                }
            }
            

        }
    }
}
