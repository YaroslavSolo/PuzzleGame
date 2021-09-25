using System;
using FileReading;
using DataAnalysis;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            try
            {
                Console.Write("Please inpet path to data file: ");
                string s = Console.ReadLine();
                List<DataReader.Probe> probes = DataReader.ReadAndParse(s);
                for (int i = 0; i < probes.Count; ++i)
                {
                    Console.WriteLine("Test number " + i + "\n" + probes[i].ToString() + "\n\n");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Program ends with some problem" + e.Message());
            }
        }
    }
}
