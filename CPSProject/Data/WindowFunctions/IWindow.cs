using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data.Signal
{
    public interface IWindow
    {
        double getValue(int n, int filterDegree);
    }
}
