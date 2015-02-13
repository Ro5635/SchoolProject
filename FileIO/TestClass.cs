using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class TestClass
    {

        public void TestFunctions(){
            for(int i = 1; i < 50; i++){
            System.Console.WriteLine("!!!!!!");
            }

            GlobalVar GlobalAccess = new GlobalVar();
            GlobalAccess.SerialPortsActiveRobotsSetValue(5, 2, "Test");
            Console.WriteLine("Data Written to var, read as bellow:");
            Console.WriteLine(GlobalAccess.SerialPortsActiveRobots(5,2));
            GlobalVar GlobalsAccess2 = new GlobalVar();
            Console.Write("From Second instance !!: ");
            Console.Write(GlobalsAccess2.SerialPortsActiveRobots(5, 2));
            Console.Write("  :AAAA");
        
        }


    }
}
