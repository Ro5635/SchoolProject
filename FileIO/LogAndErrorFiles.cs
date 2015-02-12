using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class LogAndErrorFiles : FileHandaling 
    {
        //Create the file Handles
        FileHandaling LogFileHandle = new FileHandaling();
        FileHandaling ErrorFileHandle = new FileHandaling();

        //Are the Logs Created (need initilising?)?
        Boolean CreatedErrorReporterfiles = false;

        #region Log and Error File Set up

        public void InitialiseFiles(string ClassName)
        {
            // Find The application Run ID this will be used to find the folder name to contain log and error files
            // once found the setting will be incremented to move to curent run ID


            int SettingRunID = Properties.Settings.Default.RunID;

            //Directory to use, taking the root directory from the settings file
            string FileDirectory = Properties.Settings.Default.FileDirectory.ToString() + SettingRunID.ToString() + "\\";


            

            //Create The directory
            LogFileHandle.CreateDirectory(FileDirectory);

            LogFileHandle.InitialiseNewFile("LogFile" + ClassName, FileDirectory.ToString(), ".txt");
            LogFileHandle.FileWrite("This is the logfile:");
            LogFileHandle.FileWrite("");
                        

            //Create The directory
            ErrorFileHandle.CreateDirectory(FileDirectory);

            ErrorFileHandle.InitialiseNewFile("ErrorFile" + ClassName, FileDirectory.ToString(),".txt");
            ErrorFileHandle.FileWrite("All Errors in the sytsem are documented bellow:");
            ErrorFileHandle.FileWrite("");

        }
            #endregion Log and Error File Set up

        #region Write Log File
        public void WriteLogFile(string WriteThis)
        {
            LogFileHandle.FileWrite(WriteThis);
        }
        #endregion Write Log File

        #region Write Error File
        public void ErrorWriteFile(string WriteThis)
        {
            ErrorFileHandle.FileWrite(WriteThis);
        }
        #endregion Write Error File

        #region ErrorHandling
        //This methord is to allow writing to a unique Error file
        public void ErrorHandaling(string Explination, string ErrorText,string ClassName)
        {
            if (CreatedErrorReporterfiles == false)
            {
                // Create the Error reporter object and initilise the files
                InitialiseFiles(ClassName);
                CreatedErrorReporterfiles = true;
            }

            ErrorWriteFile(Explination);
            ErrorWriteFile(ErrorText.ToString());
            ErrorWriteFile("");

        }// End ErrorHandaling

        #endregion ErrorHandaling
    }
}
