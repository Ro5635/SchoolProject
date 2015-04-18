using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileIO
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show("Notice to User, This Program is in Development. I bear no responsablility for premature death as a result of using this software be that human or machine", "Extreme Warning");

            Boolean DebugMode = false;
          

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            if (DebugMode)
            {

                Application.Run(new Form1());
            }
            else
            {
                //Application.Run(new SplashScreen());

                Application.Run(new LevelSelection());
                Application.Run(new TaskSelection());
            }
            
            //SplashScreen SplashScreenOpen = new SplashScreen();
            //SplashScreenOpen.Show();
            

            /*
             * Start Up routine
             *

            CommunicationsControl ProgramCommunicationsControl = new CommunicationsControl();
            ProgramCommunicationsControl.FileOperationsInitialise();
            */

            
            
            

        }

        



    }
}
