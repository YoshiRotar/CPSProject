using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Base
{
    public class SignalImplementation : ISignal
    {
        private readonly Func<Tuple<double, Complex>, double> selector = d => d.Item2.Real;

        public double StartingMoment { get; set; }
        public double EndingMoment { get; set; }
        public double AverageValue { get; set; }
        public double AbsouluteAverageValue { get; set; }
        public double AveragePower { get; set; }
        public double Variance { get; set; }
        public double EffectiveValue { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }

        public virtual Complex GenerateSignal(double t)
        {
            throw new NotImplementedException();
        }

        public List<int> GenerateRealHistogram(int numberOfIntervals)
        {
            double min = Points.Min(selector);
            double max = Points.Max(selector);
            double universumWidth = max - min;
            double intervalWidth = universumWidth / numberOfIntervals;

            int[] result = new int[numberOfIntervals];

            for(int i=0; i<Points.Count; i++)
            {
                int interval = (int)Math.Floor((Points[i].Item2.Real - min) / (intervalWidth + 0.0001));
                result[interval]++;
            }

            return result.ToList();
        }

        public void CalculateTraits()
        {
            int i = 0;
            double prefix = 1.0 / Points.Count;


            AverageValue = 0;
            while (i < Points.Count &&  Points[i].Item1 <= EndingMoment)
            {
                AverageValue += Points[i].Item2.Real;
                i++;
            }
            AverageValue *= prefix;

            AbsouluteAverageValue = Math.Abs(AverageValue);

            AveragePower = 0;
            i = 0;
            while (i < Points.Count && Points[i].Item1 <= EndingMoment)
            {
                AveragePower += Points[i].Item2.Real * Points[i].Item2.Real;
                i++;
            }
            AveragePower *= prefix;

            Variance = 0;
            i = 0;
            while (i < Points.Count && Points[i].Item1 <= EndingMoment)
            {
                double value = Points[i].Item2.Real - AverageValue;
                Variance += value * value;
                i++;
            }
            Variance *= prefix;

            EffectiveValue = Math.Sqrt(AveragePower);
        }
    }
}
