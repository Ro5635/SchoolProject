using System;
using System.IO;


class TestApp
{

    static void Main(string[] args){

Console.WriteLine("hello");
Console.ReadKey();
Console.WriteLine("You are about to read a text file:");
Console.ReadKey();



using(StreamReader ReadHandle = new StreamReader("longfile.txt")){

	//the bellow methord would work better for extreme size files
	//int i = 0;
	//string[] DataIn  = new string[255];
	//while (ReadHandle.ReadLine() != null){
	//DataIn[i] = ReadHandle.ReadLine();
	//i++;
	//}

    string Data = ReadHandle.ReadToEnd();
    
    foreach (string line in Data.Split('\n'))
    {
        Console.WriteLine("Line " + line);
    }

}




}
}