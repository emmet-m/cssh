using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpShell
{
    /*
     * Input Operations
     * 
     * Contains various functions for receiving input/sending output to/from the shell.
     * 
     * See also: ShellConsole.cs
     */ 
    public static class ShellInputOperations
    {
        /*
         * Tokenising function
         * 
         * Turns an input line into a list of space-delimited tokens.
         * 
         * Throws ArgumentException if the input line has invalid syntax - Right now this
         * means an unbalanced " quote.
         */ 
        public static List<string> Tokenise(string line) {

            List<string> tokens = new List<string>();

            // Empty list
            if (line == "")
                return tokens;

            // Splitting based on space0
            char[] delimiter = {' '};
            string[] rawTokens = line.Split(delimiter);

            // Now run through, joining up sections of quotes
            for (int i = 0; i < rawTokens.Length; ++i)
            {
                // Ignore blank or empty lines
                if (string.IsNullOrWhiteSpace(rawTokens[i])) continue;

                if (rawTokens[i].StartsWith("\"")) {
                    // Found valid starting quote block, delete starting quote
                    rawTokens[i] = rawTokens[i].Remove(0,1);
                    string accum = "";
                    while (i < rawTokens.Length) {

                        accum += rawTokens[i];
                        // Reached closing quote
                        if (rawTokens[i].EndsWith("\"")) {
                            // Trim closing quote
                            accum = accum.Remove(accum.Length - 1);
                            tokens.Add(accum);
                            ++i;
                            break;
                        }
                        accum += " ";

                        ++i;
                        // If a quote is left out
                        if (i == rawTokens.Length)
                            throw new ArgumentException("Missing quote in argument string");
                    }

                    // We reached the end during quote joining
                    if (i >= rawTokens.Length)
                        break;
                }

                tokens.Add(rawTokens[i]);
            }

            return tokens;
        }

        /*
         * Reads in one command from the console.
         * 
         * Commands trailing with \ will allow for the command to continue on the next line.
         * This takes precedence over multiline strings, e.g. the following command:
         * 
         *     echo "Very long sentence that gets \
         *     split into two different lines"
         *     
         * Will be resolved to:
         * 
         *     echo "Very long sentence that gets split into two different lines"
         */ 
        public static string ReadCommand(ShellConsole io) {
            string cmd = "";

            while (true) {
                cmd += io.ReadLine();
                // multiline commands
                if (!cmd.EndsWith("\\"))
                    break;
                // Delete trailing backslash
                cmd = cmd.Remove(cmd.Length - 1);
            }

            return cmd;
        }

    }
}
