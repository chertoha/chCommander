using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace chCommander {
    static class ConsoleConfig {


        

       
        public static void setConfig() {


            xmlConfig config = new xmlConfig();

            

            Console.Clear();
            Console.ResetColor();
            Console.Title = config.title;
                      

           
            //Size & position
            Console.SetWindowSize(120,50);
            Console.SetBufferSize(120,50);
            //ConsoleColor.Cyan
           
            //Console.BackgroundColor = ConsoleColor.Gray;
            //Console.BackgroundColor = ConsoleColor.Cyan;

            Console.BackgroundColor = (ConsoleColor)(config.backGroundColorIndex);
            Console.ForegroundColor = (ConsoleColor)(config.foreGroundColorIndex);

            Console.CursorSize = 10;

            
        }//c-tor

                 


    }//class ConsoleConfig
}//namespace chCommander
