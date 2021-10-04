using System;
using System.Collections.Generic;
using System.Text;

namespace DataAnalysisLib
{
    class TrajectoryFilter
    {
        public static double[] InterpolateDataToTimeIntervals(int[] indata, int[] times, double deltaTimeinmillisec)
        {
            if (indata.Length != times.Length || indata.Length == 0)
            {
                throw new ArgumentException("Data and time must have same size");
            }

            List<double> data = new List<double>();

            int j = 1;
            double i = times[0] + deltaTimeinmillisec;
            data.Add(indata[0]);
            for (; j < indata.Length; ++j)
            {
                for (; i < times[j]; i += deltaTimeinmillisec)
                {
                    data.Add((indata[j] - indata[j - 1]) / (times[j] - times[j - 1]) * (i - times[j - 1]));
                }
            }

            return data.ToArray();
        }


        public static double[] Butterworth(double[] indata, double deltaTimeinsec, double CutOff)
        {
            if (indata == null) return null;
            if (CutOff == 0) return indata;

            double Samplingrate = 1 / deltaTimeinsec;
            long dF2 = indata.Length - 1;        // The data range is set with dF2
            double[] Dat2 = new double[dF2 + 4]; // Array with 4 extra points front and back
            double[] data = indata; // Ptr., changes passed data

            // Copy indata to Dat2
            for (long r = 0; r < dF2; r++)
            {
                Dat2[2 + r] = indata[r];
            }
            Dat2[1] = Dat2[0] = indata[0];
            Dat2[dF2 + 3] = Dat2[dF2 + 2] = indata[dF2];

            const double pi = 3.14159265358979;
            double wc = Math.Tan(CutOff * pi / Samplingrate);
            double k1 = 1.414213562 * wc; // Sqrt(2) * wc
            double k2 = wc * wc;
            double a = k2 / (1 + k1 + k2);
            double b = 2 * a;
            double c = a;
            double k3 = b / k2;
            double d = -2 * a + k3;
            double e = 1 - (2 * a) - k3;

            // RECURSIVE TRIGGERS - ENABLE filter is performed (first, last points constant)
            double[] DatYt = new double[dF2 + 4];
            DatYt[1] = DatYt[0] = indata[0];
            for (long s = 2; s < dF2 + 2; s++)
            {
                DatYt[s] = a * Dat2[s] + b * Dat2[s - 1] + c * Dat2[s - 2]
                           + d * DatYt[s - 1] + e * DatYt[s - 2];
            }
            DatYt[dF2 + 3] = DatYt[dF2 + 2] = DatYt[dF2 + 1];

            // FORWARD filter
            double[] DatZt = new double[dF2 + 2];
            DatZt[dF2] = DatYt[dF2 + 2];
            DatZt[dF2 + 1] = DatYt[dF2 + 3];
            for (long t = -dF2 + 1; t <= 0; t++)
            {
                DatZt[-t] = a * DatYt[-t + 2] + b * DatYt[-t + 3] + c * DatYt[-t + 4]
                            + d * DatZt[-t + 1] + e * DatZt[-t + 2];
            }

            // Calculated points copied for return
            for (long p = 0; p < dF2; p++)
            {
                data[p] = DatZt[p];
            }

            return data;
        }

        public static List<List<KeyValuePair<double, double>>> DetectSubsequences(double[] indatax, double[] indatay, double minspeed = -1)
        {
            if (indatax.Length != indatay.Length || indatay.Length == 0)
            {
                throw new ArgumentException("Data and time must have same size");
            }

            List<List<KeyValuePair<double, double>>> data = new List<List<KeyValuePair<double, double>>>();

            if (minspeed == -1)
            {
                for (int i = 0; i < indatax.Length; ++i)
                {
                    minspeed = Math.Max(minspeed, Math.Sqrt((indatax[i] - indatax[i - 1]) * (indatax[i] - indatax[i - 1]) + (indatay[i] - indatay[i - 1]) * (indatay[i] - indatay[i - 1])));
                }
                minspeed /= 10;
            }


            data.Add(new List<KeyValuePair<double, double>>());
            data[data.Count - 1].Add(new KeyValuePair<double, double>(indatax[0], indatay[0]));
            bool ismove = false;
            double locminspeed = minspeed * 10 + 1;
            for (int i = 1; i < indatax.Length; ++i)
            {
                double speed = Math.Sqrt((indatax[i] - indatax[i - 1]) * (indatax[i] - indatax[i - 1]) + (indatay[i] - indatay[i - 1]) * (indatay[i] - indatay[i - 1]));
                if (ismove)
                {
                    if (speed < minspeed && speed <= locminspeed)
                    {
                        locminspeed = speed;
                    }
                    else
                    {
                        ismove = false;
                        locminspeed = minspeed * 10 + 1;
                        data.Add(new List<KeyValuePair<double, double>>());
                    }
                }
                else
                {
                    if (speed > minspeed)
                    {
                        ismove = true;
                    }
                }
                data[data.Count - 1].Add(new KeyValuePair<double, double>(indatax[i], indatay[i]));
            }

            return data;
        }
    }
}
