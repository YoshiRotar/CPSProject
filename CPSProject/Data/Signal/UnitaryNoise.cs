using CPSProject.Data.Signal.Base;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class UnitaryNoise : SignalWithContinousValues
    {
        Random random = new Random();

        public double Amplitude { get; set; }
        public double Duration { get; set; }

        public UnitaryNoise(double f, double A, double t1, double d)
        {
            IsLinear = true;

            Frequency = f;
            Amplitude = A;
            StartingMoment = t1;
            Duration = d;
            Points = new List<Tuple<double, Complex>>();

            for(double i = StartingMoment; i <= StartingMoment + Duration; i += (1 / Frequency))
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }

            EndingMoment = StartingMoment + Duration;
            CalculateTraits();
        }

        public override Complex GenerateSignal(double t)
        {
            Complex result;

            result = new Complex
            {
                Real = 2 * Amplitude * random.NextDouble() - Amplitude,
                Imaginary = 0
            };

            return result;
        }
    }
}
