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
        private List<Complex> _wCoefficients = new List<Complex>();

        public override SignalImplementation TransformSignal(ISignal signal)
        {
            SignalImplementation cutSignal = CutSignalSamplesToPowerOfTwo(signal);
            CalculateWCoefficients(cutSignal.Points.Count);
            SignalImplementation result = CalculateFastTransform(cutSignal, 0);
            foreach(Tuple<double, Complex> point in result.Points)
            {
                point.Item2.Real = point.Item2.Real / signal.Points.Count;
                point.Item2.Imaginary = point.Item2.Imaginary / signal.Points.Count;
            }
            return result;
        }

        public override string ToString()
        {
            return "FFT z decymacją w czasie";
        }

        private SignalImplementation CalculateFastTransform(ISignal signal, int recursionDepth)
        {
            SignalImplementation evenElements = new SignalImplementation();
            SignalImplementation oddElements = new SignalImplementation();
            bool isEven = true;
            foreach (Tuple<double, Complex> point in signal.Points)
            {
                if (isEven) evenElements.Points.Add(point);
                else oddElements.Points.Add(point);
                isEven = !isEven;
            }

            SignalImplementation evenElementsTransformed = evenElements;
            SignalImplementation oddElementsTransformed = oddElements;
            if (evenElements.Points.Count > 1) evenElementsTransformed = CalculateFastTransform(evenElements, recursionDepth+1);
            if (oddElements.Points.Count > 1) oddElementsTransformed = CalculateFastTransform(oddElements, recursionDepth+1);

            SignalImplementation result = new SignalImplementation();
            result.Points.AddRange(Enumerable.Repeat(new Tuple<double, Complex>(0, Complex.GetZero()), signal.Points.Count));

            int halfOfSampleCount = signal.Points.Count / 2;
            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double f0 = samplingFrequency / signal.Points.Count;
            for (int i = 0; i < evenElements.Points.Count; i++)
            {
                Complex product = Complex.Multiply(_wCoefficients[(int)(i*Math.Pow(2, recursionDepth))], oddElementsTransformed.Points[i].Item2);
                result.Points[i] = new Tuple<double, Complex>(i * f0, Complex.Add(evenElementsTransformed.Points[i].Item2, product));
                result.Points[i + halfOfSampleCount] = new Tuple<double, Complex>((i + halfOfSampleCount) * f0, Complex.Subtract(evenElementsTransformed.Points[i].Item2, product));
            }
            return result;
        }

        private void CalculateWCoefficients(int vectorSize)
        {
            int halfOfVectorSize = vectorSize / 2;
            for(int i=0; i<halfOfVectorSize; i++)
            {
                _wCoefficients.Add(GetWCoefficient(-i, vectorSize));
            }
        }

        private SignalImplementation CutSignalSamplesToPowerOfTwo(ISignal signal)
        {
            int powerOfTwo = 1;
            while(powerOfTwo <= signal.Points.Count)
            {
                powerOfTwo *= 2;
            }
            powerOfTwo /= 2;
            SignalImplementation result = new SignalImplementation();
            result.Points.AddRange(signal.Points.GetRange(0, powerOfTwo));
            return result;
        }
    }
}
