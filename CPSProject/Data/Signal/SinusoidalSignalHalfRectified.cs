using CPSProject.Data.Signal.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class SinusoidalSignalHalfRectified : ISignal
    {
        public bool IsLinear { get; set; } = true;
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public double Period { get; set; }
        public double StartingMoment { get; set; }
        public double Duration { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }

        public SinusoidalSignalHalfRectified(double f, double A, double T, double t1, double d)
        {
            Frequency = f;
            Amplitude = A;
            Period = T;
            StartingMoment = t1;
            Duration = d;
            Points = new List<Tuple<double, Complex>>();

            for (double i = StartingMoment; i <= StartingMoment + Duration; i += (1 / Frequency))
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }
        }

        public Complex GenerateSignal(double t)
        {
            Complex result;

            result = new Complex
            {
                Real = (Amplitude * (Math.Sin((2 * Math.PI) / Period * (t - StartingMoment)) + Math.Abs(Math.Sin((2 * Math.PI) / Period * (t - StartingMoment))) ))/2,
                Imaginary = 0
            };

            return result;
        }
    }
}
