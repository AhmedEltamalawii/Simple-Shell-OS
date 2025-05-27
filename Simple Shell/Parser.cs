namespace simple_Shell
{
    class Parser
    {
        public void parse_input(string str)
        {
            Token token = new Token();
            var argument = str.Split(' ');
            if (argument.Length == 1)
            {
                token.command = argument[0];
                token.value = null;
                token.sec_value = null;
                action(token);
            }
            else if (argument.Length == 2)
            {
                token.command = argument[0];
                token.value = argument[1];
                token.sec_value = null;
                action(token);
            }
            else if (argument.Length == 3)
            {
                token.command = argument[0];
                token.value = argument[1];
                token.sec_value = argument[2];
                action(token);
            }
        }

        void action(Token token)
        {
            switch (token.command)
            {
                case "cls":
                    if (token.value != null || token.sec_value != null)
                    {
                        Console.WriteLine("Error: 'cls' command does not accept arguments.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Screen cleared.");
                    }
                    break;
                case "quit":
                    if (token.value != null || token.sec_value != null)
                    {
                        Console.WriteLine("error the syntax command");
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
                case "help":
                    Help helper = new Help(token);
                    break;
                case "cd":
                    if (token.value == null)
                    {
                        return;
                    }
                    else if (token.value == ".")
                    {

                    }
                    else
                    {
                        cd(token.value);
                    }
                    break;

                case "md":
                    if (token.value == null)
                    {
                        Console.WriteLine("ERROR, you shold specify folder name to make\n md[path]name");
                        return;
                    }
                    else
                    {
                        md(token.value);
                    }
                    break;
                case "dir":
                    if (token.value == null || token.value == "." || token.value == "..")
                    {
                        dir(Program.current);
                    }

                    else
                    {
                        Directory targetDir = changeMyCurrentDirectory(token.value, false, true);
                        if (targetDir == null)
                        {
                            Console.WriteLine($"Error: The path '{token.value}' is not found.");
                        }
                        else
                        {
                            dir(targetDir);
                        }
                    }
                    break;

                case "rd":
                    if (token.value == null)
                    {
                        Console.WriteLine("ERROR,\n you shold specify folder name to delete\n rd[pah]Name");
                    }
                    else
                    {
                        rd(token.value);
                    }
                    break;
                case "import":
                    if (token.value == null)
                    {
                        Console.WriteLine("ERROR: You should specify the source file to import.\nSyntax: import [source path] [destination name]");
                    }
                    else
                    {
                        Console.WriteLine("Enter the destination file name to save as in the virtual disk (press Enter to use the same name as source):");
                        string destFileName = Console.ReadLine();  // User input

                        if (string.IsNullOrEmpty(destFileName))
                        {
                            destFileName = null;  
                        }

                        import(token.value, destFileName);
                    }
                    break;
                case "type":
                    if (token.value == null || token.sec_value != null)
                    {
                        Console.WriteLine("ERROR\n, you shold specify file name to show its contnet\n type [dest]filename");
                    }
                    else
                    {
                        type(token.value);
                    }
                    break;
                case "export":
                    if (token.value == null || token.sec_value == null)
                    {
                        Console.WriteLine("Error display the syntax of the export command");
                        Console.WriteLine("The Correct syntax is \n import   [Source File] [destination]");
                    }
                    else
                    {
                        export(token.value, token.sec_value);

                    }
                    break;
                case "rename":
                    if (token.value == null || token.sec_value == null)
                    {
                        Console.WriteLine("ERROR,\n");
                        Console.WriteLine("The Correct syntax is \n rename   [old name] [new name]\n");
                    }
                    else
                    {
                        rename(token.value, token.sec_value);
                    }
                    break;
                case "del":
                    if (token.value == null)
                    {
                        Console.WriteLine("Error display the syntax of the del command.");
                        Console.WriteLine("The Correct syntax is \n del   [file name]\n");
                    }
                    else
                    {
                        del(token.value);
                    }
                    break;
                case "copy":
                    if (token.value == null || token.sec_value == null)
                    {
                        Console.WriteLine("ERROR,\n");
                        Console.WriteLine("The Correct syntax is \n copy   [Source File] [destination]\n");
                    }
                    else
                    {
                        copy(token.value, token.sec_value);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown Command..");
                    break;



            }
        }

        public static void type(string name)
        {
            string[] path = name.Split("\\");
            if (path.Length > 1)
            {
                Directory dir = changeMyCurrentDirectory(name, false, false);
                if (dir == null)
                    Console.WriteLine($"The Path {name} Is not exist");
                else
                {
                    name = path[path.Length - 1];
                    int j = dir.searchDirectory(name);
                    if (j != -1)
                    {
                        int fc = dir.entries[j].firs_cluster;
                        int sz = dir.entries[j].dir_fileSize;
                        string content = null;
                        FILE file = new FILE(name, 0x0, fc, dir, content, sz);
                        file.ReadFile();
                        Console.WriteLine(file.content);
                    }
                    else
                    {
                        Console.WriteLine("The System could not found the file specified");
                    }
                }
            }
            else
            {
                int j = Program.current.searchDirectory(name);
                if (j != -1)
                {
                    int fc = Program.current.entries[j].firs_cluster;
                    int sz = Program.current.entries[j].dir_fileSize;
                    string content = null;
                    FILE file = new FILE(name, 0x0, fc, Program.current, content, sz);
                    file.ReadFile();
                    Console.WriteLine(file.content);
                }
                else
                {
                    Console.WriteLine("The System could not found the file specified");
                }
            }
        }

        public static void cd(string path)
        {
            Directory dir = changeMyCurrentDirectory(path, true, false);
            if (dir != null)
            {
                dir.ReadDirectory();
                Program.current = dir;
            }
            else
            {
                Console.WriteLine($"Eroor : path {path} is not exists!");
            }
        }

        public static void moveToDirUsedInAnother(string path)
        {
            Directory dir = changeMyCurrentDirectory(path, false, false);
            if (dir != null)
            {
                dir.ReadDirectory();
                Program.current = dir;
            }
            else
            {
                Console.WriteLine("the system cannot find the specified folder.!");
            }
        }

        private static Directory changeMyCurrentDirectory(string p, bool usedInCD, bool isUsedInRD)
        {
            Directory d = null;
            string[] arr = p.Split('\\');
            string path;
            if (arr.Length == 1)
            {
                if (arr[0] != "..")
                {
                    int i = Program.current.searchDirectory(arr[0]);
                    if (i == -1)
                        return null;//the directory is not found
                    else
                    {
                        string nameOfDiserableFolder = new string(Program.current.entries[i].dir_name);
                        byte attr = Program.current.entries[i].dir_attr;
                        int fisrtcluster = Program.current.entries[i].firs_cluster;
                        d = new Directory(nameOfDiserableFolder, attr, fisrtcluster, Program.current); 
                        d.ReadDirectory();
                        path = Program.currentPath; 
                        path += "\\" + nameOfDiserableFolder.Trim();
                        if (usedInCD)
                            Program.currentPath = path;
                    }
                }
                else 
                {
                    if (Program.current.parent != null)
                    {
                        d = Program.current.parent;
                        d.ReadDirectory();
                        path = Program.currentPath;
                        path = path.Substring(0, path.LastIndexOf('\\'));  
                        if (usedInCD)
                            Program.currentPath = path;
                    }
                    else 
                    {
                        d = Program.current;
                        d.ReadDirectory();
                    }
                }
            }
            else if (arr.Length > 1)
            {

                List<string> ListOfHandledPath = new List<string>();
                for (int i = 0; i < arr.Length; i++)
                    if (arr[i] != " ")
                        ListOfHandledPath.Add(arr[i]);



                Directory rootDirectory = new Directory("M:", 0x10, 5, null);
                rootDirectory.ReadDirectory();


                if (ListOfHandledPath[0].Equals("m:") || ListOfHandledPath[0].Equals("M:"))
                {
                    path = "M:";
                    int howLongIsMyWay;
                    if (isUsedInRD || usedInCD)
                    {
                        howLongIsMyWay = ListOfHandledPath.Count;
                    }
                    else
                    {
                        howLongIsMyWay = ListOfHandledPath.Count - 1;
                    }
                    for (int i = 1; i < howLongIsMyWay; i++)
                    {
                        int j = rootDirectory.searchDirectory(ListOfHandledPath[i]);
                        if (j != -1)
                        {
                            Directory tempOfParent = rootDirectory;
                            string newName = new string(rootDirectory.entries[j].dir_name);
                            byte attr = rootDirectory.entries[j].dir_attr;
                            int fc = rootDirectory.entries[j].firs_cluster;
                            rootDirectory = new Directory(newName, attr, fc, tempOfParent);
                            rootDirectory.ReadDirectory();
                            path += "\\" + newName.Trim();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    d = rootDirectory;
                    if (usedInCD)
                        Program.currentPath = path;
                }
                else if (ListOfHandledPath[0] == "..")
                {
                    d = Program.current;
                    for (int i = 0; i < ListOfHandledPath.Count; i++)
                    {
                        if (d.parent != null)
                        {
                            d = d.parent;
                            d.ReadDirectory();
                            path = Program.currentPath;
                            path = path.Substring(0, path.LastIndexOf('\\'));
                            if (usedInCD)
                                Program.currentPath = path;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                    return null;
            }
            return d;
        }

        public static void md(string name)
        {
            string[] arr = name.Split('\\');
            if (arr.Length == 1)
            {
                if (Program.current.searchDirectory(arr[0]) == -1)
                {
                    DirectoryEntry d = new DirectoryEntry(arr[0], 0x10, 0, 0);

                    if (FAT.GetEmptyCulster() != -1)
                    {
                        Program.current.entries.Add(d);
                        Program.current.WriteDirectory();
                        if (Program.current.parent != null)
                        {
                            Program.current.parent.updateContent(Program.current.getDirectoryEntry());
                            Program.current.parent.WriteDirectory();
                        }
                        FAT.writeFat();
                    }
                    else
                        Console.WriteLine("The Disk is Full :(");
                }
                else
                    Console.WriteLine($"{arr[0]} is aready existed :(");
            }
            else if (arr.Length > 1)
            {
                Directory dir = changeMyCurrentDirectory(name, false, false);
                if (dir == null)
                    Console.WriteLine($"The Path {name} Is not exist");
                else
                {
                    if (FAT.GetEmptyCulster() != -1)
                    {

                        DirectoryEntry d = new DirectoryEntry(arr[arr.Length - 1], 0x10, 0, 0);
                        dir.entries.Add(d);
                        dir.WriteDirectory();
                        dir.parent.updateContent(dir.getDirectoryEntry());
                        dir.parent.WriteDirectory();
                        FAT.writeFat();
                    }
                    else
                        Console.WriteLine("The Disk is Full :(");
                }
            }

        }

        public static void dir(Directory targetDir)//string fileName
        {
            if (targetDir == null)
            {
                Console.WriteLine("Error: No directory to display.");
                return;
            }

            int fc = 0, dc = 0, fz_sum = 0;
            Console.WriteLine("Directory of " + new string(targetDir.dir_name).Trim());
            Console.WriteLine();

            for (int i = 0; i < targetDir.entries.Count; i++)
            {
                var entry = targetDir.entries[i];
                if (entry.dir_attr == 0x0) // File
                {
                    Console.WriteLine($"\t{entry.dir_fileSize} \t {new string(entry.dir_name)}");
                    fc++;
                    fz_sum += entry.dir_fileSize;
                }
                else if (entry.dir_attr == 0x10) // Directory
                {
                    Console.WriteLine($"\t<DIR> {new string(entry.dir_name)}");
                    dc++;
                }
            }

            Console.WriteLine($"{"\t\t"}{fc} File(s)    {fz_sum} bytes");
            Console.WriteLine($"{"\t\t"}{dc} Dir(s)    {VirtualDisk.getFreeSpace()} bytes free");
        }


        public static void rd(string name)
        {


            string[] arr = name.Split('\\');
            Directory dir = changeMyCurrentDirectory(name, false, true);
            if (dir != null)
            {
                Console.Write($"Are you sure that you want to delete {new string(dir.dir_name).Trim()} , please enter Y for yes or N for no:");
                string choice = Console.ReadLine().ToLower();
                if (choice.Equals("y"))
                    dir.deleteDirectory();
            }
            else
                Console.WriteLine($"directory \" {arr[arr.Length - 1]} \" is not exists!");

        }

        public static void import(string sourcePath, string destFileName = null)
        {
            if (File.Exists(sourcePath))
            {
                string content = File.ReadAllText(sourcePath);
                int size = content.Length;

                string[] sourcePathParts = sourcePath.Split("\\");
                string sourceFileName = sourcePathParts[sourcePathParts.Length - 1]; 

                if (string.IsNullOrEmpty(destFileName))
                {
                    destFileName = sourceFileName.Split('.')[0] + ".txt";
                }

                int j = Program.current.searchDirectory(destFileName);
                if (j == -1)
                {
                    int fc = FAT.GetEmptyCulster();
                    if (fc == -1)
                    {
                        Console.WriteLine("The virtual disk is full. Cannot complete the import.");
                        return;
                    }

                    FILE newFile = new FILE(destFileName, 0X0, fc, Program.current, content, size);
                    newFile.writeFile();

                    DirectoryEntry newEntry = new DirectoryEntry(destFileName, 0X0, fc, size);
                    Program.current.entries.Add(newEntry);
                    Program.current.WriteDirectory();

                    Console.WriteLine($"File \"{sourceFileName}\" has been successfully imported as \"{destFileName}\" in the virtual disk.");
                }
                else
                {
                    Console.WriteLine($"The file \"{destFileName}\" already exists in the virtual disk.");
                }
            }
            else
            {
                Console.WriteLine($"The file \"{sourcePath}\" does not exist on your computer.");
            }
        }

        public static void export(string source, string dest)
        {
            string[] path = source.Split("\\");

            if (path.Length > 1)
            {
                Directory dir = changeMyCurrentDirectory(source, false, false);

                if (dir == null)
                {
                    Console.WriteLine($"The path \"{source}\" does not exist in the virtual disk.");
                }
                else
                {
                    source = path[path.Length - 1];  

                    int j = dir.searchDirectory(source);

                    if (j != -1)
                    {
                        if (System.IO.Directory.Exists(dest))
                        {
                            int fc = dir.entries[j].firs_cluster;
                            int sz = dir.entries[j].dir_fileSize;
                            string content = null;
                            FILE file = new FILE(source, 0x0, fc, dir, content, sz);
                            file.ReadFile();

                            StreamWriter sw = new StreamWriter(dest + "\\" + source);
                            sw.Write(file.content);
                            sw.Flush();
                            sw.Close();

                            Console.WriteLine($"File \"{source}\" has been successfully exported to \"{dest}\".");
                        }
                        else
                        {
                            Console.WriteLine($"The system cannot find the path \"{dest}\" specified.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The file \"{source}\" does not exist in the virtual disk.");
                    }
                }
            }
            else
            {
                int j = Program.current.searchDirectory(source);

                if (j != -1)
                {
                    if (System.IO.Directory.Exists(dest))
                    {
                        int fc = Program.current.entries[j].firs_cluster;
                        int sz = Program.current.entries[j].dir_fileSize;
                        string content = null;
                        FILE file = new FILE(source, 0x0, fc, Program.current, content, sz);
                        file.ReadFile();

                        StreamWriter sw = new StreamWriter(dest + "\\" + source);
                        sw.Write(file.content);
                        sw.Flush();
                        sw.Close();

                        Console.WriteLine($"File \"{source}\" has been successfully exported to \"{dest}\".");
                    }
                    else
                    {
                        Console.WriteLine($"The system cannot find the path \"{dest}\" specified.");
                    }
                }
                else
                {
                    Console.WriteLine($"The file \"{source}\" does not exist in the virtual disk.");
                }
            }
        }

        public static void rename(string oldName, string newName)
        {
            string[] path = oldName.Split("\\");
            if (path.Length > 1)
            {
                Directory dir = changeMyCurrentDirectory(oldName, false, false);
                if (dir == null)
                    Console.WriteLine($"The Path {oldName} Is not exist");
                else
                {
                    oldName = path[path.Length - 1];

                    int j = dir.searchDirectory(oldName);
                    if (j != -1)
                    {
                        if (dir.searchDirectory(newName) == -1)
                        {
                            DirectoryEntry d = dir.entries[j];

                            if (d.dir_attr == 0x0)
                            {
                                string[] fileName = newName.Split('.');
                                char[] goodName = getProperFileName(fileName[0].ToCharArray(), fileName[1].ToCharArray());
                                d.dir_name = goodName;
                            }
                            else if (d.dir_attr == 0x10)
                            {
                                char[] goodName = getProperDirName(newName.ToCharArray());
                                d.dir_name = goodName;
                            }



                            dir.entries.RemoveAt(j);
                            dir.entries.Insert(j, d);
                            dir.WriteDirectory();
                        }
                        else
                        {
                            Console.WriteLine("Doublicate File Name exist or file cannot be found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The System Cannot Find the File specified");
                    }

                }
            }
            else
            {
                int j = Program.current.searchDirectory(oldName);
                if (j != -1)
                {
                    if (Program.current.searchDirectory(newName) == -1)
                    {
                        DirectoryEntry d = Program.current.entries[j];



                        if (d.dir_attr == 0x0)
                        {
                            string[] fileName = newName.Split('.');
                            char[] goodName = getProperFileName(fileName[0].ToCharArray(), fileName[1].ToCharArray());
                            d.dir_name = goodName;
                        }
                        else if (d.dir_attr == 0x10)
                        {
                            char[] goodName = getProperDirName(newName.ToCharArray());
                            d.dir_name = goodName;
                        }


                        Program.current.entries.RemoveAt(j);
                        Program.current.entries.Insert(j, d);
                        Program.current.WriteDirectory();
                    }
                    else
                    {
                        Console.WriteLine("Doublicate File Name exist or file cannot be found");
                    }
                }
                else
                {
                    Console.WriteLine("The System Cannot Find the File specified");
                }
            }

        }

        public static void del(string fileName)
        {
            string[] path = fileName.Split("\\");


            Console.WriteLine($"Are you sure you want to delete \"{fileName}\"? (y/n):");
            string confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "y")
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }

            if (path.Length > 1)
            {
                Directory dir = changeMyCurrentDirectory(fileName, false, false);
                if (dir == null)
                {
                    Console.WriteLine($"The Path \"{fileName}\" does not exist.");
                }
                else
                {
                    fileName = path[path.Length - 1];

                    int j = dir.searchDirectory(fileName);
                    if (j != -1)
                    {
                        if (dir.entries[j].dir_attr == 0x0)
                        {
                            int fc = dir.entries[j].firs_cluster;
                            int sz = dir.entries[j].dir_fileSize;

                            FILE file = new FILE(fileName, 0x0, fc, dir, null, sz);
                            file.deleteFile();

                            Console.WriteLine($"File \"{fileName}\" has been deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"The system cannot find the specified file \"{fileName}\".");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the specified file.");
                    }
                }
            }
            else
            {
                int j = Program.current.searchDirectory(fileName);
                if (j != -1)
                {
                    if (Program.current.entries[j].dir_attr == 0x0)
                    {
                        int fc = Program.current.entries[j].firs_cluster;
                        int sz = Program.current.entries[j].dir_fileSize;

                        FILE file = new FILE(fileName, 0x0, fc, Program.current, null, sz);
                        file.deleteFile();

                        Console.WriteLine($"File \"{fileName}\" has been deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the specified file.");
                    }
                }
                else
                {
                    Console.WriteLine("The system cannot find the specified file.");
                }
            }
        }

        private static void DeleteAllFilesInDirectory(Directory dir)
        {
            for (int i = 0; i < dir.entries.Count; i++)
            {
                if (dir.entries[i].dir_attr == 0x0)
                {
                    int fc = dir.entries[i].firs_cluster;
                    int sz = dir.entries[i].dir_fileSize;
                    string fileName = new string(dir.entries[i].dir_name).Trim();

                    FILE file = new FILE(fileName, 0x0, fc, dir, null, sz);
                    file.deleteFile();

                    Console.WriteLine($"File \"{fileName}\" has been deleted.");
                }
            }
        }

        public static void copy(string source, string dest)
        {
            if (source == dest)
            {
                Console.WriteLine("The file cannot be copied onto itself.");
                return;
            }

            int sourceIndex = Program.current.searchDirectory(source);
            if (sourceIndex == -1)
            {
                Console.WriteLine($"The file \"{source}\" does not exist in the current directory.");
                return;
            }

            int fc = FAT.GetEmptyCulster();
            int sz = Program.current.entries[sourceIndex].dir_fileSize;


            Directory destDir = changeMyCurrentDirectory(dest, false, true);
            if (destDir == null)
            {
                Console.WriteLine($"The destination path \"{dest}\" does not exist.");
                return;
            }


            int destIndex = destDir.searchDirectory(source);
            if (destIndex != -1)
            {
                Console.Write($"The file \"{source}\" already exists in the destination. Overwrite? (y/n): ");
                string choice = Console.ReadLine()?.ToLower();
                if (choice != "y")
                {
                    Console.WriteLine("Copy operation canceled.");
                    return;
                }
            }


            int sourceCluster = Program.current.entries[sourceIndex].firs_cluster;
            string content = null;
            FILE sourceFile = new FILE(source, 0x0, sourceCluster, destDir, content, sz);
            sourceFile.ReadFile();
            content = sourceFile.content;


            FILE newFile = new FILE(source, 0x0, fc, destDir, content, sz);
            newFile.writeFile();

            DirectoryEntry newEntry = new DirectoryEntry(source, 0x0, fc, sz);
            destDir.entries.Add(newEntry);
            destDir.WriteDirectory();

            Console.WriteLine($"The file \"{source}\" has been copied to \"{dest}\" successfully.");
        }

        public static char[] getProperFileName(char[] fname, char[] extension)
        {
            char[] dir_name = new char[11];

            int length = fname.Length, count = 0, lenOfEx = extension.Length;
            if (fname.Length >= 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    dir_name[count] = fname[i];
                    count++;
                }
                dir_name[count] = '.';
                count++;

            }
            else if (length < 7)
            {
                for (int i = 0; i < length; i++)
                {
                    dir_name[count] = fname[i];
                    count++;
                }
                for (int i = 0; i < 7 - length; i++)
                {
                    dir_name[count] = '_';
                    count++;
                }
                dir_name[count] = '.';
                count++;
            }
            for (int i = 0; i < lenOfEx; i++)
            {
                dir_name[count] = extension[i];
                count++;
            }
            for (int i = 0; i < 3 - lenOfEx; i++)
            {
                dir_name[count] = ' ';
                count++;
            }
            return dir_name;
        }

        public static char[] getProperDirName(char[] name)
        {
            char[] dir_name = new char[11];

            if (name.Length <= 11)
            {
                int j = 0;
                for (int i = 0; i < name.Length; i++)
                {
                    j++;
                    dir_name[i] = name[i];
                }
                for (int i = ++j; i < dir_name.Length; i++)
                {
                    dir_name[i] = ' ';
                }
            }
            else
            {
                int j = 0;
                for (int i = 0; i < 11; i++)
                {
                    j++;
                    dir_name[i] = name[i];
                }
            }
            return dir_name;
        }


    }

}