using CPSProject.Data.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.WindowFunctions
{
    class RectangleWindowFunction : IWindow
    {
        public double getValue(int n, int filterDegree)
        {
            int rectangleLimit = (filterDegree - 1) / 2;
            if (n > rectangleLimit || n < -rectangleLimit) return 0;
            else return 1;
        }
    }
}
