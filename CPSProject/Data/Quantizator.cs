using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data
{
    public static class Quantizator
    {
        public static SignalImplementation SampleSignal(ISignal signalToSample, double samplingFrequency)
        {
            SignalImplementation result = new SignalImplementation();
            double step = 1 / samplingFrequency;

            for (double d=signalToSample.Points.First().Item1; d<=signalToSample.Points.Last().Item1; d+=step)
            {
                Tuple<double, Complex> point = signalToSample.Points.Find(x => x.Item1 == d);
                if (point != null) result.Points.Add(point);
                else
                {
                    IEnumerable<Tuple<double, Complex>> lowerPoints = signalToSample.Points.Where(x => x.Item1 < d);
                    IEnumerable<Tuple<double, Complex>> greaterPoints = signalToSample.Points.Where(x => x.Item1 > d);
                    if (lowerPoints.Count() == 0) result.Points.Add(greaterPoints.Min());
                    else if (greaterPoints.Count() == 0) result.Points.Add(lowerPoints.Max());
                    else
                    {
                        Tuple<double, Complex> lowerPoint = lowerPoints.Max();
                        Tuple<double, Complex> greaterPoint = greaterPoints.Min();
                        Complex averagedPoint = new Complex
                        {
                            Real = (greaterPoint.Item2.Real + lowerPoint.Item2.Real) / 2,
                            Imaginary = (greaterPoint.Item2.Imaginary + lowerPoint.Item2.Imaginary) / 2
                        };
                        result.Points.Add(new Tuple<double, Complex>(d, averagedPoint));
                    }
                }
            }

            return result;

        }
    }
}
