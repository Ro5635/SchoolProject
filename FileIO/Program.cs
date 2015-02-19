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

            
          

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
            Application.Run(new Form1());

            
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
