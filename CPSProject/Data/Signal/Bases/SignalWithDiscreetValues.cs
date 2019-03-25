using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Bases
{
    public class SignalWithDiscreetValues : ISignal
    {
        public double StartingMoment { get; set; }
        public double EndingMoment { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }
        public double AverageValue { get; set; }
        public double AbsouluteAverageValue { get; set; }
        public double AveragePower { get; set; }
        public double Variance { get; set; }
        public double EffectiveValue { get; set; }
        public bool IsLinear { get; set; } = false;

        public void CalculateTraits()
        {
            int i = 0;
            double prefix = 1.0 / Points.Count;

            AverageValue = 0;
            while (i < Points.Count)
            {
                AverageValue += Points[i].Item2.Real;
                i++;
            }
            AverageValue *= prefix;

            AbsouluteAverageValue = Math.Abs(AverageValue);

            AveragePower = 0;
            i = 0;
            while (i < Points.Count)
            {
                AveragePower += Points[i].Item2.Real * Points[i].Item2.Real;
                i++;
            }
            AveragePower *= prefix;

            Variance = 0;
            i = 0;
            while (i < Points.Count)
            {
                double value = Points[i].Item2.Real - AverageValue;
                Variance += value * value;
                i++;
            }
            Variance *= prefix;

            EffectiveValue = Math.Sqrt(AveragePower);
        }

        public virtual Complex GenerateSignal(double t)
        {
            throw new NotImplementedException();
        }
    }
}
