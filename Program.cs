using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static readonly List<char> listOfLetters = new List<char>();

        static void Main(string[] args)
        {
            //Stopwatch
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            var path = @"D:\\03.txt";
            var text = File.ReadAllText(path)
                .ToLower()
                .Split(" ")
                .ToList();
            
            if (new FileInfo(path).Length == 0)
            {
                Console.WriteLine("This document is empty");
            }
            else
            {
                foreach (var w in text)
                {
                    for (var i = 0; i < w.Length - 2; i++)
                    {
                        if (w[i] == w[i + 1] && w[i] == w[i + 2])
                            listOfLetters.Add(w[i]);

                        if (Console.KeyAvailable) break;
                    }
                }
            }
                
            Thread mythread = new Thread(ThreadOne);
            mythread.Start();
            mythread.Join();

            //stop stopwatch:
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}.{1:00}",
                ts.Seconds,ts.Milliseconds);
            Console.WriteLine(" ");
            Console.WriteLine("RunTime " + elapsedTime);
        }
        static void ThreadOne()
        {
            var tenMostRepeatedChar = listOfLetters
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()) 
                .Take(10);

            Console.Write(string.Join(",", tenMostRepeatedChar.Select(x=>x.Key)));
        }
    }
}
