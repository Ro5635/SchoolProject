using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace FileIO
{
    /*
     * This class is responsable for handaling the deployment of saved data to the program and saving of user data, the 
     * mechanics of doing this are compleated from the file handaling class.
     * */
    class UserSettingsHandle
    {
       
        // Create the Error Reporter object
        LogAndErrorFiles ErrorReporter = new LogAndErrorFiles();
        
        

        #region User Settings load
        
        public void LoadUserSettings(string SettingsFileLocation)
        {

            //Check settings file is selected:
            if (SettingsFileLocation == "")
            {
                return;
            }

            FileHandaling SettingsRead = new FileHandaling();
            // read all text from the settings file
            string SettingsFileDataRaw = SettingsRead.ReadText(SettingsFileLocation);
            //Split the file at every occerance of the varible indecator.
            // THE ARRAY's DEFINED BELLOW REPRESENTS THE MAX NUMBER OF VARIBLES THAT CAN BE READ, THIS WILL NEED MANIPULATING LATER
            int MaxVaribles = 99;
            string[,] VarArray = new string[MaxVaribles, 3];
            string[,] MasterReadVaribles = new string[MaxVaribles, 2];



            // varible to track progress through bellow loop
            int ForeachRunCount = 0;
            //Split the raw data at every occerance of the dilimeator, this puts into each varible set (name and value)
            string[] DataInBreakDown = SettingsFileDataRaw.Split('\n');    
            
            //This loop bellow will split the data into an array with the varible identifier and value seperated appropiatly
            foreach (string data in SettingsFileDataRaw.Split('\n'))
            {
                string[] IndexSplit = DataInBreakDown[ForeachRunCount].Split('^');
                VarArray[ForeachRunCount, 0] = IndexSplit[0];
                VarArray[ForeachRunCount, 1] = IndexSplit[1];
                ForeachRunCount++;
            }
            int NumberOfReadVaribles = ForeachRunCount;
            //account for the fact that 1 is added at the end of the loop above
            if (NumberOfReadVaribles > 1){
                NumberOfReadVaribles--;
            }

            
            //Note maxvaribles is 1 larger than an array based on it as 0 is a place
            for (int i = 0; i < (NumberOfReadVaribles + 1 ); i++)
            {
                //System.Diagnostics.Debugger.Break();
                //Set value
                char[] RemoveTheseCharsA = { '\r', '\n' };
                MasterReadVaribles[Convert.ToInt32(VarArray[i,0]), 0] = VarArray[i,1].TrimEnd(RemoveTheseCharsA);
                //Value Set = true
                MasterReadVaribles[Convert.ToInt32(VarArray[i, 0]), 1] = "1";
            }


            #region LoadInVaribleLookUpDataFromTextFile

            /*This block will bring in all of the varibleID data from the text file, this is so that the varible can be idenfified,
             * the block contains code very simular to above but sufficiantly differnt to make an object un viable
             * */

            //VaribleLook up is the array that will contain all varible lookup data:
            string[,] VaribleLookUp = new string[MaxVaribles,3]; 
            string VaribleLookUpRaw = SettingsRead.ReadText("VariblesID.txt");

            // Reset varible to track progress through bellow loop
            ForeachRunCount = 0;
            //Split the raw data at every occerance of the dilimeator, this puts into each varible set (name and value)
            DataInBreakDown = VaribleLookUpRaw.Split('\n');    
            
            //This loop bellow will split the data into an array with the varible identifier and value seperated appropiatly
            foreach (string data in VaribleLookUpRaw.Split('\n'))
            {
                string[] IndexSplit = DataInBreakDown[ForeachRunCount].Split('^');
                VarArray[ForeachRunCount, 0] = IndexSplit[0];
                VarArray[ForeachRunCount, 1] = IndexSplit[1];
                VarArray[ForeachRunCount, 2] = IndexSplit[2];
                ForeachRunCount++;
            }

            NumberOfReadVaribles = ForeachRunCount;

            
            //Note maxvaribles is 1 larger than an array based on it as 0 is a place
            for (int i = 0; i < NumberOfReadVaribles; i++)
            {
                // Get ID
                VaribleLookUp[Convert.ToInt32(VarArray[i,0]), 0] = VarArray[i,0];
                //Get Var Name
                VaribleLookUp[Convert.ToInt32(VarArray[i, 0]), 1] = VarArray[i,1];
                //Get Data Type
                //The \n and \r must be cleaned from the data
                char[] RemoveTheseChars = { '\r', '\n' };
                 VaribleLookUp[Convert.ToInt32(VarArray[i, 0]), 2] = VarArray[i,2].TrimEnd(RemoveTheseChars);
            }

            #endregion LoadInVaribleLookUpDataFromTextFile


            //Error Message displayed?
            bool ErrorUnsportedDisplayed = false;
            GlobalVar GlobalVaribles = new GlobalVar();
            for (int i = 0; i <= (MaxVaribles - 1); i++)
            {
                
               if (MasterReadVaribles[i,1] == "1"){
                  //The above line checks to see wheather the value has been set
                   if (VaribleLookUp[i, 2] == "Int")
                   {
                       // the varible is an int, convert to int
                       GlobalVaribles.SetProperty(VaribleLookUp[i, 1].ToString(), Convert.ToInt32(MasterReadVaribles[i, 0]));
                   }
                   else if (VaribleLookUp[i, 2] == "String")
                   {
                       //the varible is a string, no conversion neccasary
                       GlobalVaribles.SetProperty(VaribleLookUp[i, 1].ToString(), MasterReadVaribles[i, 0]);
                   }
                   else
                   {
                       //Unsuported varible type
                       if (ErrorUnsportedDisplayed != true)
                       {
                           MessageBox.Show("Error in reading the selected settings file, The software is not configuired correctly. \n Procced at your own risk!");
                           // Dont display this error again:
                           ErrorUnsportedDisplayed = true;
                       }
                      ErrorReporter.ErrorHandaling("Unable to identify the varible type 'string' or 'int' etc. failed at: " + i.ToString() + ", Attempting to find: " + VaribleLookUp[i, 2].ToString() + ", Full varible name: " + VaribleLookUp[i, 1].ToString(), "","UserSettingsHandle");
                   }

                 }
            }
           
            //Save the changes
            GlobalVaribles.SaveReadValues();
            
        }// End Load user settings

        #endregion User Settings load




        #region Erorr Handaling
 //This section has been redacted from this module and centralised.  
        #endregion Erorr Handaling



    }

}
    