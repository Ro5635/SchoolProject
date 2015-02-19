//The system is desined to operate at a selction of baud rates, these are:
//1200,2400,4800,9600,19200,38400,57600,115200,230400
int BaudRateSerial1 = 9600; //The BaudRate to use for Communication

  boolean syncSuccess = false;  //Sync status
  boolean syncTest = false; //This is used to compleate 3 way handshake
  boolean FinishedInput = false;
  String recivedtring = "";

//PC Access Mode Var's
boolean PCAccessMode = false; 

void setup() {
//Start the Serial Port
  Serial.begin(BaudRateSerial1); 
  //print welcome message
  Serial.println("Arduino Robotic System");
  Serial.println("www.weblink.co.uk");
}


void loop() {
if (PCAccessMode){
//PC Access Mode Enabled, 



}else {
//Standard Run


}

}//end loop

//Read Data If Data is avalable
void serialEvent(){

  while (Serial.available()){

    char recivedchar = (char)Serial.read();
    if (recivedchar == '^') {
      FinishedInput = true;
    }
    if (recivedchar != '^'){
      recivedtring += recivedchar;
    }
  }

   if (FinishedInput) {

    coreCommunication();

      if(PCAccessMode){

        //PC Access mode enabled
        //this is used because there will be a different command set to listen for compared to the standard set
        PCAccessCommunication();

        } else {

        runTimeCommunication(); // standard runtime communicaation

      }
    
    recivedtring = "";
    FinishedInput = false;
      } 
    }

    void coreCommunication(){


      if(recivedtring == "Robot?"){
        //Start Up Communication from Program
        Serial.println("^Arduino_Robot^");
        Serial.println("Entering PC Access Mode");
        PCAccessMode = true;
        } 

    }
    
    

void  runTimeCommunication(){
 }
 

    void PCAccessCommunication(){
        
        

    }

  


    























