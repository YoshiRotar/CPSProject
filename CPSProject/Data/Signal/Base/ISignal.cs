using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal.Base
{
    public interface ISignal
    {
        double AverageValue { get; set; }
        double AbsouluteAverageValue { get; set; }
        double AveragePower { get; set; }
        double Variance { get; set; }
        double EffectiveValue { get; set; }
        List<Tuple<double, Complex>> Points { get; set; }
        Complex GenerateSignal(double t);
        List<int> GenerateRealHistogram(int numberOfIntervals);
    }
}
