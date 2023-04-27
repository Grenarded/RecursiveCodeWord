//Author: Ben Petlach
//File Name: Program.cs
//Project Name: PetlachB_MP2
//Creation Date: Apr. 24, 2023
//Modified Date: Apr. 26, 2023
//Description: Read a file and output whether or not a given string is a Nerd word based off of given rules
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
            outFile = File.CreateText("Petlach_B.txt");

            //Reset and start the timer
            stopWatch.Reset();
            stopWatch.Start();

            //Read in one cheat code at a time to be validated
            while (!inFile.EndOfStream)
            {
                cheatCode = inFile.ReadLine();

                //Insert your code here to validate cheatCode and write the results to the file
                if (IsNerdWord(cheatCode, 0, cheatCode.Length - 1))
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

        public static bool IsNerdWord(string word, int startInd, int endInd)
        {
            if (word.Length > 0)
            {
                if (IsCodeWord(word, startInd, endInd))
                {
                    return true;
                }

                for (int yIdx = word.IndexOf("Y"); yIdx > 0 && yIdx < endInd; yIdx = word.IndexOf("Y", yIdx + 1))
                {
                    //if (IsCodeWord(word.Substring(0, yIdx)) && IsNerdWord(word.Substring(yIdx + 1)))
                    if (IsCodeWord(word, startInd, yIdx) && IsNerdWord(word, yIdx + 1, endInd))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsCodeWord(string word, int startInd, int endInd)
        {
            if (word[0] == 'A' && word[word.Length - 1] == 'B')
            {
                //return IsNerdWord(word.Substring(1, word.Length - 2));
                return IsNerdWord(word, 1, endInd - 1);
            }

            return word.Equals("X");
        }

        //public static bool IsNerdWord(string word)
        //{
        //    if (word.Length > 0)
        //    {
        //        if (IsCodeWord(word))
        //        {
        //            return true;
        //        }

        //        for (int yIdx = word.IndexOf("Y"); yIdx > 0 && yIdx < word.Length - 1; yIdx = word.IndexOf("Y", yIdx + 1))
        //        {
        //            if (IsCodeWord(word.Substring(0, yIdx)) && IsNerdWord(word.Substring(yIdx + 1)))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //   return false;
        //}

        //public static bool IsCodeWord(string word)
        //{
        //    if (word[0] == 'A' && word[word.Length - 1] == 'B')
        //    {
        //        return IsNerdWord(word.Substring(1, word.Length - 2));
        //    }

        //    return word.Equals("X");
        //}
    }
}