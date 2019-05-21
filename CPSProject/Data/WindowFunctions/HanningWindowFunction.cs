using CPSProject.Data.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.WindowFunctions
{
    class HanningWindowFunction : IWindow
    {
        public double getValue(int n, int filterDegree)
        {
            return 0.5 - (0.5 * Math.Cos(2 * Math.PI * n / filterDegree));
        }

        public override string ToString()
        {
            return "Hanning";
        }
    }
}
