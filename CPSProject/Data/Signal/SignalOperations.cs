using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public static class SignalOperations
    {
        private static SignalImplementation CombineSignals(Func<Complex, Complex, Complex> func, ISignal firstSignal, ISignal secondSignal)
        {
            List<Tuple<double, Complex>> signal1 = firstSignal.Points;
            List<Tuple<double, Complex>> signal2 = secondSignal.Points;
            List<Tuple<double, Complex>> outputSignal = new List<Tuple<double, Complex>>();

            int i = 0;
            int j = 0;
            Complex result;

            while (i < signal1.Count && j < signal2.Count)
            {
                if (Math.Abs(signal1[i].Item1 - signal2[j].Item1) <= 0.001)
                {
                    result = func(signal1[i].Item2, signal2[j].Item2);
                    outputSignal.Add(new Tuple<double, Complex>(signal1[i].Item1, result));
                    i++;
                    j++;
                }
                else if (signal1[i].Item1 - signal2[j].Item1 > 0.001)
                {
                    j++;
                }
                else if (signal2[j].Item1 - signal1[i].Item1 > 0.001)
                {
                    i++;
                }
            }

            SignalImplementation signalImplementation = new SignalImplementation();
            signalImplementation.Points = new List<Tuple<double, Complex>>();
            signalImplementation.Points.AddRange(outputSignal);
            return signalImplementation;
        }

        private static void ExtendSignals(ISignal firstSignal, ISignal secondSignal)
        {
            double epsilon = 0.001;
            ISignal soonerSignal = (firstSignal.Points[0].Item1 < secondSignal.Points[0].Item1) ? firstSignal : secondSignal;
            ISignal laterSignal = (firstSignal.Points[0].Item1 >= secondSignal.Points[0].Item1) ? firstSignal : secondSignal;
            double currentX = soonerSignal.Points[0].Item1;
            double laterSignalStart = laterSignal.Points[0].Item1;
            double samplePeriod = firstSignal.Points[1].Item1 - firstSignal.Points[0].Item1;
            while (currentX < (laterSignalStart - epsilon))
            {
                laterSignal.Points.Insert(0, new Tuple<double, Complex>(currentX, Complex.GetZero()));
                currentX += samplePeriod;
            }
        }

        public static SignalImplementation AddSignals(ISignal firstSignal, ISignal secondSignal)
        {
            return CombineSignals(Complex.Add, firstSignal, secondSignal);
        }

        public static SignalImplementation SubtractSignals(ISignal firstSignal, ISignal secondSignal)
        {
            return CombineSignals(Complex.Subtract, firstSignal, secondSignal);
        }

        public static SignalImplementation MultiplySignals(ISignal firstSignal, ISignal secondSignal)
        {
            return CombineSignals(Complex.Multiply, firstSignal, secondSignal);
        }

        public static SignalImplementation DivideSignals(ISignal firstSignal, ISignal secondSignal)
        {
            return CombineSignals(Complex.Divide, firstSignal, secondSignal);
        }

        //function assumes signals have the same sample frequency
        public static SignalImplementation ConvoluteSignals(ISignal firstSignal, ISignal secondSignal)
        {
            ISignal extendedFirstSignal = new SignalImplementation();
            ISignal extendedSecondSignal = new SignalImplementation();
            extendedFirstSignal.Points = new List<Tuple<double, Complex>>(firstSignal.Points);
            extendedSecondSignal.Points = new List<Tuple<double, Complex>>(secondSignal.Points);
            ExtendSignals(extendedFirstSignal, extendedSecondSignal);

            SignalImplementation outputSignal = new SignalImplementation();
            outputSignal.Points = new List<Tuple<double, Complex>>();
            double currentX = extendedFirstSignal.Points[0].Item1;
            double samplePeriod = firstSignal.Points[1].Item1 - firstSignal.Points[0].Item1;
            for (int i = 0; i < (extendedFirstSignal.Points.Count + extendedSecondSignal.Points.Count); i++)
            {
                Complex sum = Complex.GetZero();
                for (int j = 0; j < extendedFirstSignal.Points.Count; j++)
                {
                    if ((i - j) < 0 || (i - j) >= extendedSecondSignal.Points.Count) continue;
                    sum = Complex.Add(sum, Complex.Multiply(extendedFirstSignal.Points[j].Item2, extendedSecondSignal.Points[i - j].Item2));
                }

                outputSignal.Points.Add(new Tuple<double, Complex>(currentX, sum));
                currentX += samplePeriod;
            }

            return outputSignal;
        }

        //function assumes signals have the same sample frequency
        public static SignalImplementation CorrelateSignals(ISignal firstSignal, ISignal secondSignal)
        {
            ISignal extendedFirstSignal = new SignalImplementation();
            ISignal extendedSecondSignal = new SignalImplementation();
            extendedFirstSignal.Points = new List<Tuple<double, Complex>>(firstSignal.Points);
            extendedSecondSignal.Points = new List<Tuple<double, Complex>>(secondSignal.Points);
            ExtendSignals(extendedFirstSignal, extendedSecondSignal);

            SignalImplementation outputSignal = new SignalImplementation();
            outputSignal.Points = new List<Tuple<double, Complex>>();
            double samplePeriod = firstSignal.Points[1].Item1 - firstSignal.Points[0].Item1;
            double currentX = extendedFirstSignal.Points[0].Item1 - (extendedFirstSignal.Points.Count - 1) * samplePeriod;
            for (int i = -(extendedFirstSignal.Points.Count - 1); i < extendedSecondSignal.Points.Count; i++)
            {
                Complex sum = Complex.GetZero();
                for (int j = 0; j < extendedFirstSignal.Points.Count; j++)
                {
                    if ((i + j) < 0 || (i + j) >= extendedSecondSignal.Points.Count) continue;
                    sum = Complex.Add(sum, Complex.Multiply(extendedFirstSignal.Points[j].Item2, extendedSecondSignal.Points[i + j].Item2));
                }

                outputSignal.Points.Add(new Tuple<double, Complex>(currentX, sum));
                currentX += samplePeriod;
            }

            return outputSignal;
        }
    }
}
