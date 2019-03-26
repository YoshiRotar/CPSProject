using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Base
{
    public class SignalImplementation : ISignal
    {
        public virtual bool IsLinear { get; set; }
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

        Func<Tuple<double, Complex>, double> selector = d => d.Item2.Real;
    }
}
