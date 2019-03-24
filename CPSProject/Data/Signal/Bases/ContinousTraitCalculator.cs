using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Bases
{
    public class ContinousTraitCalculator
    {
        public double StartingMoment { get; set; }
        public double EndingMoment { get; set; }
        public List<Tuple<double, Complex>> Points { get; set; }
        public double AverageValue { get; set; }
        public double AbsouluteAverageValue { get; set; }
        public double AveragePower { get; set; }
        public double Variance { get; set; }
        public double EffectiveValue { get; set; }

        public void CalculateTraits()
        {
            double prefix = 1 / (EndingMoment - StartingMoment);
            int endingPoint = Points.FindIndex(t => t.Item1 == EndingMoment);

            AverageValue = 0;
            for(int i=0; i<=endingPoint; i++)
            {
                AverageValue += Points[i].Item2.Real;
            }
            AverageValue *= prefix;

            AbsouluteAverageValue = Math.Abs(AverageValue);

            AveragePower = 0;
            for (int i = 0; i <= endingPoint; i++)
            {
                AveragePower += (Points[i].Item2.Real * Points[i].Item2.Real);
            }
            AveragePower *= prefix;

            Variance = 0;
            for (int i = 0; i <= endingPoint; i++)
            {
                double value = Points[i].Item2.Real - AverageValue;
                Variance += value * value;
            }
            Variance *= prefix;

            EffectiveValue = Math.Sqrt(AveragePower);
        }
    }
}
