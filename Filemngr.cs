using Cosmos.System.FileSystem;
using MIV;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using Sys = Cosmos.System;

namespace AetherOS
{
    internal class Filemngr
    {
        public static string file;
        public void RunFileMNGR()
        {
            Console.Clear();
            Sys.FileSystem.CosmosVFS FS = new();
            while (true)
            {
                Console.WriteLine("Welcome to File Manager. Type 'help' for a list of commands.");
                var input = Console.ReadLine();
                if (input == "about")
                {
                    Console.WriteLine("File Manager Version 1.0 FOR AetherOS.\nKnown Limitations:\n  Basically only useful for reading and writing .txt files.");
                }
                else if (input == "createdir")
                {
                    Console.WriteLine("What would you like to create? Here are your choices:\n  folder\n  file\nType the object you want to create.");
                    input = Console.ReadLine();
                    if (input == "folder")
                    {
                        Console.WriteLine("name the file path. Don't worry, the drive number will be added for you.");
                        input = Console.ReadLine();
                        try
                        {
                            FS.CreateDirectory(@$"0:\{input}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    else if (input == "file")
                    {
                        Console.WriteLine("name the file path AND the file extension. Don't worry, the drive number will be added for you.");
                        try
                        {
                            File.Create($@"0:\{Console.ReadLine()}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            break;
                        }
                        Console.WriteLine($"File created at directory 0:\\{input}. Use 'modifyfile' to open the editor");
                    }
                }
                else if (input == "exit")
                {
                    Console.WriteLine("Returning to AetherOS...");
                    break;
                }
                else if (input == "help")
                {
                    Console.WriteLine("Commands for File Manager:\nabout: shows info about this software.\ncreate: creates either a folder or text file.\nlistfiles:lists all the files in a directory(this may take some time, depending on the amount of files you have)\nlistdir: lists all the directories\ncreate: creates a folder or file\nreadfile: reads a file in the directory you provide.\ndeletefile: deletes a file\ndeletedir: deletes a directory.\nexit: exits the file manager.");
                }
                else if (input == "listfiles")
                {
                    Console.WriteLine("choose the directory to list files from.");
                    var file_list = Directory.GetFiles($@"0:\{Console.ReadLine()}");
                    foreach (var file in file_list)
                    {
                        Console.WriteLine(file);
                    }
                }
                else if (input == "listdir")
                {
                    Console.WriteLine("choose the directory to list subdirectories from. leave blank to examine the root.");
                    var directory_list = Directory.GetDirectories($@"0:\{Console.ReadLine()}");
                    foreach (var directory in directory_list)
                    {
                        Console.WriteLine(directory);
                    }
                }
                else if (input == "deletefile")
                {
                    Console.WriteLine("Type in EXACTLY the file you want to delete. If you make a typo, the machine MAY crash.");
                    File.Delete(@$"0:\{Console.ReadLine()}");
                    Console.WriteLine("Deleted.");
                }
                else if (input == "deletedir")
                {
                    Console.Write("Type in the directory you want to erase. The directory MUST be empty.");
                    string directoryToErase = Console.ReadLine();
                    directoryToErase = $@"0:\{directoryToErase}";
                    Console.WriteLine($"Deleting {directoryToErase}.");
                    try
                    {
                        Directory.Delete(directoryToErase);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (input == "modifyfile")
                {
                    Console.WriteLine("You will be booted into MIV. Press y to continue.");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.Clear();
                        MIV.MIV.StartMIV();
                    }
                }
            }
        }
    }
}
