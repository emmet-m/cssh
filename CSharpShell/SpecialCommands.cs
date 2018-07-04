using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CSharpShell
{
    /*
     *  This class contains code to run all the special shell commands, 
     *  such as exit, cd, etc.
     */ 
    class SpecialCommands
    {
        public static readonly string[] ReservedWords = { "exit", "cd" };

        public static bool RunSpecialCommand(string command, List<string> arguments, ShellConsole io)
        {
            foreach (string cmd in ReservedWords)
            {
                // If given command is special command
                if (cmd.ToLower() == command.ToLower()) {
                    // Switch on command
                    switch (command.ToLower())
                    {
                        case "cd":
                            ChangeDirectory(arguments, io);
                            break;
                        case "exit":
                            Exit();
                            break;
                        default:
                            Console.WriteLine("Error - Program '" + command + "' matched special command, but reached default in command switch");
                            Environment.Exit(1);
                            // To keep compiler happy
                            break;
                    }
                    return true;
                }
            }

            // No special command found
            return false;
        }

        private static void ChangeDirectory(List<string> args, ShellConsole io)
        {
            // cd does nothing for no arguments
            if (args.Count == 0)
            {
                return;
            }

            Debug.WriteLine(args[0]);

            // Attempt to change directory, print error if not possible
            try
            {
                Directory.SetCurrentDirectory(args[0]);
            }
            catch (FileNotFoundException f)
            {
                io.WriteLine(f.Message);
            }
            catch (DirectoryNotFoundException d)
            {
                io.WriteLine(d.Message);
            }
        }

        private static void Exit()
        {
            // Unconditional Shutdown
            Environment.Exit(0);
        }
    }
}
