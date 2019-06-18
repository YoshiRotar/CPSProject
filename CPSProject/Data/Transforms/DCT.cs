using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPSProject.Data.Signal.Base;

namespace CPSProject.Data.Transforms
{
    public class DCT : SignalTransform
    {
        public override SignalImplementation TransformSignal(ISignal signal)
        {
            SignalImplementation result = new SignalImplementation
            {
                Points = new List<Tuple<double, Complex>>()
            };

            double samplingFrequency = 1d / (signal.Points[1].Item1 - signal.Points[0].Item1);
            double f0 = samplingFrequency / signal.Points.Count;

            for (int i = 0; i < signal.Points.Count; i++)
            {
                double x = i * f0 / 2;
                Complex transformValue = Complex.GetZero();
                for (int j = 0; j < signal.Points.Count; j++)
                {
                    Complex cosCoefficient = new Complex
                    {
                        Real = Math.Cos((Math.PI * (2d * j + 1d) * i) / (2d * signal.Points.Count)),
                        Imaginary = 0d
                    };
                    Complex product = Complex.Multiply(signal.Points[j].Item2, cosCoefficient);
                    transformValue = Complex.Add(transformValue, product);
                }
                transformValue.Real = transformValue.Real * CValue(i, signal.Points.Count);
                result.Points.Add(new Tuple<double, Complex>(x, transformValue));
            }

            return result;
        }

        public override string ToString()
        {
            return "DCT typu drugiego";
        }
    }
}
