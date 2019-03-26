using CPSProject.Data;
using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
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
        private ICommand addCommand;
        private ICommand subtractCommand;
        private ICommand multiplyCommand;
        private ICommand divideCommand;
        private SignalRepresentation firstSignal;
        private SignalRepresentation secondSignal;
        private PlotModel realPlotModel;
        private PlotModel imaginaryPlotModel;
        private PlotModel realHisogramPlotModel;
        private LineSeries realSeries1;
        private LineSeries imaginarySeries1;
        private LineSeries realSeries2;
        private LineSeries imaginarySeries2;
        private LineSeries realCombinedSeries;
        private LineSeries imaginaryCombinedSeries;
        private ColumnSeries realHistogramSeries;
        private SignalTextProperties textProperties1;
        private SignalTextProperties textProperties2;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public int FirstChartID { get; set; }
        public int SecondChartID { get; set; }
        public int NumberOfIntervals { get; set; }

        public ICommand DrawCommand1
        {
            get
            {
                if(drawCommand1 == null)
                {
                    drawCommand1 = new RelayCommand(
                        param => Draw(FirstChartID, ref firstSignal, ref realSeries1, ref imaginarySeries1, OxyColors.Blue, ref textProperties1),
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
                        param => Draw(SecondChartID, ref secondSignal, ref realSeries2, ref imaginarySeries2, OxyColors.Red, ref textProperties2),
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

        public PlotModel RealHistogramPlotModel
        {
            get
            {
                return realHisogramPlotModel;
            }
            set
            {
                realHisogramPlotModel = value;
                OnPropertyChanged("RealHistogramPlotModel");
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

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(
                        param => CombineSignals(Complex.Add),
                        param => CanMakeOpperation());
                }
                return addCommand;
            }
        }
        public ICommand SubtractCommand
        {
            get
            {
                if (subtractCommand == null)
                {
                    subtractCommand = new RelayCommand(
                        param => CombineSignals(Complex.Subtract),
                        param => CanMakeOpperation());
                }
                return subtractCommand;
            }
        }
        public ICommand MultiplyCommand
        {
            get
            {
                if (multiplyCommand == null)
                {
                    multiplyCommand = new RelayCommand(
                        param => CombineSignals(Complex.Multiply),
                        param => CanMakeOpperation());
                }
                return multiplyCommand;
            }
        }
        public ICommand DivideCommand
        {
            get
            {
                if (divideCommand == null)
                {
                    divideCommand = new RelayCommand(
                        param => CombineSignals(Complex.Divide),
                        param => CanMakeOpperation());
                }
                return divideCommand;
            }
        }

        public ChartController()
        {
            FirstChartID = -1;
            SecondChartID = -1;
            NumberOfIntervals = 5;

            firstSignal = new SignalRepresentation();
            secondSignal = new SignalRepresentation();
            TextProperties1 = new SignalTextProperties();
            TextProperties2 = new SignalTextProperties();

            realSeries1 = new LineSeries();
            realSeries2 = new LineSeries();
            imaginarySeries1 = new LineSeries();
            imaginarySeries2 = new LineSeries();
            realHistogramSeries = new ColumnSeries();

            realPlotModel = new PlotModel();
            imaginaryPlotModel = new PlotModel();
            RealHistogramPlotModel = new PlotModel();

            RealPlotModel.Series.Add(realSeries1);
            RealPlotModel.Series.Add(realSeries2);
            ImaginaryPlotModel.Series.Add(imaginarySeries1);
            ImaginaryPlotModel.Series.Add(imaginarySeries2);
            RealHistogramPlotModel.Series.Add(realHistogramSeries);
        }

        private void Draw(int id, ref SignalRepresentation signal, ref LineSeries realSeries, ref LineSeries imaginarySeries, OxyColor color, ref SignalTextProperties textTraits)
        {
            if (id == 0) signal.Signal = new UnitaryNoise(signal.frequency, signal.amplitude, signal.startingMoment, signal.duration);
            if (id == 1) signal.Signal = new GaussianNoise(signal.frequency, signal.amplitude, signal.startingMoment, signal.duration);
            if (id == 2) signal.Signal = new SinusoidalSignal(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration);
            if (id == 3) signal.Signal = new SinusoidalSignalHalfRectified(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration);
            if (id == 4) signal.Signal = new SinusoidalSignalFullRectified(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration);
            if (id == 5) signal.Signal = new RectangularSignal(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration, signal.dutyCycle);
            if (id == 6) signal.Signal = new RectangularSimetricalSignal(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration, signal.dutyCycle);
            if (id == 7) signal.Signal = new TriangularSignal(signal.frequency, signal.amplitude, signal.period, signal.startingMoment, signal.duration, signal.dutyCycle);
            if (id == 8) signal.Signal = new StepFunctionSignal(signal.frequency, signal.amplitude, signal.startingMoment, signal.duration, signal.timeOfStep);
            if (id == 9) signal.Signal = new KroneckerDelta(signal.frequency, signal.amplitude, signal.startingMoment, signal.numberOfAllSamples, signal.numberOfSample);
            if (id == 10) signal.Signal = new ImpulseNoise(signal.frequency, signal.amplitude, signal.startingMoment, signal.duration, signal.probability);

            bool b1 = RealPlotModel.Series.Remove(realSeries);
            bool b2 = ImaginaryPlotModel.Series.Remove(imaginarySeries);

            List<DataPoint> realUniversum = new List<DataPoint>();
            List<DataPoint> imaginaryUniversum = new List<DataPoint>();
            foreach(Tuple<double, Complex> tuple in signal.Signal.Points)
            {
                realUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new DataPoint(tuple.Item1, tuple.Item2.Imaginary));
            }
            

            if (signal.Signal.IsLinear)
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

            textTraits.AverageValueText = signal.Signal.AverageValue.ToString("N3");
            textTraits.AbsouluteAverageValueText = signal.Signal.AbsouluteAverageValue.ToString("N3");
            textTraits.AveragePowerText = signal.Signal.AveragePower.ToString("N3");
            textTraits.VarianceText = signal.Signal.Variance.ToString("N3");
            textTraits.EffectiveValueText = signal.Signal.EffectiveValue.ToString("N3");

            OnPropertyChanged("TextProperties1");
            OnPropertyChanged("TextProperties2");

            List<int> realHisogramUniversum = signal.Signal.GenerateRealHistogram(NumberOfIntervals);
            realHistogramSeries = new ColumnSeries();
            foreach (int elem in realHisogramUniversum)
            {
                realHistogramSeries.Items.Add(new ColumnItem(elem));
            }
            RealHistogramPlotModel.Axes.Clear();
            RealHistogramPlotModel.Series.Clear();
            realHistogramSeries.FillColor = color;
            RealHistogramPlotModel.Series.Add(realHistogramSeries);
            RealHistogramPlotModel.InvalidatePlot(true);
        }

        private bool CanMakeOpperation()
        {
            if (firstSignal.Signal != null && secondSignal.Signal != null) return true;
            return false;
        }

        private void CombineSignals(Func<Complex, Complex, Complex> func)
        {
            bool linearity = firstSignal.Signal.IsLinear & secondSignal.Signal.IsLinear;
            List<Tuple<double, Complex>> signal1 = firstSignal.Signal.Points;
            List<Tuple<double, Complex>> signal2 = secondSignal.Signal.Points;
            List<Tuple<double, Complex>> outputSignal = new List<Tuple<double, Complex>>();

            int i = 0;
            int j = 0;
            Complex result;

            while (i < signal1.Count && j < signal2.Count)
            {
                if (signal1[i].Item1 == signal2[j].Item1)
                {
                    result = func(signal1[i].Item2, signal2[j].Item2);
                    outputSignal.Add(new Tuple<double, Complex>(signal1[i].Item1, result));
                    i++;
                    j++;
                }
                else if (signal1[i].Item1 < signal2[j].Item1)
                {
                    result = func(signal1[i].Item2, new Complex { Real = 0, Imaginary = 0 });
                    outputSignal.Add(new Tuple<double, Complex>(signal1[j].Item1, result));
                    i++;
                }
                else if (signal1[i].Item1 > signal2[j].Item1)
                {
                    result = func(signal2[j].Item2, new Complex { Real = 0, Imaginary = 0 });
                    outputSignal.Add(new Tuple<double, Complex>(signal2[j].Item1, result));
                    j++;
                }

            }

            if(linearity)
            {
                realCombinedSeries = new LineSeries();
                imaginaryCombinedSeries = new LineSeries();
            }
            else
            {
                realCombinedSeries = new StemSeries();
                imaginaryCombinedSeries = new StemSeries();
            }

            foreach (Tuple<double, Complex> tuple in outputSignal)
            {
                realCombinedSeries.Points.Add(new DataPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryCombinedSeries.Points.Add(new DataPoint(tuple.Item1, tuple.Item2.Imaginary));
            }

            realCombinedSeries.Color = OxyColors.Purple;
            imaginaryCombinedSeries.Color = OxyColors.Purple;

            realPlotModel.Series.Clear();
            imaginaryPlotModel.Series.Clear();

            realPlotModel.Series.Add(realCombinedSeries);
            imaginaryPlotModel.Series.Add(imaginaryCombinedSeries);

            realPlotModel.InvalidatePlot(true);
            imaginaryPlotModel.InvalidatePlot(true);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
