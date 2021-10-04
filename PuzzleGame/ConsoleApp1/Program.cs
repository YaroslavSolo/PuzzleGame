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
                //Console.Write("|" + s + "|");
                if (s.Length == 0)
                {
                    s = "../../../working_result.txt";
                }
                Console.Write("Please inpet path to result file: ");
                string sr = Console.ReadLine();
                if(sr.Length == 0)
                {
                    sr = "../../../result.csv";
                }
                List<DataReader.Probe> probes = DataReader.ReadAndParse(s);
                long timetotal = 0;
                for (int i = 0; i < probes.Count; ++i)
                {
                    //Console.WriteLine("Test number " + i + "\n" + probes[i].ToString() + "\n\n");
                    FileReading.FileStreams.writeToFile(sr, "" + (i+1) + "," + probes[i].ToStringCSV() + "\n");
                    timetotal += probes[i].totaltime;
                }
                FileReading.FileStreams.writeToFile(sr, "" + "Total time:," + timetotal + "\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("Program ends with some problem: " + e.Message);
            }
        }
    }
}
