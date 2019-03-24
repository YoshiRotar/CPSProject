using CPSProject.Data.Signal.Bases;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class UnitaryNoise : ISignal
    {
        Random random = new Random();

        public bool IsLinear { get; set; } = true;
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public double StartingMoment { get; set; }
        public double Duration { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }

        public UnitaryNoise(double f, double A, double t1, double d)
        {
            Frequency = f;
            Amplitude = A;
            StartingMoment = t1;
            Duration = d;
            Points = new List<Tuple<double, Complex>>();

            for(double i = StartingMoment; i <= StartingMoment + Duration; i += Frequency)
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }
        }

        public Complex GenerateSignal(double t)
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
