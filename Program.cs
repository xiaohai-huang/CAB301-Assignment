using System;
using System.Collections.Generic;
using ToolLibrary.EmpiricalAnalysis;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {

            Menu mainMenu = new MainMenu();
            mainMenu.Display();

            // uncomment below to run the empirical analysis
            //EmpiricalAnalysis();
            //Console.ReadLine();
        }

        static void EmpiricalAnalysis()
        {
            int numToolsPerType;
            EmpiricalAnalysis analysis = new EmpiricalAnalysis();

            const int NUM_TESTS = 10; // 10 tests per input size
            long timeSpent = 0;
            var watch = new System.Diagnostics.Stopwatch();

            Dictionary<int, double> results = new Dictionary<int, double>();

            for(numToolsPerType = 100;numToolsPerType<10000;numToolsPerType = numToolsPerType+1000)
            {
                int inputSize = -1;
                for (int testIndex = 0; testIndex < NUM_TESTS; testIndex++)
                {
                    inputSize = analysis.PopulateRandomData(numToolsPerType);
                    Console.WriteLine($"=========={testIndex + 1} START ==========");
                    watch.Start();
                    analysis.GetTopThree();
                    watch.Stop();
                    Console.WriteLine($"=========={testIndex + 1} END ==========");
                    Console.WriteLine($"Input Size: {inputSize}");
                    Console.WriteLine("Time spent: " + watch.ElapsedMilliseconds + "ms");
                    timeSpent += watch.ElapsedMilliseconds;
                    watch.Reset();
                }
                double avgTime = timeSpent / NUM_TESTS;

                results.Add(inputSize, avgTime);
            }
            Console.WriteLine("\n");
            Console.WriteLine("Input Size, Average Time");
            foreach (int inputSize in results.Keys)
            {
                double avgTime = results[inputSize];
                Console.WriteLine($"{inputSize}, {avgTime}");
            }

        }
    }
}
