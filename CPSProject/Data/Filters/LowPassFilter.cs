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
    public class LowPassFilter : Filter
    {   
        public override SignalImplementation FilterSignal(ISignal signal)
        {
            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double kCoefficient = samplingFrequency / _cutoffFrequency;
            SignalImplementation filterCoefficients = getFilterCoefficients(signal, kCoefficient);
            SignalImplementation result = SignalOperations.ConvoluteSignals(signal, filterCoefficients);
            return result;
        }

        public override string ToString()
        {
            return "dolnoprzepustowy";
        }
    }
}
