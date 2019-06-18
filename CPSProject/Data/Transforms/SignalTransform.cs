using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Transforms
{
    public abstract class SignalTransform
    {
        abstract public SignalImplementation TransformSignal(ISignal signal);

        protected Complex GetWCoefficient(double upperCoefficient, double lowerCoefficient)
        {
            Complex result = new Complex();
            double exponent = (2.0 * Math.PI * upperCoefficient) / lowerCoefficient;
            result.Real = Math.Cos(exponent);
            result.Imaginary = Math.Sin(exponent);
            return result;
        }

        protected double CValue(int m, double N)
        {
            if (m != 0)
            {
                return Math.Sqrt(2d / N);
            }
            else
            {
                return Math.Sqrt(1d / N);
            }
        }
    }
}
