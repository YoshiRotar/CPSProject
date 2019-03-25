using CPSProject.Data;
using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Bases;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSProject.Controller
{
    public class ChartController : INotifyPropertyChanged
    {
        private ICommand drawCommand1;
        private ICommand drawCommand2;
        private SignalRepresentation firstSignal;
        private SignalRepresentation secondSignal;
        private PlotModel realPlotModel;
        private PlotModel imaginaryPlotModel;
        private LineSeries realSeries1;
        private LineSeries imaginarySeries1;
        private LineSeries realSeries2;
        private LineSeries imaginarySeries2;
        private SignalTextProperties textProperties1;
        private SignalTextProperties textProperties2;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public int FirstChartID { get; set; }
        public int SecondChartID { get; set; }

        public ICommand DrawCommand1
        {
            get
            {
                if(drawCommand1 == null)
                {
                    drawCommand1 = new RelayCommand(
                        param => Draw(firstSignal.Signal, ref realSeries1, ref imaginarySeries1, OxyColors.Blue, ref textProperties1),
                        param => firstSignal.CanDraw(FirstChartID,TextProperties1));
                }
                return drawCommand1;
            }
        }

        public ICommand DrawCommand2
        {
            get
            {
                if (drawCommand2 == null)
                {
                    drawCommand2 = new RelayCommand(
                        param => Draw(secondSignal.Signal, ref realSeries2, ref imaginarySeries2, OxyColors.Red, ref textProperties2),
                        param => secondSignal.CanDraw(SecondChartID, TextProperties2));
                }
                return drawCommand2;
            }
        }

        public PlotModel RealPlotModel
        {
            get
            {
                return realPlotModel;
            }
            set
            {
                realPlotModel = value;
                OnPropertyChanged("RealPlotModel");
            }
        }

        public PlotModel ImaginaryPlotModel
        {
            get
            {
                return imaginaryPlotModel;
            }
            set
            {
                imaginaryPlotModel = value;
                OnPropertyChanged("ImaginaryPlotModel");
            }
        }

        public SignalTextProperties TextProperties1
        {
            get
            {
                return textProperties1;
            }
            set
            {
                textProperties1 = value;
                OnPropertyChanged("TextProperties1");
            }
        }
        public SignalTextProperties TextProperties2
        {
            get
            {
                return textProperties2;
            }
            set
            {
                textProperties2 = value;
                OnPropertyChanged("TextProperties2");
            }
        }

        public ChartController()
        {
            FirstChartID = -1;
            SecondChartID = -1;

            firstSignal = new SignalRepresentation();
            secondSignal = new SignalRepresentation();
            TextProperties1 = new SignalTextProperties();
            TextProperties2 = new SignalTextProperties();

            realSeries1 = new LineSeries();
            realSeries2 = new LineSeries();
            imaginarySeries1 = new LineSeries();
            imaginarySeries2 = new LineSeries();
            realPlotModel = new PlotModel();
            imaginaryPlotModel = new PlotModel();

            RealPlotModel.Series.Add(realSeries1);
            RealPlotModel.Series.Add(realSeries2);
            ImaginaryPlotModel.Series.Add(imaginarySeries1);
            ImaginaryPlotModel.Series.Add(imaginarySeries2);
        }

        private void Draw(ISignal signal, ref LineSeries realSeries, ref LineSeries imaginarySeries, OxyColor color, ref SignalTextProperties textTraits)
        {
            bool b1 = RealPlotModel.Series.Remove(realSeries);
            bool b2 = ImaginaryPlotModel.Series.Remove(imaginarySeries);

            List<DataPoint> realUniversum = new List<DataPoint>();
            List<DataPoint> imaginaryUniversum = new List<DataPoint>();
            foreach(Tuple<double, Complex> tuple in signal.Points)
            {
                realUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Imaginary));
            }
            

            if (signal.IsLinear)
            {
                realSeries = new LineSeries();
                imaginarySeries = new LineSeries();
            }
            else
            {
                realSeries = new StemSeries();
                imaginarySeries = new StemSeries();
            }

            realSeries.Color = color;
            imaginarySeries.Color = color;
            realSeries.Points.AddRange(realUniversum);
            imaginarySeries.Points.AddRange(imaginaryUniversum);
            RealPlotModel.Series.Add(realSeries);
            ImaginaryPlotModel.Series.Add(imaginarySeries);
            RealPlotModel.InvalidatePlot(true);
            ImaginaryPlotModel.InvalidatePlot(true);

            textTraits.AverageValueText = signal.AverageValue.ToString("N3");
            textTraits.AbsouluteAverageValueText = signal.AbsouluteAverageValue.ToString("N3");
            textTraits.AveragePowerText = signal.AveragePower.ToString("N3");
            textTraits.VarianceText = signal.Variance.ToString("N3");
            textTraits.EffectiveValueText = signal.EffectiveValue.ToString("N3");

            OnPropertyChanged("TextProperties1");
            OnPropertyChanged("TextProperties2");
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
