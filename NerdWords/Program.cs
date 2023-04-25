using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace NerdWords
{
    class MainClass
    {
        //Track the execution time passed
        static Stopwatch stopWatch = new Stopwatch();

        public static void Main(string[] args)
        {
            StreamReader inFile;
            StreamWriter outFile;
            string cheatCode;
            int validCount = 0;

            Console.Write("Enter a file name to test: ");
            inFile = File.OpenText(Console.ReadLine());
            outFile = File.CreateText("Petlach_B.txt"); //TODO: Replace the outFile file name

            //Reset and start the timer
            stopWatch.Reset();
            stopWatch.Start();

            //Read in one cheat code at a time to be validated
            while (!inFile.EndOfStream)
            {
                cheatCode = inFile.ReadLine();

                //Insert your code here to validate cheatCode and write the results to the file
                if (NerdWord(cheatCode))
                {
                    outFile.WriteLine(cheatCode + ":YES");
                    validCount++;
                }
                else
                {
                    outFile.WriteLine(cheatCode + ":NO");
                }
            }

            //Stop the timer and display results on the screen
            stopWatch.Stop();
            Console.WriteLine(GetTimeOutput(stopWatch));
            Console.WriteLine("Valid Nerd-Words: " + validCount);

            //Close the files
            inFile.Close();
            outFile.Close();
            Console.ReadLine();
        }

        public static string GetTimeOutput(Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            return "Time- Days:Hours:Minutes:Seconds.Milliseconds:" + ts.Days + ":" + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds;
        }

        public static bool NerdWord(string code)
        {
            if (CodeWord(code))
            {
                return true;
            }

            int yIdx = code.LastIndexOf("Y"); //TODO: try first index of

            while(yIdx > 0)
            {
                if (CodeWord(code.Substring(0, yIdx)) && NerdWord(code.Substring(yIdx + 1)))
                {
                    return true;
                }

                yIdx = code.LastIndexOf("Y", yIdx - 1);
            }

            return false;
        }

        public static bool CodeWord(string code)
        {
            //if (code[0] == 'A' && code[code.Length - 1] == 'B')
            if (code.StartsWith("A") && code.EndsWith("B"))
            {
                return NerdWord(code.Substring(1, code.Length - 2));
            }

            return code.Equals("X");
        }
    }
}
