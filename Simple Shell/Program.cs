using System;

namespace simple_Shell
{
    public struct Token
    {
        public string command;
        public string value;
        public string sec_value;
    }

    class Program
    {

        

         
        public static string PATH_ON_PC = "E:\\Gam3a Subject\\Semester5\\OperatingSystem\\FAT-system-master\\Simple Shell\\";

        public static Directory current;
        public static string currentPath;
        static void Main(string[] args)
        {
            //VirtualDisk.initialize(PATH_ON_PC);
            VirtualDisk.initialize("miniFat.txt");



            currentPath = new string(current.dir_name);
            currentPath = currentPath.Trim();


            Parser parser = new Parser();


           
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("dddd, MMMM dd, yyyy");
            string currentTime = now.ToString("hh:mm:ss tt");
            string developerName = "Ahmed Sherif | The Visionary Coder";
            string projectVersion = "OS Virtual Disk ";

            Console.Clear(); 

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                      ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║   ██████╗ ███████╗     ██████╗ ██████╗  █████╗ ██████╗ ███████╗     ║");
            Console.WriteLine("║  ██╔═══██╗██╔════╝    ██╔════╝ ██╔══██╗██╔══██╗██╔══██╗██╔════╝     ║");
            Console.WriteLine("║  ██║   ██║███████╗    ██║  ███╗██████╔╝███████║██████╔╝█████╗       ║");
            Console.WriteLine("║  ██║   ██║╚════██║    ██║   ██║██╔═══╝ ██╔══██║██╔══██╗██╔══╝       ║");
            Console.WriteLine("║  ╚██████╔╝███████║    ╚██████╔╝██║     ██║  ██║██║  ██║███████╗     ║");
            Console.WriteLine("║   ╚═════╝ ╚══════╝     ╚═════╝ ╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝     ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"║        {projectVersion,-50}        ║");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"║        Welcome To The Future Of Virtual Storage        ^_^          ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"║    Creator: {developerName,-45} ║");
            Console.WriteLine($"║    Date: {currentDate,-58} ║");
            Console.WriteLine($"║    Time: {currentTime,-58} ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");

            string loadingText = "Waiting Download...";
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 2; i++) 
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("║    ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(loadingText);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(" [");
                    for (int k = 0; k < 10; k++)
                    {
                        if (k <= j * 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("■");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("□");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("] ");

                    // Percentage
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{j * 33 + 1}%");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.CursorLeft = 69;
                    Console.WriteLine("║");

                    if (i == 1 && j == 2) break;

                    Console.SetCursorPosition(7, 18);
                    System.Threading.Thread.Sleep(300);
                }

                if (i == 0)
                {
                    Console.SetCursorPosition(7, 18);
                    Console.Write("║                                                                      ║");
                    Console.SetCursorPosition(7, 18);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("║    System Features:                                                  ║");
            Console.WriteLine("║    • Advanced Virtual Disk Management                                ║");
            Console.WriteLine("║    • Custom File Allocation System                                   ║");
            Console.WriteLine("║    • Secure Data Storage                                             ║");
            Console.WriteLine("║    • Optimized For Performance                                       ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  System ready. Type 'help' for available commands.");
            Console.ResetColor();
            while (true)
            {
          
                var currentLocation = currentPath;
                Console.Write(currentLocation + "\\>>");
                current.ReadDirectory();

                string input = Console.ReadLine();
                parser.parse_input(input);
            }
        }
    }
}
