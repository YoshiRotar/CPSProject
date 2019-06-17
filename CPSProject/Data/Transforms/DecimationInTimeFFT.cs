using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Transform
{
    public class DecimationInTimeFFT : SignalTransform
    {
        public override SignalImplementation TransformSignal(ISignal signal)
        {
            if (!((signal.Points.Count & (signal.Points.Count - 1)) == 0)) throw new ArgumentException();
            SignalImplementation evenElements = new SignalImplementation();
            SignalImplementation oddElements = new SignalImplementation();
            bool isEven = true;
            foreach(Tuple<double, Complex> point in signal.Points)
            {
                if (isEven) evenElements.Points.Add(point);
                else oddElements.Points.Add(point);
                isEven = !isEven;
            }

            SignalImplementation evenElementsTransformed = evenElements;
            SignalImplementation oddElementsTransformed = oddElements;
            if (evenElements.Points.Count > 1)  evenElementsTransformed = TransformSignal(evenElements);
            if (oddElements.Points.Count > 1) oddElementsTransformed = TransformSignal(oddElements);

            SignalImplementation result = new SignalImplementation();
            result.Points.AddRange(Enumerable.Repeat(new Tuple<double, Complex>(0, Complex.GetZero()), signal.Points.Count));

            int halfOfSampleCount = signal.Points.Count / 2;
            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double f0 = samplingFrequency / signal.Points.Count;
            for (int i=0; i<evenElements.Points.Count; i++)
            {
                Complex value = GetWCoefficient(-i, signal.Points.Count);
                value = Complex.Multiply(value, oddElementsTransformed.Points[i].Item2);
                result.Points[i] = new Tuple<double, Complex>(i * f0, Complex.Add(evenElementsTransformed.Points[i].Item2, value));
                result.Points[i+halfOfSampleCount] = new Tuple<double, Complex>((i+halfOfSampleCount) * f0, Complex.Subtract(evenElementsTransformed.Points[i].Item2, value));
            }
            return result;
        }

        public override string ToString()
        {
            return "FFT z decymacją w czasie";
        }
    }
}
