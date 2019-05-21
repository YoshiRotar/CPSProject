using CPSProject.Data.Filters;
using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data
{
    public class HighPassFilter : IFilter
    {
        private LowPassFilter _lowPassFilter = new LowPassFilter();

        public SignalImplementation Filter(ISignal signal)
        {
            SignalImplementation lowPassfilterCoefficients = _lowPassFilter.getFilterCoefficients(signal);
            double samplingFrequency = 1d/(signal.Points[1].Item1 - signal.Points[0].Item1);
            double d = signal.Points[signal.Points.Count - 1].Item1 - signal.Points[0].Item1;
            SinusoidalSignal sinusoidal = new SinusoidalSignal(samplingFrequency, 1, 1d/samplingFrequency, 0, d);
            
            SignalImplementation filterCoefficients = SignalOperations.MultiplySignals(lowPassfilterCoefficients, sinusoidal);
            SignalImplementation result = SignalOperations.ConvoluteSignals(signal, filterCoefficients);
            return result;
        }

        public void InitFilter(double cutoffFrequency, int filterDegree, IWindow windowFunction)
        {
            _lowPassFilter.InitFilter(cutoffFrequency, filterDegree, windowFunction);
        }

        public override string ToString()
        {
            return "górnoprzepustoey";
        }
    }
}
