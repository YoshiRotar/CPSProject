using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public class KroneckerDelta : SignalWithDiscreetValues
    {
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public int NumberOfAllSamples { get; set; }
        public int NumberOfSample { get; set; }

        public KroneckerDelta(double f, double A, double t1, int l, int ns)
        {
            Frequency = f;
            Amplitude = A;
            StartingMoment = t1;
            NumberOfAllSamples = l;
            NumberOfSample = ns;
            Points = new List<Tuple<double, Complex>>();

            double Moment = StartingMoment;

            for (int i = 1; i <= NumberOfAllSamples; i++)
            {
                Points.Add(new Tuple<double, Complex>(Moment, GenerateSignal(i)));
                Moment += (1 / Frequency);
            }

            EndingMoment = StartingMoment + (1 / Frequency) * (NumberOfAllSamples - 1);
            CalculateTraits();
        }

        public override Complex GenerateSignal(double t)
        {
            if (t == NumberOfSample) return new Complex { Real = Amplitude, Imaginary = 0 };
            else return new Complex { Real = 0, Imaginary = 0 };
        }
    }
}
