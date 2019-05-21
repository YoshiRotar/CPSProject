using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Filters
{
    public interface IFilter
    {
        void InitFilter(double cutoffFrequency, int filterDegree, IWindow windowFunction);
        SignalImplementation Filter(ISignal signal);
    }
}
