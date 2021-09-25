using System;
//using FileReading;

namespace DataAnalysis
{
    public class DataWriter
    {
        public delegate void DataConsumerDelegate(DateTime time, int x, int y, string obj);

        public static DataConsumerDelegate dataDel;

        static DataWriter()
        {
            dataDel = DataConsmer;
        }

        public static void DataConsmer(DateTime time, int x, int y, string obj)
        {
            //FileStreams.writeToFile("../../../working_result.txt", "" + time.ToFileTime() + ":" + x + ":" + y + ":" + obj + ";");
        }
    }
}
