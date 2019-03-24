using CPSProject.Data.Signal.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class ImpulseNoise : ISignal
    {
        Random random = new Random();

        public bool IsLinear { get; set; } = false;
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public double StartingMoment { get; set; }
        public double Duration { get; set; }
        public double Probability { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }

        public ImpulseNoise(double f, double A, double t1, double d, double p)
        {
            Frequency = f;
            Amplitude = A;
            StartingMoment = t1;
            Duration = d;
            Probability = p;
            Points = new List<Tuple<double, Complex>>();

            for (double i = StartingMoment; i <= StartingMoment + Duration; i += Frequency)
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }
        }

        public Complex GenerateSignal(double t)
        {
            Complex result;

            double value;

            if (1 - random.NextDouble() <= Probability) value = Amplitude;
            else value = 0;

            result = new Complex
            {
                Real = value,
                Imaginary = 0
            };

            return result;
        }
    }
}
