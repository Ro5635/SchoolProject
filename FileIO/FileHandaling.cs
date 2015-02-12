using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class FileHandaling
    {
        /*
         * This is the class that will control all FIle IO to the hard drive, specificly it is responsable for:
         * 
         * --Settings read / write
         * --Log File
         * --Error Log
         * 
         * Note: Error handaling here is to the console as writing errors to logs would use this code
         * 
         * */

        #region  File Handaling
        // This section handales all IO for the log files

        //Set up the log file, should be ran during the start up proccess.
        #region CreateFile
        
        // Bellow is declaring the varible so it can be accessed between the methords.
        public string FileFullPath;
        public string FIleExtension = ".txt";

        public  void InitialiseNewFile(string CreateFileName, string CreateFileLocation, string FileExtension)
        {



            // Define the Log file location and name stem
            string LogFile = CreateFileLocation; //"C:\\Deleateme\\";
            string LogFileName = CreateFileName; //"LogFile";
            FileFullPath = LogFile + LogFileName;
            byte i = 0;

            //Check to see if name is allready used, if it is then find new name
            if (System.IO.File.Exists(FileFullPath + FileExtension))
            {

               
                string LogFileFullPathtemp;

                do
                {
                    i++;
                    LogFileFullPathtemp = LogFile + LogFileName + i.ToString();
                } while (System.IO.File.Exists(LogFileFullPathtemp + FileExtension));

                FileFullPath = LogFile + LogFileName + i.ToString();

            }// End IF - File Exists




            // Create the Log File

            try
            {


                using (System.IO.File.Create(FileFullPath + FileExtension))
                {
                    System.IO.File.Create(FileFullPath + FileExtension);
                }
                                
            }// End Try

            catch (Exception ErrorText)
            {
                Console.WriteLine("Error in creating the log file at:" + FileFullPath + FileExtension);
                Console.WriteLine(ErrorText.ToString());
            }// End Catch Error


            //Write successful creation to the log file
            FileWrite("File Created at: " + System.DateTime.Now.ToString());

        }// end methord InitialiseNewLogFile

        #endregion CreateFile

        #region CreateDirectory
        //Create the directory that is needed
        public void CreateDirectory(string DirectoryLocation)
        {
            try
            {
                System.IO.Directory.CreateDirectory(DirectoryLocation);
            }
            catch (Exception Errortext){

                Console.WriteLine("Failed to create the directory" + DirectoryLocation);
                Console.WriteLine(Errortext);
            }

        }//End Create Directory

        #endregion CreateDirectory


        #region WriteFile

        // Public accessable methord to write text to the log file
        public void FileWrite(string StringToWrite)
        {
            // Append new text to an existing file. 
            // The using statement automatically closes the stream and calls  
            // IDisposable.Dispose on the stream object. 

            try
            {
                using (System.IO.StreamWriter LogFileStream = new System.IO.StreamWriter((FileFullPath + FIleExtension), true))
                {
                    LogFileStream.WriteLine(StringToWrite);


                }

            }
            catch (Exception ErrorText) {
                Console.WriteLine("Failed to write to the file");
                Console.WriteLine(ErrorText);



            }
            

        }// End FileWrite

        #endregion WriteFile


        #region ReadFileAll
        public string ReadText(string FileToRead)
        {
            if (FileToRead != null && FileToRead != "")
            {
                using (System.IO.StreamReader FileReader = new System.IO.StreamReader(FileToRead))
                {
                    return FileReader.ReadToEnd();
                }
            }

            return "Error";
            
        }
        #endregion ReadFileAll

        #endregion File Handaling

    }
}
