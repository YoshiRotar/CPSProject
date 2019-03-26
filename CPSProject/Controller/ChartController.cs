using CPSProject.Data;
using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private ICommand saveCommand1;
        private ICommand loadCommand1;
        private ICommand saveCommand2;
        private ICommand loadCommand2;
        private ICommand saveCommand3;
        private ICommand addCommand;
        private ICommand subtractCommand;
        private ICommand multiplyCommand;
        private ICommand divideCommand;
        private SignalRepresentation firstSignal;
        private SignalRepresentation secondSignal;
        private PlotModel realPlotModel;
        private PlotModel imaginaryPlotModel;
        private PlotModel realHistogramPlotModel;
        private ScatterSeries realSeries1;
        private ScatterSeries imaginarySeries1;
        private ScatterSeries realSeries2;
        private ScatterSeries imaginarySeries2;
        private ScatterSeries realCombinedSeries;
        private ScatterSeries imaginaryCombinedSeries;
        private ColumnSeries realHistogramSeries;
        private ColumnSeries realCombinedHistogramSeries;
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

        public ICommand SaveCommand1
        {
            get
            {
                if (saveCommand1 == null)
                {
                    saveCommand1 = new RelayCommand(
                        param => SavePlot(firstSignal.Signal),
                        param => CanSavePlot(firstSignal.Signal));
                }
                return saveCommand1;
            }
        }

        public ICommand SaveCommand2
        {
            get
            {
                if (saveCommand2 == null)
                {
                    saveCommand2 = new RelayCommand(
                        param => SavePlot(secondSignal.Signal),
                        param => CanSavePlot(secondSignal.Signal));
                }
                return saveCommand2;
            }
        }

        public ICommand SaveCommand3
        {
            get
            {
                if (saveCommand3 == null)
                {
                    saveCommand3 = new RelayCommand(
                        param => SavePlot(secondSignal.Signal),
                        param => CanSavePlot(secondSignal.Signal));
                }
                return saveCommand3;
            }
        }

        public ICommand LoadCommand1
        {
            get
            {
                if (loadCommand1 == null)
                {
                    loadCommand1 = new RelayCommand(
                        param => { LoadPlot(firstSignal); Draw(-1, ref firstSignal, ref realSeries1, ref imaginarySeries1, OxyColors.Blue, ref textProperties1); },
                        param => true);
                }
                return loadCommand1;
            }
        }

        public ICommand LoadCommand2
        {
            get
            {
                if (loadCommand2 == null)
                {
                    loadCommand2 = new RelayCommand(
                        param => { LoadPlot(secondSignal); Draw(-1, ref secondSignal, ref realSeries2, ref imaginarySeries2, OxyColors.Red, ref textProperties2); },
                        param => true);
                }
                return loadCommand2;
            }
        }

        private void SavePlot(ISignal signal)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = "xD";
            dlg.AddExtension = true;
            if (dlg.ShowDialog() == false) return;
            using (BinaryWriter writer = new BinaryWriter(File.Open(dlg.FileName, FileMode.Create)))
            {
                foreach (Tuple<double, Complex> point in signal.Points)
                {
                    writer.Write(point.Item1);
                    writer.Write(point.Item2.Real);
                    writer.Write(point.Item2.Imaginary);
                }
            }
        }

        private bool CanSavePlot(ISignal signal)
        {
            if (signal == null) return false;
            return (signal.Points.Count != 0);
        }

        private void LoadPlot(SignalRepresentation signalRepresentation)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "*All Files | *.xD";
            if (dlg.ShowDialog() == false) return;
            using (BinaryReader reader = new BinaryReader(File.Open(dlg.FileName, FileMode.Open)))
            {
                signalRepresentation.Signal = new SignalImplementation();
                signalRepresentation.Signal.Points = new List<Tuple<double, Complex>>();
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    double xCoordinate = reader.ReadDouble();
                    Complex value = new Complex();
                    value.Real = reader.ReadDouble();
                    value.Imaginary = reader.ReadDouble();
                    signalRepresentation.Signal.Points.Add(new Tuple<double, Complex>(xCoordinate, value));
                }
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
                return realHistogramPlotModel;
            }
            set
            {
                realHistogramPlotModel = value;
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

            realSeries1 = new ScatterSeries();
            realSeries2 = new ScatterSeries();
            imaginarySeries1 = new ScatterSeries();
            imaginarySeries2 = new ScatterSeries();
            realCombinedSeries = new ScatterSeries();
            imaginaryCombinedSeries = new ScatterSeries();
            realHistogramSeries = new ColumnSeries();

            realPlotModel = new PlotModel();
            imaginaryPlotModel = new PlotModel();
            realHistogramPlotModel = new PlotModel();

            RealPlotModel.Series.Add(realSeries1);
            RealPlotModel.Series.Add(realSeries2);
            ImaginaryPlotModel.Series.Add(imaginarySeries1);
            ImaginaryPlotModel.Series.Add(imaginarySeries2);
            realHistogramPlotModel.Series.Add(realHistogramSeries);
        }

        private void Draw(int id, ref SignalRepresentation signal, ref ScatterSeries realSeries, ref ScatterSeries imaginarySeries, OxyColor color, ref SignalTextProperties textTraits)
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

            RealPlotModel.Series.Remove(realSeries);
            ImaginaryPlotModel.Series.Remove(imaginarySeries);
            RealPlotModel.Series.Remove(realCombinedSeries);
            ImaginaryPlotModel.Series.Remove(imaginaryCombinedSeries);

            realPlotModel.Axes.Clear();
            imaginaryPlotModel.Axes.Clear();

            List<ScatterPoint> realUniversum = new List<ScatterPoint>();
            List<ScatterPoint> imaginaryUniversum = new List<ScatterPoint>();
            foreach(Tuple<double, Complex> tuple in signal.Signal.Points)
            {
                realUniversum.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Imaginary));
            }
            

            realSeries = new ScatterSeries();
            imaginarySeries = new ScatterSeries();

            realSeries.MarkerType = MarkerType.Circle;
            imaginarySeries.MarkerType = MarkerType.Circle;
            realSeries.MarkerFill = color;
            imaginarySeries.MarkerFill = color;
            realSeries.MarkerSize = 1;
            imaginarySeries.MarkerSize = 1;

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
            realHistogramPlotModel.Axes.Clear();
            realHistogramPlotModel.Series.Clear();
            realHistogramSeries.FillColor = color;
            realHistogramPlotModel.Series.Add(realHistogramSeries);
            realHistogramPlotModel.InvalidatePlot(true);
        }

        private bool CanMakeOpperation()
        {
            if (firstSignal.Signal != null && secondSignal.Signal != null) return true;
            return false;
        }

        private void CombineSignals(Func<Complex, Complex, Complex> func)
        {
            List<Tuple<double, Complex>> signal1 = firstSignal.Signal.Points;
            List<Tuple<double, Complex>> signal2 = secondSignal.Signal.Points;
            List<Tuple<double, Complex>> outputSignal = new List<Tuple<double, Complex>>();

            int i = 0;
            int j = 0;
            Complex result;

            while (i < signal1.Count && j < signal2.Count)
            {
                if (Math.Abs(signal1[i].Item1 - signal2[j].Item1) <= 0.001)
                {
                    result = func(signal1[i].Item2, signal2[j].Item2);
                    outputSignal.Add(new Tuple<double, Complex>(signal1[i].Item1, result));
                    i++;
                    j++;
                }
                else if (signal1[i].Item1 - signal2[j].Item1 > 0.001 )
                {
                    j++;
                }
                else if (signal2[j].Item1 - signal1[i].Item1 > 0.001)
                {
                    i++;
                }

            }

            realPlotModel.Axes.Clear();
            imaginaryPlotModel.Axes.Clear();

            realCombinedSeries = new ScatterSeries();
            imaginaryCombinedSeries = new ScatterSeries();

            realCombinedSeries.MarkerType = MarkerType.Circle;
            imaginaryCombinedSeries.MarkerType = MarkerType.Circle;
            realCombinedSeries.MarkerFill = OxyColors.Purple;
            imaginaryCombinedSeries.MarkerFill = OxyColors.Purple;
            realCombinedSeries.MarkerSize = 1;
            imaginaryCombinedSeries.MarkerSize = 1;

            foreach (Tuple<double, Complex> tuple in outputSignal)
            {
                realCombinedSeries.Points.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryCombinedSeries.Points.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Imaginary));
            }

            realPlotModel.Series.Clear();
            imaginaryPlotModel.Series.Clear();

            realPlotModel.Series.Add(realCombinedSeries);
            imaginaryPlotModel.Series.Add(imaginaryCombinedSeries);

            realPlotModel.InvalidatePlot(true);
            imaginaryPlotModel.InvalidatePlot(true);

            double min = outputSignal.Min(d => d.Item2.Real);
            double max = outputSignal.Max(d => d.Item2.Real);
            double universumWidth = max - min;
            double intervalWidth = universumWidth / NumberOfIntervals;

            int[] histogramUniversum = new int[NumberOfIntervals];

            for (int k = 0; k < outputSignal.Count; k++)
            {
                int interval = (int)Math.Floor((outputSignal[k].Item2.Real - min) / (intervalWidth + 0.0001));
                histogramUniversum[interval]++;
            }

            List<int> realHisogramUniversum = histogramUniversum.ToList();
            realCombinedHistogramSeries = new ColumnSeries();
            foreach (int elem in realHisogramUniversum)
            {
                realCombinedHistogramSeries.Items.Add(new ColumnItem(elem));
            }

            realHistogramPlotModel.Axes.Clear();
            realHistogramPlotModel.Series.Clear();
            realCombinedHistogramSeries.FillColor = OxyColors.Purple;
            realHistogramPlotModel.Series.Add(realCombinedHistogramSeries);
            realHistogramPlotModel.InvalidatePlot(true);

            firstSignal.Signal = null;
            secondSignal.Signal = null;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
