using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Transform
{
    public abstract class SignalTransform
    {
        abstract public SignalImplementation TransformSignal(ISignal signal);
    }
}
