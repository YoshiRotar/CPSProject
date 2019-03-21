﻿using CPSProject.Data;
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

        private double frequency;
        private double amplitude;
        private double startingMoment;
        private double duration;

        public int FirstChartID { get; set; } = -1;
        public string FrequencyText { get; set; }
        public string AmplitudeText { get; set; }
        public string StartingMomentText { get; set; }
        public string DurationText { get; set; }
        public PlotModel RealPlotModel { get; set; }


        private bool CanDraw()
        {
            if (FirstChartID == -1) return false;
            if (FirstChartID == 0)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
            }
            return true;
        }

        private void Draw()
        {
            UnitaryNoise unitaryNoise = new UnitaryNoise(frequency, amplitude, startingMoment, duration);
            List<Tuple<double, double>> realUniversum = new List<Tuple<double, double>>();
            List<Tuple<double, double>> imaginaryUniversum = new List<Tuple<double, double>>();
            foreach(Tuple<double, Complex> tuple in unitaryNoise.Points)
            {
                realUniversum.Add(new Tuple<double, double>(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new Tuple<double, double>(tuple.Item1, tuple.Item2.Imaginary));
            }
        }
    }
}
