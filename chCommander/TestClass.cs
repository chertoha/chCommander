using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace chCommander {
    static class TestClass {

        

        public static void ShowCommander() {


            //top
            for (int i = 0; i < 120; ) {

                if (i == 30) {
                    Console.Write(@"C:\");
                    i += 3;
                }
                if (i == 90) {
                    Console.Write(@"D:\");
                    i += 3;
                }
                else {
                    Console.Write("*");
                    i++;
                }
            }//for i


            //middle
            for (int i = 0; i < 45; i++) {
                Console.WriteLine();
            }//for i



            Console.Write(@"C:\>");

            Console.WriteLine();
        }//ShowCommander



        public static void ShowDir() {

            

            string currentPath = Directory.GetCurrentDirectory();
            string[] arrDir = Directory.GetDirectories(currentPath);
            string[] arrFiles = Directory.GetFiles(currentPath);


            //Console.WriteLine(currentPath);
            

            Print.ShowArray(arrDir);
            Print.ShowArray(arrFiles);
            
        }//ShowDir()

        





    }//class TestClass 
}//namespace chCommander
