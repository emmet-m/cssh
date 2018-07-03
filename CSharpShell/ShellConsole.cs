using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpShell
{
    /*
     * Motivation for this class:
     * Suppose we have an impure IO function (such as ReadCommand, which calls Console.ReadLine)
     * and we wish to test it. How can we do this with a unit testing environment, without
     * using a script that runs an instance of the application at runtime?
     * 
     * This class wraps around console functions, such that in a test environment a child class
     * can be used instead which merely yields predefined values. In production, this class is 
     * used instead.
     * 
     */ 
    public class ShellConsole
    {
        /*
         * Reads one line from stdin
         */ 
        public virtual string ReadLine() {
            return Console.ReadLine();
        }

        /*
         * Writes one line to stdout, with a newline
         */ 
        public virtual void WriteLine(string line) {
            Console.WriteLine(line);
        }

        /*
         * Writes one line to stdout
         */ 
        public virtual void Write(string line) {
            Console.Write(line);
        }
    }
}
