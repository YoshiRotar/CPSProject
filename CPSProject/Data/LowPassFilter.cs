using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data
{
    public class LowPassFilter
    {
        private double _cutoffFrequency;
        private int _filterDegree;
        private IWindow _windowFunction;

        public LowPassFilter(double cutoffFrequency, int filterDegree, IWindow windowFunction)
        {
            _cutoffFrequency = cutoffFrequency;
            _filterDegree = filterDegree;
            _windowFunction = windowFunction;
        }

        public double GetFilterCoeficcient(int n, double kCoefficient)
        {
            if (n == 0) return kCoefficient / 2;
            int halfOfFilterDegree = (_filterDegree - 1) / 2;
            double result = Math.Sin((2 * Math.PI * (n - halfOfFilterDegree)) / kCoefficient) / Math.PI * (n - halfOfFilterDegree);
            return result * _windowFunction.getValue(n, _filterDegree);
        }

        public SignalImplementation Filter(ISignal signal)
        {
            SignalImplementation filterCoefficients = new SignalImplementation();
            filterCoefficients.Points = new List<Tuple<double, Complex>>();
            double samplingFrequency = signal.Points[1].Item1 - signal.Points[0].Item1;
            double kCoefficient = samplingFrequency / _cutoffFrequency;
            int i = 0;
            foreach(Tuple<double, Complex> point in signal.Points)
            {
                Complex filterCoefficient = Complex.GetZero();
                filterCoefficient.Real = GetFilterCoeficcient(i, kCoefficient);
                filterCoefficients.Points.Add(new Tuple<double, Complex>(point.Item1, filterCoefficient));
            }

            SignalImplementation result = SignalOperations.ConvoluteSignals(signal, filterCoefficients);
            return result;
        }
    }
}
