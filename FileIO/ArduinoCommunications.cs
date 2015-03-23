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
        int[,] VarableStatus = new int[500,2];
        string 


    }
}
