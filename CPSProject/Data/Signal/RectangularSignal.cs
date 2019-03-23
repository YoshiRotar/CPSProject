using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class RectangularSignal : ISignal
    {
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public double Period { get; set; }
        public double StartingMoment { get; set; }
        public double Duration { get; set; }
        public double DutyCycle { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }

        public RectangularSignal(double f, double A, double T, double t1, double d, double kw)
        {
            Frequency = f;
            Amplitude = A;
            Period = T;
            StartingMoment = t1;
            Duration = d;
            DutyCycle = kw;
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

            if (t%Period >= StartingMoment && t%Period < DutyCycle + StartingMoment) value = Amplitude;
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
