using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Controller
{
    public class ChartRepresentation
    {
        public int SelectedIndex { get; set; }
        public SignalRepresentation SignalRepresentation { get; set; }
        public SignalTextProperties SignalProperties { get; set; }
        public ScatterSeries RealSeries { get; set; }
        public ScatterSeries ImaginarySeries { get; set; }
        public OxyColor PlotColor { get; set; }

        public bool CanDraw()
        {
            return SignalRepresentation.CanDraw(SelectedIndex, SignalProperties);
        }
    }
}
