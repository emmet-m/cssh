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
        /* Stateful variables */
        public string currentDirectory;

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

            // Start off at parent processes working directory
            this.currentDirectory = Directory.GetCurrentDirectory();

            // IO wrapper
            this.console = new ShellConsole();

            return;
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

            // Fetch command path
            string commandPath = null;
            if (!ShellSystemOperations.IsRelativeName(command[0]))
            {
                // Exists somewhere in %PATH%
                commandPath = ShellSystemOperations.CheckProgramExists(command[0]);
            }
            else if (File.Exists(this.currentDirectory + command[0]))
            {
                // Relative path
                commandPath = this.currentDirectory + command[0];
            }

            string progName = command[0]; 

            // Invalid somewhere along the way
            if (commandPath == null)
            {
                this.console.WriteLine("Command '" + progName + "' not found.");
                return;
            }

            // turn into ARGV
            command.RemoveAt(0);

            // Create new process, set info
            Process running = new Process();
            running.StartInfo.UseShellExecute = false;
            running.StartInfo.FileName = commandPath;
            running.StartInfo.Arguments = string.Join(" ", command);
            try
            {
                running.Start();
            }
            catch (InvalidOperationException f)
            {
                console.WriteLine("Process somehow had no file name - Terminating...");
                Environment.Exit(1);
            }
            catch (Win32Exception f)
            {
                this.console.WriteLine("Program '" + progName + "' not an executable file.");
                this.console.WriteLine(f.Message);
            }
            // Wait for process to finish executing
            running.WaitForExit();

            // Done!
            return;
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
    }
}
