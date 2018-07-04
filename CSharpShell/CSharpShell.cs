using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;


namespace CSharpShell
{
    public class CSharpShell
    {
        /* IO Wrapper */
        public ShellConsole console;

        static void Main(string[] args)
        {
            CSharpShell sh = new CSharpShell();
            while (true)
            {
                sh.PrintPrompt();
                sh.Menu();
            }
        }

        /*
         * Constructor
         * 
         * Boots up the shell, loads all PATH and ENV information required
         */
        CSharpShell()
        {
            // Check we're running on a supported platform
            this.OsVersionCheck();

            // IO wrapper
            this.console = new ShellConsole();
        }

        /*
         * The main program execution flow
         */
        void Menu()
        {
            // Read in shell input
            List<String> command = ShellInputOperations.Tokenise(this.console.ReadLine());

            // Enter key pressed
            if (command.Count == 0) return;

            // Check program exists
            string commandPath = null;
            commandPath = ShellSystemOperations.CheckProgramExists(command[0]);
            string progName = command[0]; 
            if (commandPath == null)
            {
                this.console.WriteLine("Program '" + progName + "' not found.");
                return;
            }

            // turn into ARGV
            command.RemoveAt(0);

            // Run the program!
            Process running = this.RunProgram(commandPath, string.Join(" ", command));
            if (running == null) return;
            running.WaitForExit();
        }

        /*
         * Shows the current prompt
         */
        public void PrintPrompt()
        {
            this.console.Write("cssh> ");
        }

        /*
         * Ensures that we don't run in an environment that we don't support
         */
        private void OsVersionCheck()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix
                || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                // Shutdown 
                Console.WriteLine("Not supported for UNIX environments yet!");
                Environment.Exit(1);
            }

        }

        /*
         * Runs a program with the given absolute path name, it's arguments and optionally
         * the command name that was entered by a user.
         */ 
        private Process RunProgram(string programName, string arguments, string enteredName = null)
        {
            // Create new process, set info
            Process running = new Process();
            running.StartInfo.UseShellExecute = false;
            running.StartInfo.FileName = programName;
            running.StartInfo.Arguments = arguments;
            try
            {
                running.Start();
            }
            catch (InvalidOperationException f)
            {
                console.WriteLine("Process somehow had no file name - Terminating...");
                console.WriteLine(f.Message);
                Environment.Exit(1);
            }
            catch (Win32Exception f)
            {
                if (enteredName == null) { 
                    this.console.WriteLine("Program '" + programName + "' not an executable file.");
                } else {
                    this.console.WriteLine("Program '" + enteredName + "' not an executable file.");
                }
                this.console.WriteLine(f.Message);

                return null;
            }

            return running;
        }
    }
}
