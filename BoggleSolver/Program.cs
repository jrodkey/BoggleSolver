
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BoggleSolver.Board;

namespace BoggleSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Loads a list up front from local memory.
            string path = Path.GetFullPath("../../../BoggleDictionary.txt");
            string[] wordList = File.ReadAllLines(path);

            // Initialize our Boggle class and validate the list of words in order to 
            // populate our dictionary.
            Stopwatch stopwatch = Stopwatch.StartNew();
            Boggle boggle = new Boggle();
            boggle.SetLegalWords(wordList);
            stopwatch.Stop();

            Console.WriteLine(string.Format("Boggle Initializing Time: {0:00}:{1:00}:{2:00}:{3:00}:", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.ElapsedMilliseconds / 10));

            stopwatch = Stopwatch.StartNew();
            
            //IEnumerable<string> results = boggle.SolveBoard(10, 4, "");
            IEnumerable<string> results = boggle.SolveBoard(3, 3, "yoxrbaved");
            //IEnumerable<string> results = boggle.SolveBoard(5, 4, "yoxearbaeavedeahueea");
            //IEnumerable<string> results = boggle.SolveBoard(4, 4, "baexbaagvedueuda");
            //IEnumerable<string> results = boggle.SolveBoard(5, 5, "braexrbaagvedqueudasimdie");
            //IEnumerable<string> results = boggle.SolveBoard(6, 6, "braexrbaiagvedqueudasimdivemnoppzwyxe");
            //IEnumerable<string> results = boggle.SolveBoard(7, 7, "braexrbaagvedqueudasimdiemabyzgwdplzwkilpdehasecyk");
            //IEnumerable<string> results = boggle.SolveBoard(10, 10, "braexrbaagvedqueudasimdiemabyzgwdplzwkilpdehasecykbraexrbaagvedqueudasimdiemabyzgwdplzwkilpdehasecyk");
            stopwatch.Stop();

            Console.WriteLine("Boggle Solving Result: ============================");

            // Run one final sorting pass on the results, then output to terminal.
            List<string> sortedResult = new List<string>(results);
            sortedResult.Sort();
            Console.WriteLine("Words Found: " + sortedResult.Count);
            foreach(string word in sortedResult)
            {
                Console.Write(word + ", ");
            }
            Console.WriteLine();
            Console.WriteLine(string.Format("Boggle Solving Time: {0:00}:{1:00}:{2:00}:{3:00}:", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.ElapsedMilliseconds / 10));
        }
    }
}
