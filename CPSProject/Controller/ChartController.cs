using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSProject.Controller
{
    public class ChartController
    {
        private ICommand drawCommand;

        public ICommand DrawCommand
        {
            get
            {
                if(drawCommand == null)
                {
                    drawCommand = new RelayCommand(
                        param => Draw(),
                        param => CanDraw());
                }
                return drawCommand;
            }
        }

        private int amplitude;
        private int startingMoment;
        private int duration;

        public int FirstChartID { get; set; } = -1;
        public string AmplitudeText { get; set; }
        public string StartingMomentText { get; set; }
        public string DurationText { get; set; }
        public PlotModel RealPlotModel { get; set; }


        private bool CanDraw()
        {
            if (FirstChartID == -1) return false;
            if (FirstChartID == 0)
            {
                if (!int.TryParse(AmplitudeText, out amplitude)) return false;
                if (!int.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!int.TryParse(DurationText, out duration)) return false;
            }
            return true;
        }

        private void Draw()
        {
            //RealPlotModel;
        }
    }
}
