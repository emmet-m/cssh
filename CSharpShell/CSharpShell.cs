using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CSharpShell
{
    public class CSharpShell
    {
        /* Stateful variables */
        string current_directory;

        /* IO Wrapper */
        ShellConsole console;

        static void Main(string[] args)
        {
            CSharpShell sh = new CSharpShell();
            while (true) {
                sh.Menu();
            }
        }

        /*
         * Constructor
         * 
         * Boots up the shell, loads all PATH and ENV information required
         */ 
        CSharpShell() {

            // Check we're running on a supported platform
            this.OsVersionCheck();

            // Start off at parent processes working directory
            this.current_directory = Directory.GetCurrentDirectory();

            // IO wrapper
            this.console = new ShellConsole();

            return;
        }

        /*
         * The main program execution flow
         */ 
        void Menu() {
            throw new NotImplementedException();

            return;
        }

        /*
         * Ensures that we don't run in an environment that we don't support
         */
        private void OsVersionCheck() {
            if (! (Environment.OSVersion.Platform == PlatformID.Unix 
                || Environment.OSVersion.Platform == PlatformID.MacOSX))
            {
                // Shutdown 
                Console.WriteLine("Not supported for UNIX environments yet!");
                Environment.Exit(1);
            }

        }
    }
}
