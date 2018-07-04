using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpShell
{
    public static class ShellSystemOperations
    {
        /*
         * Checks to see if a given program name exists somewhere on %PATH%.
         * 
         * Returns the absolute path of the program if the program was found, null otherwise
         */
        public static string CheckProgramExists(string progname) {
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');

            Console.WriteLine(paths.Length);
            Console.WriteLine(paths.ToString());

            foreach (string path in paths) {
                if (File.Exists(path + progname)) {
                    return path + progname;
                }
            }

            return null;
        }



        /*
         * Checks if a given program name is relative to the current directory,
         * or should be searched for on %PATH%.
         * 
         * e.g. './echo' vs 'echo'
         *
         * Returns true if the given name is relative, false if it is not.
         */ 
        public static bool IsRelativeName(string progname) {

            // Any form of '../' or './' means a relative name
            if (progname.Contains("../")) return true;
            if (progname.Contains("./")) return true;

            return false;
        }
    }
}
