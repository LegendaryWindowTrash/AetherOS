using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Schema;
using Sys = Cosmos.System;

namespace AetherOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS FS = new();

        Filemngr filemngr = new();

        public const string Version = "0.9 Dev";

        public const string userDataPath = @"0:\systemdata\userdata.txt";

        public string nameOfUser;

        protected override void BeforeRun()
        {
            // Does system file checks
            Console.WriteLine("Initializing File System...");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FS);
            Console.WriteLine("Checking systemdata folder...");
            if (FS.GetDirectory(@"0:\systemdata") == null)
            {
                Console.WriteLine("No systemdata folder found. Creating one.");
                FS.CreateDirectory(@"0:\systemdata");
            }
            Console.WriteLine("All checks good. AetherOS is now booting up...");
        }

        protected override void Run()
        {
            if (FS.GetFile(userDataPath) == null)
            {
                Console.WriteLine("No user file detected. Customize yourself a user? [y/n]");
                var choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("What is your name?");
                    var name = Console.ReadLine();
                    FS.CreateFile(userDataPath);
                    File.WriteAllText(userDataPath, name);
                    nameOfUser = name;
                }
                else
                {
                    FS.CreateFile(userDataPath);
                    File.WriteAllText(userDataPath, "user");
                    nameOfUser = "user";
                }
            }
            else
            {
                nameOfUser = File.ReadAllText(userDataPath);
            }
            Console.Write($"What would you like to do, {nameOfUser}? (type 'help' for list of commands): ");
            var input = Console.ReadLine();
            if (input == "help")
            {
                Console.WriteLine("Help:");
                Console.WriteLine("filemngr: boots into file manager, where you can manage files.\nabout: shows system info\nexit: shuts down or restarts the machine.\nuser: modifies the user's name on file.");
            }
            else if (input == "about")
            {
                Console.WriteLine($"About AetherOS: \n Version: {Version} \n User: {nameOfUser} \n Library Used: COSMOS Userkit 20221121 for VS 2022 \n Other Software Used: \n    MIV by Github user bartashevich");
            }
            else if (input == "user")
            {
                Console.WriteLine("Choose new name for user.");
                nameOfUser = Console.ReadLine();
                File.WriteAllText(userDataPath, nameOfUser);
            }
            else if (input == "filemngr")
            {
                Console.WriteLine("Booting into File Manager...");
                filemngr.RunFileMNGR();
            }
            else if (input == "exit")
            {
                Console.WriteLine("Choose a power option. 'cancel' to cancel. Your 2 options are:\n  shutdown\n  reboot");
                input = Console.ReadLine();
                if (input == "shutdown")
                {
                    Shutdown();
                }
                if (input == "reboot")
                {
                    Reboot();
                }
            }
        }

        public static void Shutdown()
        {
            Sys.Power.Shutdown();
        }

        public static void Reboot()
        {
            Sys.Power.Reboot();
        }

    }
}
