using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;// Add to references -> System.Windows.Forms




namespace chCommander {
    class Window {
        List<string> listWindow;
        List<string> listWindowPerfomance;
        public string root { get; set; }
        public string currentDirectoryPath { get; set; }

        public string properties { get; set; }
        public int indexBeginPerfomance { get; set; }
        public int sizePerfomance { get; set; }
        public int currentIndex { get; set; }
        public bool active { get; set; }
               

        //Constructor with no arguments
        public Window() {}//C-tor

        //C-tor with arguments
        public Window(string path) {
            
            active = false;
            try {
                if (path == "") {
                    this.GetWindowByPath(Directory.GetCurrentDirectory());
                }
                else {
                    this.GetWindowByPath(path);
                }
            }//try
            catch (DirectoryNotFoundException e) {
                MessageBox.Show("Ошибка конфигурации. "+e.Message+"\n\n будут выбраны директории по умолчанию", "error");
                this.GetWindowByPath(Directory.GetCurrentDirectory());
            }
            
        }//c-tor


        //GET LIST ITEM------------------------------------------------
        public  string GetlistWindowPerfomanceItem(int index){
            return listWindowPerfomance[index];
        }//GetlistWindowPerfomanceItem
        





        // WINDOW CREATOR------------------------------------------------
        public void GetWindowByPath(string path) {
            
            indexBeginPerfomance = 0;
            sizePerfomance = 35;
            currentIndex = 0;
            listWindow = new List<string>();
            listWindowPerfomance = new List<string>();
            root = Directory.GetDirectoryRoot(path);
            currentDirectoryPath = path;
            

            string[] arrDir = Directory.GetDirectories(path);
            string[] arrFiles = Directory.GetFiles(path);

            // Get all Disk names
            DriveInfo[] arrDisks = DriveInfo.GetDrives();
            bool tempPathCompare = false;
            foreach (DriveInfo i in arrDisks) {
                if (i.Name == path) {
                    tempPathCompare = true;
                    break;
                }
            }//foreach
            if (!tempPathCompare) {
                listWindow.Add("..");
                listWindowPerfomance.Add("..");  
            }//end if

            //Copy DIR and FILES arrays to listWindow
            for (int i = 0; i < arrDir.Length; i++) {
                listWindow.Add(arrDir[i]);
                string dirName = Path.GetFileName(arrDir[i]);
                dirName = dirName.ToUpper();
                dirName = TrimString(dirName);
                listWindowPerfomance.Add("<DIR> " + dirName);
            }//for i
            for (int i = 0; i < arrFiles.Length; i++) {
                listWindow.Add(arrFiles[i]);
                string fileName = Path.GetFileName(arrFiles[i]);
                fileName = fileName.ToLower();
                fileName = TrimString(fileName);
                listWindowPerfomance.Add("" + fileName);
            }//for i            
            
        }//GetWindowByPath()



        // PERFOMANCE ------------------------------------------------
        public void WindowPerfomanceCalculate() {

            if (listWindowPerfomance.Count < sizePerfomance) {
                sizePerfomance = listWindowPerfomance.Count;
            }

            // RANGE OF PERFOMANCE (SIZE OF RANGE CAN BE CHANGED IN CONSTRUCTORS)
            if (currentIndex >= indexBeginPerfomance + sizePerfomance) {
                indexBeginPerfomance = currentIndex - sizePerfomance + 1;                
            }
            else if (indexBeginPerfomance == 0) {
                
            }
            else if (currentIndex < indexBeginPerfomance && indexBeginPerfomance>0) {
                indexBeginPerfomance = currentIndex ;
            }

        }//WindowPerfomance()






        // KEY EVENTS ----------------------------------------------------------------------------------------------------------

        public void KeyEventsHandler(ConsoleKeyInfo button, string oppositeWindowCurrendDirectoryPath) {

            //NAVIGATION
            if (button.Key == ConsoleKey.LeftArrow) {// LEFT
                currentIndex = 0;
                properties = listWindow[currentIndex];
            }
            if (button.Key == ConsoleKey.RightArrow) {//RIGHT
                currentIndex = listWindow.Count-1;
                properties = listWindow[currentIndex];
            }
            if (button.Key == ConsoleKey.UpArrow && currentIndex!=0) {//UP
                currentIndex--;
                properties = listWindow[currentIndex];
            }
            if (button.Key == ConsoleKey.DownArrow && currentIndex != listWindow.Count-1) {//DOWN
                currentIndex++;
                properties = listWindow[currentIndex];
            }            
            if (button.Key == ConsoleKey.Tab) {//TAB
                this.active = false;
                properties = listWindow[currentIndex];
            }


            //ENTER---------------------------------
            if (button.Key == ConsoleKey.Enter) {//ENTER        
        
                string tempPath = currentDirectoryPath;//for exception

                if (listWindow[currentIndex] == "..") {// BACK TO PREVIOUS FOLDER                   
                    this.GetWindowByPath(Path.GetDirectoryName(currentDirectoryPath));                    
                    this.active = true;
                }//if
                else if (Directory.Exists(listWindow[currentIndex])) {//ENTER FOLDER
                    try {
                        this.GetWindowByPath(listWindow[currentIndex]);
                    }
                    catch (UnauthorizedAccessException) {
                        MessageBox.Show("ДОСТУП ЗАПРЕЩЕН!", "Помощь!");
                        this.GetWindowByPath(tempPath);
                    }
                    this.active = true;
                }//else if
                else if (File.Exists(listWindow[currentIndex])) {// ENTER FILE
                    properties = "FILE";
                    try {
                        Process.Start(listWindow[currentIndex]);
                    }
                    catch (System.ComponentModel.Win32Exception){
                        MessageBox.Show("WIN32 EXCEPTION!", "Info");
                    }
                }//else if

            }//ENTER-----------------------------


            //F1-F10
            if (button.Key == ConsoleKey.F1 && (button.Modifiers & ConsoleModifiers.Alt) == 0) {//F1 - Help
                string F1Text = "F1 - Помощь\nF2 - Командная строка\nF3 - Чтение файла\nF4 - Свойства паки/файла\nF5 - Копирование\nF6 - Переименование\nF7 - Создать директорию\nF8 - Удаление\n\nALT+F1 - Выбор диска\n\nEsc - Выход";
                MessageBox.Show(F1Text, "Помощь!");
            }//F1
            if (button.Key == ConsoleKey.F2) {//F2 - CMD
                CMD cmd = new CMD(currentDirectoryPath);
                cmd.RunCMD();
                
            }//F2
            if (button.Key == ConsoleKey.F3) {//F3 - Read File
                if (File.Exists(listWindow[currentIndex])) {
                    ReadFile(listWindow[currentIndex]);                    
                    this.GetWindowByPath(currentDirectoryPath);                    
                }
            }//F3
            if (button.Key == ConsoleKey.F4) {//F4 - Properties
                GetPropertiesDirAndFile(listWindow[currentIndex]);
            }//F4
            if (button.Key == ConsoleKey.F5) {//F5 - Copy 
                if (listWindow[currentIndex] != "..") {
                    CopyDirAndFile(listWindow[currentIndex], oppositeWindowCurrendDirectoryPath);
                    this.GetWindowByPath(currentDirectoryPath);
                }
            }//F5
            if (button.Key == ConsoleKey.F6) {//F6 - Rename
                if (listWindow[currentIndex] != "..") {
                    RenameDirAndFile(listWindow[currentIndex]);

                    this.GetWindowByPath(currentDirectoryPath);
                }                
            }//F6

            if (button.Key == ConsoleKey.F7) {//F7 - Create Folder
                CreateNewDir(currentDirectoryPath);
                this.GetWindowByPath(currentDirectoryPath);
            }//F7
            if (button.Key == ConsoleKey.F8) {//F8 - Delete
                if (listWindow[currentIndex] != "..") {
                    DeleteDirAndFile(listWindow[currentIndex]);
                    this.GetWindowByPath(currentDirectoryPath);
                }                
            }//F8



            // COMBINATION ALT+
            if ((button.Modifiers & ConsoleModifiers.Alt) != 0 && button.Key == ConsoleKey.F1) {//ALT+F1
                string tempPath = currentDirectoryPath;//for exception
                string newPath = ChooseDisk();
                try{
                    this.GetWindowByPath(newPath);
                }//try
                catch (IOException) {
                    MessageBox.Show("Устройство не готово!","Info");
                    this.GetWindowByPath(tempPath);
                }//
                
            }//ALT+F1



        }//KeyEventsHandler()---------------------------------------------------------------------------------------




        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        // FUNCTIONS---------------------------------------------------------------------------------------


        //Properties of Folders and Files F4
        private void GetPropertiesDirAndFile(string sourcePath) {


            try {
                if (Directory.Exists(sourcePath)) {
                    DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
                    DateTime creationDate = Directory.GetCreationTime(sourcePath);
                    DateTime accessDate = Directory.GetLastAccessTime(sourcePath);

                    string title = "Свойства: " + sourceDir.Name;
                    string type = "Тип: Директория";
                    string source = "\n\nРасположение: " + Path.GetDirectoryName(sourcePath);
                    string capacity = "\n\nРазмер: " + String.Format("{0}", RecursionGetFilesCapacity(sourcePath)) + " Б";
                    string countDirFiles = "\n\nСодержит: Файлов: " + RecursionGetFilesCount(sourcePath) + ", директорий: " + RecursionGetDirsCount(sourcePath);
                    string creation = "\n\n--------------------------------------------\nДата создания: " + creationDate.ToShortDateString();
                    string access = "\n\nИзменён: " + accessDate.ToShortDateString() + "\n--------------------------------------------\n";

                    MessageBox.Show(type + source + capacity + countDirFiles + creation + access, title);
                }//if (Directory.Exists)
                if (File.Exists(sourcePath)) {
                    FileInfo file = new FileInfo(sourcePath);
                    DateTime creationDate = File.GetCreationTime(sourcePath);
                    DateTime accessDate = File.GetLastAccessTime(sourcePath);


                    string title = "Свойства: " + file.Name;
                    string type = "Тип: Файл";
                    string source = "\n\nРасположение: " + file.Directory;//Path.GetDirectoryName(sourcePath);
                    string capacity = "\n\nРазмер: " + String.Format("{0}", file.Length) + " Б";

                    string creation = "\n\n--------------------------------------------\nДата создания: " + creationDate.ToShortDateString();
                    string access = "\n\nИзменён: " + accessDate.ToShortDateString() + "\n--------------------------------------------\n";

                    MessageBox.Show(type + source + capacity + creation + access, title);
                }//if (File.Exists)
            }
            catch (UnauthorizedAccessException e) {
                MessageBox.Show(e.Message,"Error");
            }
        }//GetPropertiesDirAndFile

        protected double RecursionGetFilesCapacity(string sourcePath) {
            double capacityFiles=0;
            string[] arrFilePaths = Directory.GetFiles(sourcePath);
            List <FileInfo> arrFiles= new List<FileInfo>();

            for(int i=0;i<arrFilePaths.Length;i++){
                arrFiles.Add(new FileInfo(arrFilePaths[i]));
                capacityFiles+=arrFiles[i].Length;
            }//for i

            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] arrFolders = sourceDir.GetDirectories();
                        
            foreach (DirectoryInfo i in arrFolders) {
                string tempSourcePath = Path.Combine(sourcePath, i.Name);
                RecursionGetFilesCapacity(tempSourcePath);
            }//end foreach

            return capacityFiles;
        }//RecursionGetFilesCapacity()

        protected int RecursionGetFilesCount(string sourcePath) {
            int countFiles = 0;
            string[] arrFilePaths = Directory.GetFiles(sourcePath);
            countFiles += arrFilePaths.Length;
            
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] arrFolders = sourceDir.GetDirectories();

            foreach (DirectoryInfo i in arrFolders) {
                string tempSourcePath = Path.Combine(sourcePath, i.Name);
                countFiles+=RecursionGetFilesCount(tempSourcePath);
            }//end foreach

            return countFiles;
        }//RecursionGetFilesCapacity()

        protected int RecursionGetDirsCount(string sourcePath) {
            int countDirs = 0;           

            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] arrFolders = sourceDir.GetDirectories();
            countDirs += arrFolders.Length;

            foreach (DirectoryInfo i in arrFolders) {
                string tempSourcePath = Path.Combine(sourcePath, i.Name);
                countDirs+=RecursionGetDirsCount(tempSourcePath);
            }//end foreach

            return countDirs;
        }//RecursionGetFilesCapacity()



        // ReadOnly Files F3
        protected void ReadFile(string soursePath) {
            Console.Clear();
            FileStream fs = new FileStream(soursePath,FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            
            string fileStr ="";

            int countLines = 0;
            string line=" ";
            while (true) {
                line = sr.ReadLine();
                fileStr +=line+"\n";
                if (line != null) {                    
                    countLines++;
                }
                else {
                    break;
                }               
            }//while
            //Console.WriteLine(countLines);
            if (countLines + 20 > 50) {
                Console.SetBufferSize(120, countLines+20);
            }            
            Console.Write(fileStr);                       
            sr.Dispose();

            if (countLines + 20 > 50) {
                Console.WriteLine("\n\n\nБуфер консоли увеличен для удобства чтения файла");
            }           
            Console.WriteLine("\nДля выхода нажмите Esc");

            while (true) {
                ConsoleKeyInfo button = Console.ReadKey();
                if (button.Key == ConsoleKey.Escape) {
                    Console.Clear();
                    Console.SetBufferSize(120, 50);
                    break;
                }
            }//while    
            
            
        }//ReadFile()

        
        //Rename folder or file F6
        private void RenameDirAndFile(string sourcePath) {

            string newPath = Path.GetDirectoryName(sourcePath);

            if (Directory.Exists(sourcePath)){
                //DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);                
                Console.Clear();
                Console.WriteLine();
                Console.Write(String.Format("{0,10}", "Введите новое название директории: "));
                string newDirName = Console.ReadLine();
                try {
                    Directory.Move(sourcePath, Path.Combine(newPath, newDirName));
                }
                catch(IOException e) {
                    MessageBox.Show(e.Message, "Info");
                }
            }//if (Directory.Exists)
            else if (File.Exists(sourcePath)) {

                while (true) {
                    Console.Clear();
                    Console.WriteLine();
                    Console.Write(String.Format("{0,10}", "Введите новое название файла: "));
                    string newFileName = Console.ReadLine();


                    bool goOut = false;
                    if (Path.GetExtension(Path.Combine(newPath, newFileName)) == "") {
                        if (YesNoMenu("\nВы уверенны, что хотите оставить файл без расширения?\n")) {
                            File.Move(sourcePath, Path.Combine(newPath, newFileName));
                            goOut = true;
                        }
                    }// if no extension
                    else {
                        File.Move(sourcePath, Path.Combine(newPath, newFileName));
                        break;
                    }
                    
                    if (goOut) {
                        break;
                    }                    
                }//while                
            }// else if (File.Exists)

        }//RenameDirAndFile


        //Copy Folder or File F5
        private void CopyDirAndFile(string sourcePath, string destinationPath) {

            DirectoryInfo dirName = new DirectoryInfo(sourcePath);

            if (File.Exists(Path.Combine(destinationPath, Path.GetFileName(sourcePath)))) {// if file already exists at the destination folder
                MessageBox.Show("Файл уже сущестует по адресу назначения!", "Info");
            }//if already exists
            else if (Directory.Exists(Path.Combine(destinationPath,dirName.Name))) {// if file already exists at the destination folder
                MessageBox.Show("Директория уже сущестует по адресу назначения!", "Info");
            }//if already exists
            else if (File.Exists(sourcePath)) {
                string fileName=Path.GetFileName(sourcePath);

                if (YesNoMenu(String.Format("{0,10}{1,10}{2,10}{3,10}", "Вы уверенны, что хотите копировать:\n\n", fileName, "\n\nИЗ: " + Path.GetDirectoryName(sourcePath), "\n\nВ: " + destinationPath))) {
                    File.Copy(sourcePath, Path.Combine(destinationPath, fileName));
                    MessageBox.Show("Файл  успешно скопирован!", "Info");
                }
            }//if (File.Exists)
            else if (Directory.Exists(sourcePath)) {
                if (YesNoMenu(String.Format("{0,10}{1,10}{2,10}{3,10}", "Вы уверенны, что хотите копировать директорию:\n\n", dirName.Name, "\n\nИЗ: " + Path.GetDirectoryName(sourcePath), "\n\nВ: " + destinationPath))){
                    RecursionCopyDirsAndFiles(sourcePath, destinationPath);
                    MessageBox.Show("Директория и всё её содержимое успешно скопирована!", "Info");
                }                      
            }//else if (Directory.Exists)



        }//CopyDirAndFile
        protected void RecursionCopyDirsAndFiles(string sourcePath, string destinationPath) {
            //string dirName = Path.GetDirectoryName(sourcePath);
            string[] arrFiles = Directory.GetFiles(sourcePath);
            //string[] arrFolders = Directory.GetDirectories(sourcePath);

            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);            
            DirectoryInfo[] arrFolders = sourceDir.GetDirectories();

            Directory.CreateDirectory(Path.Combine(destinationPath, sourceDir.Name));

            foreach (string i in arrFiles) {
                File.Copy(i, Path.Combine(destinationPath,sourceDir.Name, Path.GetFileName(i)));
            }//end foreach
            foreach (DirectoryInfo i in arrFolders) {
                string tempSourcePath = Path.Combine(sourcePath, i.Name);
                string tempDestinationPath = Path.Combine(destinationPath, sourceDir.Name);
                RecursionCopyDirsAndFiles(tempSourcePath, tempDestinationPath);
            }//end foreach

        }//RecursionCopyDirsAndFiles


        // Change Disk ALT+F1
        private string ChooseDisk() {
            string newPath="";
            int indexDisk = 0;
            DriveInfo[] arrDisks = DriveInfo.GetDrives();  

            while (true) {
                Console.Clear();
                Console.WriteLine(String.Format("{0,60}","Выбор диска\n"));
                
                // Get all Disk names
                            
                for (int i=0;i<arrDisks.Length;i++){
                    if (indexDisk == i) {
                        Console.WriteLine(String.Format("{0,52}{1,3}{2,0}","► ", arrDisks[i].Name," ◄"));
                    }
                    else {
                        Console.WriteLine(String.Format("{0,55}", arrDisks[i].Name));
                    }                    
                }//for i

                ConsoleKeyInfo button = Console.ReadKey();
                if (button.Key == ConsoleKey.UpArrow) {
                    if (indexDisk == 0) indexDisk = arrDisks.Length;
                    indexDisk--;
                }
                if (button.Key == ConsoleKey.DownArrow) {
                    if (indexDisk == arrDisks.Length - 1) indexDisk = -1;
                    indexDisk++;
                }
                if (button.Key == ConsoleKey.RightArrow) {
                    indexDisk = arrDisks.Length - 1;
                }
                if (button.Key == ConsoleKey.LeftArrow) {
                    indexDisk = 0;
                }
                if (button.Key == ConsoleKey.Enter) {
                        newPath = arrDisks[indexDisk].Name;
                        break;
                }
                
            }//while

            return newPath;
        }//ChooseDisk
        

        //Create Folder F7
        private void CreateNewDir(string path) {

            Console.Clear();
            Console.WriteLine("Введите название новой директории:");
            string newDirName = Console.ReadLine();
            path += @"\"+newDirName;
            if (Directory.Exists(path)) {
                MessageBox.Show("Каталог уже существует", "Info");
            }
            else {
                Directory.CreateDirectory(path);
            }
            
        }//CreateNewDir()

        //Delete F8
        private void DeleteDirAndFile(string path) {
            
            if (Directory.Exists(path)) {
                if (YesNoMenu("\nВы уверенны, что хотите удалить директорию и всё её содержимое???\n")) {
                    Directory.Delete(path, true);
                    MessageBox.Show("Папка успешно удалена!", "Info");
                }
            }//if DIR
            else if (File.Exists(path)) {
                if (YesNoMenu("\nВы уверенны, что хотите удалить файл???\n")) {
                    FileInfo delFile = new FileInfo(path);
                    delFile.IsReadOnly = false;
                    File.Delete(path);
                    MessageBox.Show("Файл успешно удалён!", "Info");                     
                }                
            }//else FILE
        }//DeleteDirAndFile()




        // FUNCTIONS---------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------



        //ADDITION
        private string TrimString (string str) {
            
            if (str.Length >= 40) {
                string temp = "";
                if (str.Contains(".")) {
                    temp = str.Remove(36, str.LastIndexOf('.') - 36);
                }
                else {
                    temp = str.Remove(34, str.Length - 34);
                }
                return temp;
            }
            else {
                return str;
            }
        }//TrimDirString()


        private bool YesNoMenu(string question) {
            bool yesNo = false;
            while (true) {
                Console.Clear();
                Console.WriteLine(question.ToUpper());
                if (!yesNo) {
                    Console.WriteLine(String.Format("{0,10}{1,10}", "► НЕТ ◄", " ДА "));
                }
                else {
                    Console.WriteLine(String.Format("{0,9}{1,12}", " НЕТ ", "► ДА ◄"));
                }
                ConsoleKeyInfo button = Console.ReadKey();
                if (button.Key == ConsoleKey.LeftArrow || button.Key == ConsoleKey.RightArrow) {
                    yesNo = !yesNo;
                }//LeftArrow-RightArrow
                if (button.Key == ConsoleKey.Enter) {
                    if (yesNo) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }//Enter
            }//while

        }//YesNoMenu


    }//Window[][][][]][][][][][][]][][][][][][]][][][][][][]][][][][][][]][][][][][][]][][][][][][]][][]
}//namespace
