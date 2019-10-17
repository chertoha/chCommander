using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;// Add to references -> System.Windows.Forms

namespace chCommander {
    class CMD :Window{
        
        public string currentDirPath{get;set;}
        public string totalText { get; set; }
        public bool exit;

        public CMD() { }
        public CMD(string path) {
            exit = false;
            Console.Clear();
            currentDirPath = path;
            totalText = "chCommander [Version 1.00]\nMade by Chertok Anton.\n\n";
            //totalText += currentDirPath + ">";
            
        }//c-tor

        public void RunCMD() {

            //while (true) {




            //}//while
            
            string command;

            Console.Clear();
            totalText += "\n\n" + currentDirPath + ">";
            Console.Write(totalText);
            

            command = Console.ReadLine();
            totalText += command;

            AnalyzeCommand(command);
            


            //while (true) {
            //    Console.Clear();
            //    Console.Write(totalText);
                
            //    command = Console.ReadLine();
            //    totalText += command;
            //    totalText += AnalyzeCommand(command);

            //    totalText += "\n\n" + currentDirPath + ">";

            //    if (exit) {
            //        break;
            //    }
                


            //}//while
                
            
        }//RunCMD()



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        
        private void AnalyzeCommand(string str) {


            if (str.StartsWith("dir")){
                Dir(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("go")) {
                Go(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("cls")) {
                totalText = "";
                RunCMD();
            }
            else if (str.StartsWith("del")) {
                Del(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("copy")) {
                Copy(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("read")) {
                Read(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("info")) {
                Properties(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("add")) {
                AddDirectory(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("rename")) {
                Rename(currentDirPath, str);
                RunCMD();
            }
            else if (str.StartsWith("help")) {
                Help();
                RunCMD();
            }
            else if (str == "exit") {

            }
            else {
                RunCMD();
            }



        }//AnalyzeCommand()

        private void Help() {


            totalText += "\n";
            totalText += String.Format("\n{0,7}{1}","dir"," - вывод содержимого директории");   
            totalText += String.Format("\n{0,7}{1}", "go", " - перемещение в файловой системе");
            totalText += String.Format("\n{0,7}{1}", "cls", " - очистка экрана");
            totalText += String.Format("\n{0,7}{1}", "del", " - удаление файлов и директорий");
            totalText += String.Format("\n{0,7}{1}", "del!", " - Удаление без подтверждения");
            totalText += String.Format("\n{0,7}{1}", "copy", " - копирование файлов и директорий");
            totalText += String.Format("\n{0,7}{1}", "copy!", " - Копирование без подтверждения");
            totalText += String.Format("\n{0,7}{1}", "read", " - чтение файлов");
            totalText += String.Format("\n{0,7}{1}", "info", " - свойства директорий и файлов");
            totalText += String.Format("\n{0,7}{1}", "add", " - создание директорий");
            totalText += String.Format("\n{0,7}{1}", "rename", " - переименование директорий и файлов");
            totalText += String.Format("\n{0,7}{1}", "help", " - помощь");
            totalText += String.Format("\n{0,7}{1}", "exit", " - выход");

            totalText += "\n\nСправки по командам:\n";
            totalText += String.Format("\n{0,7}{1}","dir/?"," - справка по dir");   
            totalText += String.Format("\n{0,7}{1}","go/?"," - справка по go");
            
            
                       

        }//Help




        //-------------------------------------------------------------------------------------------------------------------------------------------
        //RENAME----------------------------------------------------------------------------------------------------------------------------------------


        private void Rename(string sourcePath, string command) {


            string renameObjPath = "";
            string newPath = "";
            string extra = "";
            


            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')



            if (extra.Contains(">")) {
                string[] extraArr = extra.Split('>');

                if (Directory.Exists(Path.Combine(sourcePath, extraArr[0]))) {
                    renameObjPath = Path.Combine(sourcePath, extraArr[0]);
                }
                else if (File.Exists(Path.Combine(sourcePath, extraArr[0]))) {
                    renameObjPath = Path.Combine(sourcePath, extraArr[0]);
                }
                newPath = Path.Combine(sourcePath, extraArr[1]);

                
                // Question 
                try {
                    if (Directory.Exists(renameObjPath)) {
                        Directory.Move(renameObjPath, newPath);
                    }//
                    else if (File.Exists(renameObjPath)) {
                        File.Move(renameObjPath, newPath);
                    }                    
                }//try
                catch (PathTooLongException e) {
                    totalText += (e.Message);
                }
                catch (IOException e) {
                    totalText+=(e.Message);
                }


            }//if 



        }//Rename



        //RENAME----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        



        //-------------------------------------------------------------------------------------------------------------------------------------------
        //ADD DIRECTORY----------------------------------------------------------------------------------------------------------------------------------------

        private void AddDirectory(string sourcePath, string command) {

            string extra = "";
            string addPath = "";

            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')


            // addPath Relative or Absolute  Dir
            if (Path.IsPathRooted(extra)) {
                addPath = extra;
            }
            else {
                addPath = Path.Combine(sourcePath, extra);
            }
            
            if (Directory.Exists(addPath)) {
                totalText += "\nДиректория уже существует";
            }
            else {
                Directory.CreateDirectory(addPath);
            }
        }//Properties


        //ADD DIRECTORY----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        
        


        //-------------------------------------------------------------------------------------------------------------------------------------------
        //PROPERTIES----------------------------------------------------------------------------------------------------------------------------------------
        
        private void Properties(string sourcePath, string command) {

            string extra = "";
            string propPath = "";

            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')


            // propPath Relative or Absolute  Dir
            if (Path.IsPathRooted(extra) && Directory.Exists(extra)) {
                propPath = extra;
            }
            else if (Directory.Exists(Path.Combine(sourcePath, extra))) {
                propPath = Path.Combine(sourcePath, extra);
            }

            // propPath Relative or Absolute  File
            if (Path.IsPathRooted(extra) && File.Exists(extra)) {
                propPath = extra;
            }
            else if (File.Exists(Path.Combine(sourcePath, extra))) {
                propPath = Path.Combine(sourcePath, extra);                
            }

            try {
                if (Directory.Exists(propPath)) {
                    DirectoryInfo sourceDir = new DirectoryInfo(propPath);
                    DateTime creationDate = Directory.GetCreationTime(propPath);
                    DateTime accessDate = Directory.GetLastAccessTime(propPath);

                    string title = "\n\n\tСвойства: " + sourceDir.Name;
                    string type = "\n\nТип: Директория";
                    string source = "\n\nРасположение: " + Path.GetDirectoryName(propPath);
                    string capacity = "\n\nРазмер: " + String.Format("{0}", RecursionGetFilesCapacity(propPath)) + " Б";
                    string countDirFiles = "\n\nСодержит: Файлов: " + RecursionGetFilesCount(propPath) + ", директорий: " + RecursionGetDirsCount(propPath);
                    string creation = "\n\n--------------------------------------------\nДата создания: " + creationDate.ToShortDateString();
                    string access = "\n\nИзменён: " + accessDate.ToShortDateString() + "\n--------------------------------------------\n";

                    totalText += (title + type + source + capacity + countDirFiles + creation + access);
                }//if (Directory.Exists)
                if (File.Exists(propPath)) {
                    FileInfo file = new FileInfo(propPath);
                    DateTime creationDate = File.GetCreationTime(propPath);
                    DateTime accessDate = File.GetLastAccessTime(propPath);
                    //totalText += propPath;

                    string title = "\n\n\tСвойства: " + file.Name;
                    string type = "\n\nТип: Файл";
                    string source = "\n\nРасположение: " + file.Directory;//Path.GetDirectoryName(sourcePath);
                    string capacity = "\n\nРазмер: " + String.Format("{0}", file.Length ) + " Б";

                    string creation = "\n\n--------------------------------------------\nДата создания: " + creationDate.ToShortDateString();
                    string access = "\n\nИзменён: " + accessDate.ToShortDateString() + "\n--------------------------------------------\n";

                    totalText += (title + type + source + capacity + creation + access);
                }//if (File.Exists)
            }//try
            catch (UnauthorizedAccessException e) {
                totalText+=("\n\n"+e.Message);
            }           
            
        }//Properties

        //PROPERTIES----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        
        

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //READ----------------------------------------------------------------------------------------------------------------------------------------

        private void Read(string sourcePath, string command) {

            string extra = "";
            string readPath = "";

            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')

            // readPath Relative or Absolute  File
            if (Path.IsPathRooted(extra) && File.Exists(extra)) {
                readPath = extra;
            }
            else if (File.Exists(Path.Combine(sourcePath, extra))) {
                readPath = Path.Combine(sourcePath, extra);                
            }

            if (File.Exists(readPath)) {
                ReadFile(readPath);
            }

        }//Read

        //READ----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        
        

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //COPY----------------------------------------------------------------------------------------------------------------------------------------

        private void Copy(string sourcePath, string command) {

            string copyObjPath = "";
            string destinationPath = "";
            string extra = "";
            string extraSymbol = "";

            
            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                if (tempArr[0] == "copy!") {
                    extraSymbol = "!";
                }

                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')


            
            if (extra.Contains(">")) {
                string[] extraArr = extra.Split('>');

                // CopyPath Relative or Absolute  FOLDER 
                if (Path.IsPathRooted(extraArr[0]) && Directory.Exists(extraArr[0])) {
                    copyObjPath = extraArr[0];
                }
                else if (Directory.Exists(Path.Combine(sourcePath, extraArr[0]))) {
                    copyObjPath = Path.Combine(sourcePath, extraArr[0]);
                }

                // CopyPath Relative or Absolute  File
                if (Path.IsPathRooted(extraArr[0]) && File.Exists(extraArr[0])) {
                    copyObjPath = extraArr[0];
                }
                else if (File.Exists(Path.Combine(sourcePath, extraArr[0]))) {
                    copyObjPath = Path.Combine(sourcePath, extraArr[0]);
                    //MessageBox.Show("!!!");
                }


                // destinationPath Relative or Absolute  FOLDER 
                if (Path.IsPathRooted(extraArr[1]) && Directory.Exists(extraArr[1])) {
                    destinationPath = extraArr[1];
                }
                else if (Directory.Exists(Path.Combine(sourcePath, extraArr[1]))) {
                    destinationPath = Path.Combine(sourcePath, extraArr[1]);
                }

                
                // Question 
                try {
                    if (extraSymbol != "!") {
                        Console.Write("\nВы уверенны, что хотите копировть объект? [Y/N]");
                        string yesNo = Console.ReadLine();
                        if (yesNo == "Y" || yesNo == "y") {
                            if (Directory.Exists(copyObjPath)) {
                                RecursionCopyDirsAndFiles(copyObjPath, destinationPath);
                                Console.Write("\nПапка успешно скопирована");
                            }//
                            else if (File.Exists(copyObjPath)) {
                                FileInfo copyFile = new FileInfo(copyObjPath);
                                File.Copy(copyObjPath, Path.Combine(destinationPath, copyFile.Name));
                                Console.Write("\nФайл успешно скопирован");
                            }
                        }//if yesNo Y

                    }//if (extraSymbol != "!")
                    else {
                        if (Directory.Exists(copyObjPath)) {
                            RecursionCopyDirsAndFiles(copyObjPath, destinationPath);
                        }//
                        else if (File.Exists(copyObjPath)) {
                            FileInfo copyFile = new FileInfo(copyObjPath);
                            File.Copy(copyObjPath, Path.Combine(destinationPath, copyFile.Name));
                        }
                    }//else 
                }//try
                catch (PathTooLongException e) {
                    Console.WriteLine(e.Message);
                }
                catch (IOException e) {
                    Console.WriteLine(e.Message);
                }


            }//if 
                
               

                  //totalText += "\n" + copyObjPath + " " + copyPath;

        }//Copy

        
        //COPY----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------

        

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //DEL----------------------------------------------------------------------------------------------------------------------------------------

        private void Del(string sourcePath, string command) {
           
            string delPath="";
            string extra = "";
            string extraSymbol = "";

            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                if (tempArr[0] == "del!") {
                    extraSymbol = "!"; 
                }
                //command = tempArr[0];
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//if (' ')
                       
            // delPath Relative or Absolute  FOLDER 
            if (Path.IsPathRooted(extra) && Directory.Exists(extra)) {
                delPath = extra;
            }
            else if (Directory.Exists(Path.Combine(sourcePath, extra))) {
                delPath = Path.Combine(sourcePath, extra);
            }

            // delPath Relative or Absolute  File
            if (Path.IsPathRooted(extra) && File.Exists(extra)) {
                delPath = extra;
            }
            else if (File.Exists(Path.Combine(sourcePath, extra))) {
                delPath = Path.Combine(sourcePath, extra);
                //MessageBox.Show("!!!");
            }



            try {
                // Question 
                if (extraSymbol != "!") {
                    Console.Write("\nВы уверенны, что хотите удалить объект? [Y/N]");
                    string yesNo = Console.ReadLine();
                    if (yesNo == "Y" || yesNo == "y") {
                        if (Directory.Exists(delPath)) {

                            Directory.Delete(delPath, true);
                            Console.Write("\nПапка успешно удалена");
                        }//
                        else if (File.Exists(delPath)) {
                            FileInfo delFile = new FileInfo(delPath);
                            delFile.IsReadOnly = false;
                            File.Delete(delPath);
                            Console.Write("\nФайл успешно удален");
                        }
                    }//if yesNo Y

                }//if (extraSymbol != "!")
                else {
                    if (Directory.Exists(delPath)) {
                        Directory.Delete(delPath, true);
                    }//
                    else if (File.Exists(delPath)) {
                        FileInfo delFile = new FileInfo(delPath);
                        delFile.IsReadOnly = false;
                        File.Delete(delPath);
                    }
                }//else 

            }//try
            catch (UnauthorizedAccessException e) {
                totalText += "\n" + e.Message;
            }
        }//Del

        //DEL----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        








        //-------------------------------------------------------------------------------------------------------------------------------------------
        //GO----------------------------------------------------------------------------------------------------------------------------------------

        private void Go(string sourcePath, string command) {

            string extra = "";
            //string res = "";

            if (command.Contains(' ')) {
                string[] tempArr = command.Split(' ');
                //command = tempArr[0];
                for (int i = 1; i < tempArr.Length; i++) {
                    if (i != 1) extra += " ";
                    extra += tempArr[i];
                }//for i
            }//else if (' ')
            else if (command.Contains("..")) {
                command = command.Remove(command.IndexOf(".."), 2);
                extra = "..";
            }
            else if (command.Contains("/")) {
                string[] tempArr = command.Split('/');
                extra = tempArr[1];
            }


            if (extra=="root"){
                if (extra == "root") {
                    currentDirPath = Path.GetPathRoot(sourcePath);
                }
            }//if (extra=="root")
            else if (extra == ".."){
                string rootPath = Path.GetPathRoot(sourcePath);
                if (rootPath != sourcePath) {
                    currentDirPath = Path.GetDirectoryName(sourcePath);
                }
            }//else if (extra == "..")
            else if (extra == "?") {
                totalText += "\n\n[folder] - переход в директорию по названию или абсолютному пути";
                totalText += "\nroot - переход в корневую директорию";
                totalText += "\n.. - переход в предыдущую директорию";
                totalText += "\n[diskName:] - переход на выбранный диск";
                totalText += "\n/? - Вывод справки";
            }//else if (extra == "?")
            else {
                //Go Drive
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo i in drives) {
                    if (i.Name == extra) {
                        currentDirPath = i.Name + "\\";
                    }
                }//foreach   

                //Go path
                if (Path.IsPathRooted(extra) && Directory.Exists(extra)) {
                    currentDirPath = extra;
                }
                else if (Directory.Exists(Path.Combine(sourcePath, extra))) {
                    currentDirPath = Path.Combine(sourcePath, extra);
                }
                else {
                    if (extra != "?") currentDirPath = sourcePath;
                }
                

                
            }//else 
            
        }//Go


        //GO----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
        


        //-------------------------------------------------------------------------------------------------------------------------------------------
        //DIR----------------------------------------------------------------------------------------------------------------------------------------
        private void Dir(string sourcePath, string command) {
            string extra = "";
            string res = "";

            if (command.Contains('/')) {
                string[] tempArr = command.Split('/');
                //command = tempArr[0];
                extra = tempArr[1];
            }//if
            
            if (extra == "") {
                res += "\n\n\n Содержимое директории " + sourcePath + "\n";
                DirectoryInfo currentDir = new DirectoryInfo(sourcePath);
                string[] arrFilePaths = Directory.GetFiles(sourcePath);
                DirectoryInfo[] arrDirs = currentDir.GetDirectories();

                foreach (DirectoryInfo i in arrDirs) {
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", i.CreationTime, "<DIR>", "", i.Name);
                }//end foreach
                foreach (string i in arrFilePaths) {
                    FileInfo tempFile = new FileInfo(i);
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                }//end foreach
                res += "\n\n Всего директорий: " + arrDirs.Length + ", файлов: " + arrFilePaths.Length;
            }//if (extra == "")
            else if (extra == "d") {
                res += "\n\n\n Содержимое директории " + sourcePath + "\n";
                DirectoryInfo currentDir = new DirectoryInfo(sourcePath);
                DirectoryInfo[] arrDirs = currentDir.GetDirectories();

                foreach (DirectoryInfo i in arrDirs) {
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", i.CreationTime, "<DIR>", "", i.Name);
                }//end foreach
                res += "\n\n Всего директорий: " + arrDirs.Length;
            }//else if (extra == "d")
            else if (extra == "f") {
                res += "\n\n\n Содержимое директории " + sourcePath + "\n";
                string[] arrFilePaths = Directory.GetFiles(sourcePath);
                foreach (string i in arrFilePaths) {
                    FileInfo tempFile = new FileInfo(i);
                    res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                }//end foreach
                res += "\n\n Всего файлов: " + arrFilePaths.Length;
            }//else if (extra == "f")
            else if (extra.StartsWith(".")) {
                res += "\n\n\n Содержимое директории " + sourcePath + "\n";
                string[] arrFilePaths = Directory.GetFiles(sourcePath);
                int countFiles = 0;
                foreach (string i in arrFilePaths) {
                    FileInfo tempFile = new FileInfo(i);
                    if (Path.GetExtension(i) == extra) {
                        res += String.Format("\n\t{0,-25}{1,-10}{2,-10}{3,-50}", tempFile.CreationTime, "", tempFile.Length / 1024 + " КБ", tempFile.Name);
                        countFiles++;
                    }
                }//end foreach
                res += "\n\n Всего файлов: " + countFiles;
            }// else if (extra.StartsWith("."))
            else if (extra == "?") {
                res += "\n\n/d - Вывод директорий";
                res += "\n/f - Вывод файлов";
                res += "\n/[extension] - Вывод файлов с указанным расширением. Пример -> dir/.exe";
                res += "\n/? - Вывод справки";
            }//else if (extra == "?")     
       
            totalText += res;            
        }//Dir

        //DIR----------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------
       




        

    }// class CMD 
}//namespace chCommander 
