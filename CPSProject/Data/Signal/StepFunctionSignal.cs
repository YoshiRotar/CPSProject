using CPSProject.Data.Signal.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class StepFunctionSignal : SignalImplementation
    {
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public double Duration { get; set; }
        public double TimeOfStep { get; set; }

        public StepFunctionSignal(double f, double A, double t1, double d, double ts)
        {
            Frequency = f;
            Amplitude = A;
            StartingMoment = t1;
            Duration = d;
            TimeOfStep = ts;
            Points = new List<Tuple<double, Complex>>();

            for (double i = StartingMoment; i <= StartingMoment + Duration; i += (1 / Frequency))
            {
                Points.Add(new Tuple<double, Complex>(i, GenerateSignal(i)));
            }

            EndingMoment = StartingMoment + Duration;
            CalculateTraits();
        }

        public override Complex GenerateSignal(double t)
        {
            Complex result;

            double value;

            if (t > TimeOfStep) value = Amplitude;
            else if (t < TimeOfStep) value = 0;
            else value = 0.5 * Amplitude;

            result = new Complex
            {
                Real = value,
                Imaginary = 0
            };

            return result;
        }
    }
}
