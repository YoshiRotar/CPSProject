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
    public class LowPassFilter : IFilter
    {
        private double _cutoffFrequency;
        private int _filterDegree;
        private IWindow _windowFunction;

        private double GetFilterCoeficcient(int n, double kCoefficient)
        {
            if (n == (_filterDegree - 1) / 2) return 2d / kCoefficient;
            int halfOfFilterDegree = (_filterDegree - 1) / 2;
            double result = Math.Sin((2 * Math.PI * (n - halfOfFilterDegree)) / kCoefficient);
            double denominator = Math.PI * (n - halfOfFilterDegree);
            result = result / denominator;
            return result * _windowFunction.getValue(n, _filterDegree);
        }

        public SignalImplementation getFilterCoefficients(ISignal signal)
        {
            SignalImplementation filterCoefficients = new SignalImplementation();
            filterCoefficients.Points = new List<Tuple<double, Complex>>();
            double samplingFrequency = 1d/(signal.Points[1].Item1 - signal.Points[0].Item1);
            double kCoefficient = samplingFrequency / _cutoffFrequency;
            int i = 0;
            foreach (Tuple<double, Complex> point in signal.Points)
            {
                Complex filterCoefficient = Complex.GetZero();
                filterCoefficient.Real = GetFilterCoeficcient(i, kCoefficient);
                filterCoefficients.Points.Add(new Tuple<double, Complex>(point.Item1, filterCoefficient));
                i++;
            }
            return filterCoefficients;
        }

        public SignalImplementation Filter(ISignal signal)
        {
            SignalImplementation filterCoefficients = getFilterCoefficients(signal);
            SignalImplementation result = SignalOperations.ConvoluteSignals(signal, filterCoefficients);
            return result;
        }

        public void InitFilter(double cutoffFrequency, int filterDegree, IWindow windowFunction)
        {
            _cutoffFrequency = cutoffFrequency;
            _filterDegree = filterDegree;
            _windowFunction = windowFunction;
        }

        public override string ToString()
        {
            return "dolnoprzepustowy";
        }
    }
}
