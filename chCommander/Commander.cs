using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace chCommander {
    static class Commander {


        public static void RunCommander() {

            xmlConfig config = new xmlConfig();

            //Create left & right windows

            Window leftWindow = new Window(config.leftWindowStartPath);
            Window rightWindow = new Window(config.rightWindowStartPath);
           
                     
            leftWindow.active = config.leftWindowActive;
            rightWindow.active = !leftWindow.active;
    


            //leftWindow.ShowWindow();
            //rightWindow.ShowWindow();

            List<string> totalList = new List<string>();
            List<string> leftWin = new List<string>();
            List<string> rightWin = new List<string>();


            

            //GLOBAL WHILE
            bool escape = false;
            while (!escape) {


                Console.Clear();

                // SHOW PERFOMANCE
                leftWindow.WindowPerfomanceCalculate();
                rightWindow.WindowPerfomanceCalculate();



                for (int i = leftWindow.indexBeginPerfomance; i < leftWindow.indexBeginPerfomance + leftWindow.sizePerfomance; i++) {
                    if (i != leftWindow.currentIndex || !leftWindow.active) {
                        leftWin.Add(leftWindow.GetlistWindowPerfomanceItem(i));
                    }
                    else if (leftWindow.active) {
                        leftWin.Add("►" + leftWindow.GetlistWindowPerfomanceItem(i) + "◄");
                    }
                }//for i

                Console.WriteLine();

                for (int i = rightWindow.indexBeginPerfomance; i < rightWindow.indexBeginPerfomance + rightWindow.sizePerfomance; i++) {
                    if (i != rightWindow.currentIndex || !rightWindow.active) {
                        rightWin.Add(rightWindow.GetlistWindowPerfomanceItem(i));
                    }
                    else if (rightWindow.active) {
                        rightWin.Add("►" + rightWindow.GetlistWindowPerfomanceItem(i) + "◄");
                    }

                }//for i




                int size = 35;// (leftWindow.sizePerfomance >= rightWindow.sizePerfomance) ? leftWindow.sizePerfomance : rightWindow.sizePerfomance;
                Console.Write("------------------------------------------------------------------------------------------------------------------------");
                Console.Write(String.Format("|{0,28} {1,27}{2,30}{3,33}", leftWindow.root,"|" ,rightWindow.root,"|"));
                Console.Write("------------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < size; i++) {
                    totalList.Add("");
                    if (i < leftWin.Count && i < rightWin.Count) {
                        //totalList[i] = leftWin[i].ToString() + "\t" + rightWin[i].ToString();
                        totalList[i] = String.Format("|{0,-55}{1}{2,-55}{3,8}", leftWin[i].ToString(), "|",rightWin[i].ToString(),"|");
                    }
                    else if (i < leftWin.Count && i >= rightWin.Count) {
                        totalList[i] = String.Format("|{0,-55}{1}{2,-55}{3,8}", leftWin[i].ToString(), "|", "", "|");
                    }
                    else if (i < rightWin.Count && i >= leftWin.Count) {
                        totalList[i] = String.Format("|{0,-55}{1}{2,-55}{3,8}", "", "|", rightWin[i].ToString(), "|");
                    }
                    else {
                        totalList[i] = String.Format("|{0,-55}{1}{2,-55}{3,8}", "", "|", "", "|");
                    }
                    
                }//for i                

                Print.ShowList(totalList);
                Console.Write("------------------------------------------------------------------------------------------------------------------------");
                
                //Properties
                if (leftWindow.active) {
                    Console.WriteLine(leftWindow.properties);
                }
                if (rightWindow.active) {
                    Console.WriteLine(rightWindow.properties);
                }        
       
                Console.Write("------------------------------------------------------------------------------------------------------------------------");       
         
                //CMD
                if (leftWindow.active){
                    Console.Write("{0}>",leftWindow.currentDirectoryPath);
                }
                if (rightWindow.active) {
                    Console.Write("{0}>",rightWindow.currentDirectoryPath);
                }
                //string command = Console.ReadLine();

                totalList.Clear();
                leftWin.Clear();
                rightWin.Clear();







                // NAVIGATION
                ConsoleKeyInfo button = Console.ReadKey();


                        // Escape
                if (button.Key == ConsoleKey.Escape) {
                    break;
                }
                
                        //Window events
                if (leftWindow.active && !rightWindow.active) {                    
                    leftWindow.KeyEventsHandler(button, rightWindow.currentDirectoryPath);
                    if (!leftWindow.active) {
                        rightWindow.active = true;
                    }
                }//if
                else if (rightWindow.active && !leftWindow.active) {

                    rightWindow.KeyEventsHandler(button, leftWindow.currentDirectoryPath);
                    if (!rightWindow.active) {
                        leftWindow.active = true;
                    }
                }//else

                

               



            }//GLOBAL WHILE





        }//RunCommander



    }//Commander
}//namespace
