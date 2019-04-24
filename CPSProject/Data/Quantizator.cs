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

        public static SignalImplementation Quantize(ISignal signalToQuantize, double levelsOfQuantization)
        {
            double realMinimum = signalToQuantize.Points.Select(x => x.Item2.Real).Min();
            double realMaximum = signalToQuantize.Points.Select(x => x.Item2.Real).Max();
            double realStep = (realMaximum - realMinimum) / (levelsOfQuantization - 1);

            double imaginaryMinimum = signalToQuantize.Points.Select(x => x.Item2.Imaginary).Min();
            double imaginaryMaximum = signalToQuantize.Points.Select(x => x.Item2.Imaginary).Max();
            double imaginaryStep = (realMaximum - realMinimum) / levelsOfQuantization;

            foreach(Tuple<double,Complex> point in signalToQuantize.Points)
            {
                double realPointValue = realMaximum;
                double imaginaryPointValue = imaginaryMaximum;
                while (realPointValue > point.Item2.Real && realPointValue - realStep >= realMinimum) realPointValue -= realStep;
                while (imaginaryPointValue > point.Item2.Imaginary && imaginaryPointValue - imaginaryStep >= imaginaryMinimum) imaginaryPointValue -= imaginaryStep;
                point.Item2.Real = realPointValue;
                point.Item2.Imaginary = imaginaryPointValue;
            }

            return (SignalImplementation)signalToQuantize;
        }

        public static SignalImplementation ExtrapolationZeroOrderHold(ISignal signalToReconstruct, double targetFrequency)
        {
            SignalImplementation reconstructedSignal = new SignalImplementation();
            double step = 1 / targetFrequency;
            if (signalToReconstruct.Points.Count < 2) throw new ArgumentException();
            double baseSignalStep = signalToReconstruct.Points.ElementAtOrDefault(1).Item1 - signalToReconstruct.Points.ElementAtOrDefault(0).Item1;
            Tuple<double, Complex> referencePoint = signalToReconstruct.Points.First();
            foreach(Tuple<double, Complex> point in signalToReconstruct.Points)
            {
                for(double d=point.Item1; d<point.Item1+baseSignalStep; d+=step)
                {
                    reconstructedSignal.Points.Add(new Tuple<double, Complex>(d, point.Item2));
                }
            }
            return reconstructedSignal;
        }

        public static double CalculateMeanSqueredError(ISignal signalToCompare, ISignal comparedSignal)
        {
            double MSE = 0;
            int numberOfElements = 0;
            for(int i=0, j=0; i<signalToCompare.Points.Count && j<comparedSignal.Points.Count;)
            {
                double xDifference = signalToCompare.Points.ElementAt(i).Item1 - comparedSignal.Points.ElementAt(j).Item1;
                if (Math.Abs(xDifference) < 0.001)
                {
                    double difference = signalToCompare.Points.ElementAt(i).Item2.Real - comparedSignal.Points.ElementAt(j).Item2.Real;
                    MSE += difference * difference;
                    numberOfElements++;
                    i++;
                    j++;
                }
                else if (xDifference < 0) i++;
                else j++;
                    
            }
            return MSE / numberOfElements;
        }

        public static double CalculatePeekSignalToNoiseRatio(ISignal signalToCompare, ISignal comparedSignal)
        {
            double peekSquared = Math.Pow(signalToCompare.Points.Max(x => x.Item2.Real), 2);
            double MSE = CalculateMeanSqueredError(signalToCompare, comparedSignal);
            return 10 * Math.Log10(peekSquared / MSE);
        }
    }
}
