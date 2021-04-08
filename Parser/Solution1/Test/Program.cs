using System;
using FileReading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            foreach (var i in FileStreams.readCsvFile("../../../../res/test3.csv"))
            {
                Console.WriteLine(i.Key.Key + " " + i.Key.Value + " " + i.Value.ToString());
            }
        }
    }
}