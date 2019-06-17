using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Transform
{
    public class DefinitionDFT : SignalTransform
    {
        public override SignalImplementation TransformSignal(ISignal signal)
        {
            SignalImplementation result = new SignalImplementation();
            result.Points = new List<Tuple<double, Complex>>();

            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double f0 = samplingFrequency / signal.Points.Count;

            for (int i=0; i<signal.Points.Count; i++)
            {
                double x = i * f0;
                Complex transformValue = Complex.GetZero();
                for(int j = 0; j < signal.Points.Count; j++)
                {
                    Complex wCoefficient = GetWCoefficient(-i * j, signal.Points.Count);
                    Complex product = Complex.Multiply(signal.Points[j].Item2, wCoefficient);
                    transformValue = Complex.Add(transformValue, product);
                }
                transformValue.Real = transformValue.Real/signal.Points.Count;
                transformValue.Imaginary = transformValue.Imaginary / signal.Points.Count;
                result.Points.Add(new Tuple<double, Complex>(x, transformValue));
            }

            return result;
        }

        public override string ToString()
        {
            return "DFT z definicji";
        }
    }
}
