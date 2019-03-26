using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class TriangularSignal : SignalWithContinousValues
    {
        public double Amplitude { get; set; }
        public double Period { get; set; }
        public double Duration { get; set; }
        public double DutyCycle { get; set; }

        public TriangularSignal(double f, double A, double T, double t1, double d, double kw)
        {
            Frequency = f;
            Amplitude = A;
            Period = T;
            StartingMoment = t1;
            Duration = d;
            DutyCycle = kw;
            Points = new List<Tuple<double, Complex>>();

            for (double i = StartingMoment; i <= StartingMoment + Duration; i += (1 / Frequency))
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }

            if (Duration >= Period)
            {
                EndingMoment = StartingMoment + Duration - (Duration % Period);
                CalculateTraits();
            }
            else
            {
                AverageValue = 0;
                AbsouluteAverageValue = 0;
                AveragePower = 0;
                Variance = 0;
                EffectiveValue = 0;
            }
        }

        public override Complex GenerateSignal(double t)
        {
            Complex result;

            double value;

            double relativeTime = (t - StartingMoment) % Period;

            if (relativeTime >= 0 && relativeTime < DutyCycle * Period) value = Amplitude / (DutyCycle * Period) * relativeTime;
            else value = -Amplitude / ((1.0 - DutyCycle) * Period) * relativeTime + (Amplitude / (1.0 - DutyCycle));

            result = new Complex
            {
                Real = value,
                Imaginary = 0
            };

            return result;
        }
    }
}
