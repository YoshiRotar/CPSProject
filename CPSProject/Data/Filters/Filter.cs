using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Filters
{
    public abstract class Filter
    {
        protected double _cutoffFrequency;
        protected int _filterDegree;
        protected IWindow _windowFunction;

        protected double GetFilterCoeficcient(int n, double kCoefficient)
        {
            if (n == (_filterDegree - 1) / 2) return 2d / kCoefficient;
            int halfOfFilterDegree = (_filterDegree - 1) / 2;
            double result = Math.Sin((2 * Math.PI * (n - halfOfFilterDegree)) / kCoefficient);
            double denominator = Math.PI * (n - halfOfFilterDegree);
            result = result / denominator;
            return result * _windowFunction.getValue(n, _filterDegree);
        }

        protected SignalImplementation getFilterCoefficients(ISignal signal, double kCoefficient)
        {
            SignalImplementation filterCoefficients = new SignalImplementation();
            filterCoefficients.Points = new List<Tuple<double, Complex>>();
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

        public void InitFilter(double cutoffFrequency, int filterDegree, IWindow windowFunction)
        {
            _cutoffFrequency = cutoffFrequency;
            _filterDegree = filterDegree;
            _windowFunction = windowFunction;
        }

        abstract public SignalImplementation FilterSignal(ISignal signal);
    }
}
