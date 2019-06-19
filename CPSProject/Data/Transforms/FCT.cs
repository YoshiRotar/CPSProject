using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPSProject.Data.Signal.Base;

namespace CPSProject.Data.Transforms
{
    public class FCT : SignalTransform
    {
        public override SignalImplementation TransformSignal(ISignal signal)
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
            oddElements.Points.Reverse();
            SignalImplementation combinedSignal = new SignalImplementation();
            combinedSignal.Points.AddRange(evenElements.Points);
            combinedSignal.Points.AddRange(oddElements.Points);
            DecimationInTimeFFT fft = new DecimationInTimeFFT();
            SignalImplementation signalAfterFFT = fft.TransformSignal(combinedSignal);
            SignalImplementation result = new SignalImplementation();
            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double f0 = samplingFrequency / signal.Points.Count;
            for (int i = 0; i < signalAfterFFT.Points.Count; i++)
            {
                double exponent = ((-1d) * i * Math.PI) / (2d * signalAfterFFT.Points.Count);
                Complex complexExponent = new Complex
                {
                    Real = Math.Cos(exponent) * CValue(i, signalAfterFFT.Points.Count),
                    Imaginary = 0
                };
                result.Points.Add(new Tuple<double, Complex>(i * f0 / 2, Complex.Multiply(signalAfterFFT.Points.ElementAt(i).Item2, complexExponent)));
                result.Points[i].Item2.Imaginary = 0;
            }
            return result;
        }

        public override string ToString()
        {
            return "FCT typu drugiego";
        }
    }
}
