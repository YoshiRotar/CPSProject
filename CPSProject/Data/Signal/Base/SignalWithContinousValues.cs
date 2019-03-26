using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Base
{
    public class SignalWithContinousValues : SignalImplementation
    {
        public double StartingMoment { get; set; }
        public double EndingMoment { get; set; }
        public double Frequency { get; set; }
        public override bool IsLinear { get; set; } = true;

        public void CalculateTraits()
        {
            int i = 0;
            double prefix = 1 / (EndingMoment - StartingMoment);
            double distanceBetweenSamples = 1 / Frequency;

            AverageValue = 0;
            while (i < Points.Count - 1 && Points[i].Item1 <= EndingMoment)
            {
                AverageValue += ((Points[i].Item2.Real + Points[i+1].Item2.Real) / 2) * distanceBetweenSamples;
                i++;
            }
            AverageValue *= prefix;

            AbsouluteAverageValue = Math.Abs(AverageValue);

            AveragePower = 0;

            i = 0;
            while (i < Points.Count - 1 && Points[i].Item1 <= EndingMoment)
            {
                AveragePower += (Points[i].Item2.Real * Points[i].Item2.Real) + (Points[i+1].Item2.Real * Points[i+1].Item2.Real) / 2 * distanceBetweenSamples;
                i++;
            }
            AveragePower *= prefix;

            Variance = 0;

            i = 0;
            while (i < Points.Count - 1 && Points[i].Item1 <= EndingMoment)
            {
                double value1 = Points[i].Item2.Real - AverageValue;
                value1 *= value1;
                double value2 = Points[i+1].Item2.Real - AverageValue;
                value2 *= value2;
                Variance += (value1 + value2) / 2 * distanceBetweenSamples;
                i++;
            }
            Variance *= prefix;

            EffectiveValue = Math.Sqrt(AveragePower);
        }
    }
}
