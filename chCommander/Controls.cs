using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace chCommander {
    static class Controls {

        static string res;
        public delegate void DirMethods(string soursePath, string extra);
        public delegate void GoMethods(string soursePath, string extra);


        public static string GetResultDir(string currentDirectoryPath, string extra) {
            res = "";
            DirMethods delDir = new DirMethods(Controls.Dir);
            delDir += Controls.DirFiles;
            delDir += Controls.DirFolders;
            delDir += Controls.DirFilesExtension;
            delDir += Controls.DirHelp;

            delDir(currentDirectoryPath, extra);

            return res;
        }//GetResult

        public static string GetResultGo(string currentDirectoryPath, string extra) {
            res = "";
            GoMethods delGo = new GoMethods(Controls.Go);
            delGo += Controls.GoRoot;
            delGo += Controls.GoUp;
            delGo += Controls.GoDrive;
            delGo += Controls.GoHelp;

            delGo(currentDirectoryPath, extra);

            return res;
        }//GetResult

        public static string GetResult;









        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----
        // go//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----


        private static void Go(string soursePath, string extra) {
            if (Path.IsPathRooted(extra) && Directory.Exists(extra)) {
                 res= extra;
            }
            else if (Directory.Exists(Path.Combine(soursePath, extra))) {
                res= Path.Combine(soursePath, extra);
            }
            else {
                if (extra != "?") res = soursePath;
            }
        }//Go
        
        private static void GoRoot(string soursePath, string extra) {
            if (extra == "root") {
                res = Path.GetPathRoot(soursePath);
            }
        }//GoRoot
        

        private static void GoUp(string soursePath, string extra) {
            string root = Path.GetPathRoot(soursePath);
            if (extra == ".." && root!=soursePath) {
                res = Path.GetDirectoryName(soursePath);
            }
            
        }//GoUp
        

        private static void GoDrive(string soursePath, string extra) {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo i in drives) {
                if (i.Name == extra) {
                    res = i.Name + "\\";
                }
            }//foreach           
        }//GoDrive
        

        private static void GoHelp(string soursePath, string extra) {
            //res = "";
            if ((extra == "?")) {
                res += "\n\n[path/folder] - Перемещение ро указанному пути/в указанную папку. Пример -> go c:\\windows / go windows";
                res += "\nroot - Перемещение в корень. Пример -> go root";
                res += "\n.. - Перемещение в предыдущую директорию";
                res += "\n[drive:] - Перемещение на указанный диск. Пример -> go C:";
                res += "\n/? - Вывод справки";
            }                    
        }//GoHelp

        // go//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----
        //-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----





        //-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----
        // dir//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----
        
        // simple dir
        private static void Dir(string soursePath, string extra) {
            if (extra != "") {
                res += ""; 
            }
            else {
                res += "\n\n\n Содержимое директории " + soursePath + "\n";
                DirectoryInfo currentDir = new DirectoryInfo(soursePath);
                string[] arrFilePaths = Directory.GetFiles(soursePath);
                DirectoryInfo[] arrDirs = currentDir.GetDirectories();

                foreach (DirectoryInfo i in arrDirs) {
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", i.CreationTime, "<DIR>", "", i.Name);
                }//end foreach
                foreach (string i in arrFilePaths) {
                    FileInfo tempFile = new FileInfo(i);
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                }//end foreach
                res += "\n\n Всего директорий: " + arrDirs.Length + ", файлов: " + arrFilePaths.Length;
            }//else
        }//Dir




        // dir/f
        private static void DirFiles(string soursePath, string extra) {
            if (extra != "f") {
                res += ""; 
            }
            else {
                res += "\n\n\n Содержимое директории " + soursePath + "\n";
                string[] arrFilePaths = Directory.GetFiles(soursePath);
                foreach (string i in arrFilePaths) {
                    FileInfo tempFile = new FileInfo(i);
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                }//end foreach
                res += "\n\n Всего файлов: " + arrFilePaths.Length;
            }
        }//DirFiles




        // dir/d
        private static void DirFolders(string soursePath, string extra) {
            if (extra != "d") {
                res += "";
            }
            else {
                res += "\n\n\n Содержимое директории " + soursePath + "\n";
                DirectoryInfo currentDir = new DirectoryInfo(soursePath);
                DirectoryInfo[] arrDirs = currentDir.GetDirectories();

                foreach (DirectoryInfo i in arrDirs) {
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", i.CreationTime, "<DIR>", "", i.Name);
                }//end foreach
                res += "\n\n Всего директорий: " + arrDirs.Length;
            
            } 
                       
        }//DirFolders



        // dir/.exe
        private static void DirFilesExtension(string soursePath, string extra) {
            

            if (!extra.StartsWith(".")) {
                res += "";
            }
            else {                
                res += "\n\n\n Содержимое директории " + soursePath + "\n";
                string[] arrFilePaths = Directory.GetFiles(soursePath);
                int countFiles = 0;
                foreach (string i in arrFilePaths) {                    
                    FileInfo tempFile = new FileInfo(i);
                    if (Path.GetExtension(i) == extra) {
                        res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                        countFiles++;
                    }
                }//end foreach
                res += "\n\n Всего файлов: " + countFiles;
            }
        }//DirFiles


        private static void DirHelp(string soursePath, string extra) {


            if ((extra != "?")) {
                res += "";
            }
            else {
                res += "\n\n/d - Вывод директорий";
                res += "\n/f - Вывод файлов";
                res += "\n/[extension] - Вывод файлов с указанным расширением. Пример -> dir/.exe";
                res += "\n/? - Вывод справки";
            }
        }//DirFiles








        // dir//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----
        //-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----//-----





    }//class Controls
}//namespace chCommander
