using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cssh = CSharpShell;
namespace TestSuite
{
    [TestClass]
    public class TestShellFunctions
    {
        [TestMethod]
        public void TestTokenise()
        {
            List<string> result;

            // Empty test case
            result = Cssh.ShellInputOperations.Tokenise("");
            Assert.AreEqual(0, result.Count);

            // Simple test
            result = Cssh.ShellInputOperations.Tokenise("Hello");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Hello", result[0]);

            // Space delimited test
            result = Cssh.ShellInputOperations.Tokenise("Hello there");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Hello", result[0]);
            Assert.AreEqual("there", result[1]);

            // More spaces
            result = Cssh.ShellInputOperations.Tokenise("Hello there my name is Emmet");
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual("Hello", result[0]);
            Assert.AreEqual("there", result[1]);
            Assert.AreEqual("my", result[2]);
            Assert.AreEqual("name", result[3]);
            Assert.AreEqual("is", result[4]);
            Assert.AreEqual("Emmet", result[5]);

            // Alright, onto quotation
            result = Cssh.ShellInputOperations.Tokenise("\"Hello\"");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Hello", result[0]);

            // Spaces inside of quotes
            result = Cssh.ShellInputOperations.Tokenise("\"Hello there\"");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Hello there", result[0]);

            // Irregular spacing
            result = Cssh.ShellInputOperations.Tokenise("\" Hello there  \"");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(" Hello there  ", result[0]);

            // More than just quotes
            result = Cssh.ShellInputOperations.Tokenise("He says \" Hello there  \" to you");
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("He", result[0]);
            Assert.AreEqual("says", result[1]);
            Assert.AreEqual(" Hello there  ", result[2]);
            Assert.AreEqual("to", result[3]);
            Assert.AreEqual("you", result[4]);

        }


        // Instance of test class. See ShellConsole for explanation
        public class TestConsole : Cssh.ShellConsole
        {
            int index = 0;

            public override string ReadLine()
            {
                
                string[] testData = {"echo \"Hello world\"",
                        "echo multiple lin\\", "es are cool",
                        "echo \"Now multiple st\\", "ring lines too!\""
                    };

                // As per behaviour of the actual Console.ReadLine()
                if (this.index == testData.Length) return null;

                return testData[this.index++];
            }
        }

        [TestMethod]
        public void TestReadCommand()
        {
            // New test console
            Cssh.ShellConsole io = new TestConsole();

            // First line is just a standard command
            Assert.AreEqual("echo \"Hello world\"", Cssh.ShellInputOperations.ReadCommand(io));

            // Now for multiline commands
            Assert.AreEqual("echo multiple lines are cool", Cssh.ShellInputOperations.ReadCommand(io));

            // Now for multiline commands, with quotes
            Assert.AreEqual("echo \"Now multiple string lines too!\"", Cssh.ShellInputOperations.ReadCommand(io));

        }
    }
}
