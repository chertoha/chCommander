using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chCommander {
    static class Print {

        //PRINT ARRAYS
        public static void ShowArray(string[]arr) {
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine();
        }//ShowArray(string)
        public static void ShowArray(int[] arr) {
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine();
        }//ShowArray(int)
        public static void ShowArray(double[] arr) {
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine();
        }//ShowArray(double)
        public static void ShowArray(char[] arr) {
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine();
        }//ShowArray(char)


        // PRINT COLLECTIONS
        public static void ShowList(List<string> list) {
            //Console.WriteLine();
            foreach (string i in list) {
                Console.Write(i);
                //Console.WriteLine();
            }
            //Console.WriteLine();
        }//ShowList
        


    }//class Print
}//namespace chCommander
