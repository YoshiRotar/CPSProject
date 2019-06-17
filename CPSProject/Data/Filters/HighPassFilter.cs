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
    public class HighPassFilter : Filter
    {
        private LowPassFilter _lowPassFilter = new LowPassFilter();

        public override SignalImplementation FilterSignal(ISignal signal)
        {
            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double kCoefficient = samplingFrequency / (samplingFrequency/2 -_cutoffFrequency);
            SignalImplementation filterCoefficients = getFilterCoefficients(signal, kCoefficient);
           
            for(int i=0; i<filterCoefficients.Points.Count; i++)
            {
                filterCoefficients.Points[i].Item2.Real = filterCoefficients.Points[i].Item2.Real * Math.Pow(-1.0, i);
            }
            SignalImplementation result = SignalOperations.ConvoluteSignals(signal, filterCoefficients);
            return result;
        }

        public override string ToString()
        {
            return "górnoprzepustowy";
        }
    }
}
