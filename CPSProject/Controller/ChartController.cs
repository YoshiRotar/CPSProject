using CPSProject.Data;
using CPSProject.Data.Signal;
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
    public class ChartController
    {
        private double frequency;
        private double amplitude;
        private double period;
        private double startingMoment;
        private double duration;
        private double dutyCycle;
        private double timeOfStep;
        private double probability;
        private int numberOfAllSamples;
        private int numberOfSample;
        private ICommand drawCommand;
        private PlotModel realPlotModel;
        public event PropertyChangedEventHandler PropertyChanged;

        ISignal SignalToDraw { get; set; }
        public int FirstChartID { get; set; }
        public string FrequencyText { get; set; }
        public string AmplitudeText { get; set; }
        public string PeriodText { get; set; }
        public string StartingMomentText { get; set; }
        public string DurationText { get; set; }
        public string DutyCycleText { get; set; }
        public string TimeOfStepText { get; set; }
        public string ProbabilityText { get; set; }
        public string NumberOfAllSamplesText { get; set; }
        public string NumberOfSampleText { get; set; }

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



        public ChartController()
        {
            FirstChartID = -1;
            CategoryAxis signalAxis = new CategoryAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                MinimumMinorStep = 1,
                AbsoluteMinimum = 0,
                TicklineColor = OxyColors.Transparent,
            };
            var timeAxis = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                MinimumMajorStep = 1,
                AbsoluteMinimum = 0,
            };

            realPlotModel = new PlotModel();
            realPlotModel.Axes.Add(signalAxis);
            realPlotModel.Axes.Add(timeAxis);
        }

        private bool CanDraw()
        {
            if (FirstChartID == -1) return false;
            if (FirstChartID == 0)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                SignalToDraw = new UnitaryNoise(frequency, amplitude, startingMoment, duration);
            }
            if (FirstChartID == 1)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                SignalToDraw = new GaussianNoise(frequency, amplitude, startingMoment, duration);
            }
            if (FirstChartID == 2)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                SignalToDraw = new SinusoidalSignal(frequency, amplitude, period, startingMoment, duration);
            }
            if (FirstChartID == 3)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                SignalToDraw = new SinusoidalSignalHalfRectified(frequency, amplitude, period, startingMoment, duration);
            }
            if (FirstChartID == 4)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                SignalToDraw = new SinusoidalSignalFullRectified(frequency, amplitude, period, startingMoment, duration);
            }
            if (FirstChartID == 5)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                if (!double.TryParse(DutyCycleText, out dutyCycle)) return false;
                SignalToDraw = new RectangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (FirstChartID == 6)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                if (!double.TryParse(DutyCycleText, out dutyCycle)) return false;
                SignalToDraw = new RectangularSimetricalSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (FirstChartID == 7)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(PeriodText, out period)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                if (!double.TryParse(DutyCycleText, out dutyCycle)) return false;
                SignalToDraw = new TriangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (FirstChartID == 8)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                if (!double.TryParse(TimeOfStepText, out timeOfStep)) return false;
                SignalToDraw = new StepFunctionSignal(frequency, amplitude, startingMoment, duration, timeOfStep);
            }
            if (FirstChartID == 9)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!int.TryParse(NumberOfAllSamplesText, out numberOfAllSamples)) return false;
                if (!int.TryParse(NumberOfSampleText, out numberOfSample)) return false;
                SignalToDraw = new KroneckerDelta(frequency, amplitude, startingMoment, numberOfAllSamples, numberOfSample);
            }
            if (FirstChartID == 10)
            {
                if (!double.TryParse(FrequencyText, out frequency)) return false;
                if (!double.TryParse(AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(DurationText, out duration)) return false;
                if (!double.TryParse(ProbabilityText, out probability)) return false;
                SignalToDraw = new ImpulseNoise(frequency, amplitude, startingMoment, duration, probability);
            }
            return true;
        }

        private void Draw()
        {
            List<DataPoint> realUniversum = new List<DataPoint>();
            List<DataPoint> imaginaryUniversum = new List<DataPoint>();
            foreach(Tuple<double, Complex> tuple in SignalToDraw.Points)
            {
                realUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Imaginary));
            }
            RealPlotModel.Axes.Clear();
            RealPlotModel.Series.Clear();
            LineSeries realSeries;

            if (SignalToDraw.IsLinear) realSeries = new LineSeries();
            else realSeries = new StemSeries();

            realSeries.Color = OxyColors.Blue;
            realSeries.Points.AddRange(realUniversum);
            RealPlotModel.Series.Add(realSeries);
            RealPlotModel.InvalidatePlot(true);
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
