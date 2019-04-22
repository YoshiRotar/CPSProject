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
using System.Windows;
using System.Windows.Input;

namespace CPSProject.Controller
{
    public class ChartController : INotifyPropertyChanged
    {
        private ICommand drawCommand;
        private ICommand saveCommand;
        private ICommand loadCommand;
        private ICommand addCommand;
        private ICommand subtractCommand;
        private ICommand multiplyCommand;
        private ICommand divideCommand;
        private ICommand clearCommand;
        private ICommand quantizeCommand;
        private ICommand openQuantizeWindow;
        private SignalRepresentation combinedSignal;
        private PlotModel realPlotModel;
        private PlotModel imaginaryPlotModel;
        private PlotModel realHistogramPlotModel;
        private ChartRepresentation firstChart;
        private ChartRepresentation secondChart;
        private ScatterSeries realCombinedSeries;
        private ScatterSeries imaginaryCombinedSeries;
        private ColumnSeries realHistogramSeries;
        private ColumnSeries realCombinedHistogramSeries;
        private SignalTextProperties combinedTextProperties;
        private string samplingFrequencyText;
        private string numberOfQuantizationLevelsText;
        public int numberOfQuantizationLevels;
        public double samplingFrequency;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public int NumberOfIntervals { get; set; }
        

        public string SamplingFrequencyText
        {
            get
            {
                return samplingFrequencyText;
            }
            set
            {
                samplingFrequencyText = value;
                OnPropertyChanged("SamplingFrequencyText");
            }
        }

        public string NumberOfQuantizationLevelsText
        {
            get
            {
                return numberOfQuantizationLevelsText;
            }
            set
            {
                numberOfQuantizationLevelsText = value;
                OnPropertyChanged("NumberOfQuantizationLevels");
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

        public ChartRepresentation FirstChart
        {
            get
            {
                return firstChart;
            }
            set
            {
                firstChart = value;
                OnPropertyChanged("FirstChart");
            }
        }

        public ChartRepresentation SecondChart
        {
            get
            {
                return secondChart;
            }
            set
            {
                secondChart = value;
                OnPropertyChanged("SecondChart");
            }
        }

        public SignalTextProperties CombinedTextProperties
        {
            get
            {
                return combinedTextProperties;
            }
            set
            {
                combinedTextProperties = value;
                OnPropertyChanged("CombinedTextProperties");
            }
        }

        public ICommand DrawCommand
        {
            get
            {
                if(drawCommand == null)
                {
                    drawCommand = new RelayCommand(
                        param => Draw(param),
                        param => CanDraw(param));
                }
                return drawCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(
                        param => SavePlot(param),
                        param => CanSavePlot(param));
                }
                return saveCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => { LoadPlot(param); Draw(param); },
                        param => true);
                }
                return loadCommand;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(
                        param => ClearPlot(),
                        param => CanClear());
                }
                return clearCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(
                        param => CombineSignals(Complex.Add, combinedTextProperties),
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
                        param => CombineSignals(Complex.Subtract, combinedTextProperties),
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
                        param => CombineSignals(Complex.Multiply, combinedTextProperties),
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
                        param => CombineSignals(Complex.Divide, combinedTextProperties),
                        param => CanMakeOpperation());
                }
                return divideCommand;
            }
        }

        public ICommand QuantizeCommand
        {
            get
            {
                if(quantizeCommand == null)
                {
                    quantizeCommand = new RelayCommand(
                        param => Quantize(param),
                        param => CanQuantize());
                }
                return quantizeCommand;
            }
        }

        public ICommand OpenQuantizeWindow
        {
            get
            {
                if (openQuantizeWindow == null)
                {
                    openQuantizeWindow = new RelayCommand(
                        param => OpenNewWindow(param),
                        param => CanOpenQuantize(param));
                }
                return openQuantizeWindow;
            }
        }

        public ChartController()
        {
            FirstChart = new ChartRepresentation
            {
                SelectedIndex = -1,
                SignalRepresentation = new SignalRepresentation(),
                SignalProperties = new SignalTextProperties(),
                PlotColor = OxyColors.Blue,
                RealSeries = new ScatterSeries(),
                ImaginarySeries = new ScatterSeries()
            };
            SecondChart = new ChartRepresentation
            {
                SelectedIndex = -1,
                SignalRepresentation = new SignalRepresentation(),
                SignalProperties = new SignalTextProperties(),
                PlotColor = OxyColors.Red,
                RealSeries = new ScatterSeries(),
                ImaginarySeries = new ScatterSeries()
            };

            NumberOfIntervals = 5;

            combinedSignal = new SignalRepresentation
            {
                Signal = new SignalImplementation()
            };
            combinedSignal.Signal.Points = new List<Tuple<double, Complex>>();   
           
            CombinedTextProperties = new SignalTextProperties();
            
            realCombinedSeries = new ScatterSeries();
            imaginaryCombinedSeries = new ScatterSeries();
            realHistogramSeries = new ColumnSeries();

            realPlotModel = new PlotModel();
            imaginaryPlotModel = new PlotModel();
            realHistogramPlotModel = new PlotModel();

            RealPlotModel.Series.Add(FirstChart.RealSeries);
            RealPlotModel.Series.Add(SecondChart.RealSeries);
            ImaginaryPlotModel.Series.Add(FirstChart.ImaginarySeries);
            ImaginaryPlotModel.Series.Add(SecondChart.ImaginarySeries);
            realHistogramPlotModel.Series.Add(realHistogramSeries);
        }

        private bool CanDraw(object param)
        {
            switch (param.ToString())
            {
                case "1":
                    return FirstChart.CanDraw();
                case "2":
                    return SecondChart.CanDraw();
                default:
                    return false;
            }
        }

        private void Draw(object param)
        {
            ChartRepresentation chart;

            switch(param.ToString())
            {
                case "1":
                    chart = FirstChart;
                    break;
                case "2":
                    chart = SecondChart;
                    break;
                default:
                    throw new ArgumentException();
            }

            if (chart.SelectedIndex == 0) chart.SignalRepresentation.Signal = new UnitaryNoise(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration);
            if (chart.SelectedIndex == 1) chart.SignalRepresentation.Signal = new GaussianNoise(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration);
            if (chart.SelectedIndex == 2) chart.SignalRepresentation.Signal = new SinusoidalSignal(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration);
            if (chart.SelectedIndex == 3) chart.SignalRepresentation.Signal = new SinusoidalSignalHalfRectified(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration);
            if (chart.SelectedIndex == 4) chart.SignalRepresentation.Signal = new SinusoidalSignalFullRectified(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration);
            if (chart.SelectedIndex == 5) chart.SignalRepresentation.Signal = new RectangularSignal(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration, chart.SignalRepresentation.dutyCycle);
            if (chart.SelectedIndex == 6) chart.SignalRepresentation.Signal = new RectangularSimetricalSignal(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration, chart.SignalRepresentation.dutyCycle);
            if (chart.SelectedIndex == 7) chart.SignalRepresentation.Signal = new TriangularSignal(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.period, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration, chart.SignalRepresentation.dutyCycle);
            if (chart.SelectedIndex == 8) chart.SignalRepresentation.Signal = new StepFunctionSignal(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration, chart.SignalRepresentation.timeOfStep);
            if (chart.SelectedIndex == 9) chart.SignalRepresentation.Signal = new KroneckerDelta(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.numberOfAllSamples, chart.SignalRepresentation.numberOfSample);
            if (chart.SelectedIndex == 10) chart.SignalRepresentation.Signal = new ImpulseNoise(chart.SignalRepresentation.frequency, chart.SignalRepresentation.amplitude, chart.SignalRepresentation.startingMoment, chart.SignalRepresentation.duration, chart.SignalRepresentation.probability);

            if (chart.SignalRepresentation.Signal == null) return;

            combinedSignal.Signal.Points.RemoveRange(0, combinedSignal.Signal.Points.Count);

            RealPlotModel.Series.Remove(chart.RealSeries);
            ImaginaryPlotModel.Series.Remove(chart.ImaginarySeries);
            RealPlotModel.Series.Remove(realCombinedSeries);
            ImaginaryPlotModel.Series.Remove(imaginaryCombinedSeries);

            realPlotModel.Axes.Clear();
            imaginaryPlotModel.Axes.Clear();

            List<ScatterPoint> realUniversum = new List<ScatterPoint>();
            List<ScatterPoint> imaginaryUniversum = new List<ScatterPoint>();
            foreach(Tuple<double, Complex> tuple in chart.SignalRepresentation.Signal.Points)
            {
                realUniversum.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryUniversum.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Imaginary));
            }
            

            chart.RealSeries = new ScatterSeries();
            chart.ImaginarySeries = new ScatterSeries();

            chart.RealSeries.MarkerType = MarkerType.Circle;
            chart.ImaginarySeries.MarkerType = MarkerType.Circle;
            chart.RealSeries.MarkerFill = chart.PlotColor;
            chart.ImaginarySeries.MarkerFill = chart.PlotColor;
            chart.RealSeries.MarkerSize = 1;
            chart.ImaginarySeries.MarkerSize = 1;

            chart.RealSeries.Points.AddRange(realUniversum);
            chart.ImaginarySeries.Points.AddRange(imaginaryUniversum);
            RealPlotModel.Series.Add(chart.RealSeries);
            ImaginaryPlotModel.Series.Add(chart.ImaginarySeries);
            RealPlotModel.InvalidatePlot(true);
            ImaginaryPlotModel.InvalidatePlot(true);

            chart.SignalProperties.AverageValueText = chart.SignalRepresentation.Signal.AverageValue.ToString("N3");
            chart.SignalProperties.AbsouluteAverageValueText = chart.SignalRepresentation.Signal.AbsouluteAverageValue.ToString("N3");
            chart.SignalProperties.AveragePowerText = chart.SignalRepresentation.Signal.AveragePower.ToString("N3");
            chart.SignalProperties.VarianceText = chart.SignalRepresentation.Signal.Variance.ToString("N3");
            chart.SignalProperties.EffectiveValueText = chart.SignalRepresentation.Signal.EffectiveValue.ToString("N3");

            OnPropertyChanged("FirstChart");
            OnPropertyChanged("SecondChart");

            List<int> realHisogramUniversum = chart.SignalRepresentation.Signal.GenerateRealHistogram(NumberOfIntervals);
            realHistogramSeries = new ColumnSeries();
            foreach (int elem in realHisogramUniversum)
            {
                realHistogramSeries.Items.Add(new ColumnItem(elem));
            }
            realHistogramPlotModel.Axes.Clear();
            realHistogramPlotModel.Series.Clear();
            realHistogramSeries.FillColor = chart.PlotColor;
            realHistogramPlotModel.Series.Add(realHistogramSeries);
            realHistogramPlotModel.InvalidatePlot(true);
        }

        private bool CanOpenQuantize(object param)
        {
            switch (param.ToString())
            {
                case "1":
                    return FirstChart.SignalRepresentation.Signal != null;
                default:
                    return false;
            }
        }

        private void OpenNewWindow(object param)
        {
            Window quantizeWindow = new QuentizeWindow
            {
                Tag = param
            };
            quantizeWindow.ShowDialog();
        }

        private bool CanQuantize()
        {
            if (!double.TryParse(SamplingFrequencyText, out samplingFrequency)) return false;
            if (!int.TryParse(NumberOfQuantizationLevelsText, out numberOfQuantizationLevels)) return false;
            return true;
        }

        private void Quantize(object param)
        {
            switch(param.ToString())
            {
                case "1":

                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private bool CanMakeOpperation()
        {
            if (FirstChart.SignalRepresentation.Signal != null && SecondChart.SignalRepresentation.Signal != null) return true;
            return false;
        }

        private void ClearPlot()
        {
            realPlotModel.Series.Clear();
            imaginaryPlotModel.Series.Clear();
            RealHistogramPlotModel.Series.Clear();
            realPlotModel.InvalidatePlot(true);
            imaginaryPlotModel.InvalidatePlot(true);
            RealHistogramPlotModel.InvalidatePlot(true);
        }

        private void CombineSignals(Func<Complex, Complex, Complex> func, SignalTextProperties textTraits)
        {
            List<Tuple<double, Complex>> signal1 = FirstChart.SignalRepresentation.Signal.Points;
            List<Tuple<double, Complex>> signal2 = SecondChart.SignalRepresentation.Signal.Points;
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

            SignalImplementation signalImplementation = new SignalImplementation
            {
                Points = new List<Tuple<double, Complex>>()
            };

            foreach (Tuple<double, Complex> tuple in outputSignal)
            {
                realCombinedSeries.Points.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Real));
                imaginaryCombinedSeries.Points.Add(new ScatterPoint(tuple.Item1, tuple.Item2.Imaginary));
                signalImplementation.Points.Add(tuple);
            }

            ClearPlot();

            signalImplementation.StartingMoment = signalImplementation.Points[0].Item1;
            signalImplementation.EndingMoment = signalImplementation.Points[signalImplementation.Points.Count-1].Item1;
            signalImplementation.CalculateTraits();
            combinedSignal.Signal = signalImplementation;

            textTraits.AverageValueText = combinedSignal.Signal.AverageValue.ToString("N3");
            textTraits.AbsouluteAverageValueText = combinedSignal.Signal.AbsouluteAverageValue.ToString("N3");
            textTraits.AveragePowerText = combinedSignal.Signal.AveragePower.ToString("N3");
            textTraits.VarianceText = combinedSignal.Signal.Variance.ToString("N3");
            textTraits.EffectiveValueText = combinedSignal.Signal.EffectiveValue.ToString("N3");

            OnPropertyChanged("TextProperties3");

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

            FirstChart.SignalRepresentation.Signal = null;
            SecondChart.SignalRepresentation.Signal = null;
        }

        private void SavePlot(object param)
        {
            ISignal signal;

            switch(param.ToString())
            {
                case "1":
                    signal = FirstChart.SignalRepresentation.Signal;
                    break;
                case "2":
                    signal = SecondChart.SignalRepresentation.Signal;
                    break;
                case "3":
                    signal = combinedSignal.Signal;
                    break;
                default:
                    throw new ArgumentException();
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = "xD",
                AddExtension = true
            };
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

        private bool CanSavePlot(object param)
        {
            ISignal signal;
            switch(param.ToString())
            {
                case "1":
                    signal = FirstChart.SignalRepresentation.Signal;
                    break;
                case "2":
                    signal = SecondChart.SignalRepresentation.Signal;
                    break;
                case "3":
                    signal = combinedSignal.Signal;
                    break;
                default:
                    return false;
            }
            if (signal == null) return false;
            return (signal.Points.Count != 0);
        }

        private bool CanClear()
        {
            if (RealPlotModel.Axes.Count != 0) return true;
            if (ImaginaryPlotModel.Axes.Count != 0) return true;
            if (RealHistogramPlotModel.Axes.Count != 0) return true;
            return false;
        }

        private void LoadPlot(object param)
        {
            SignalRepresentation signalRepresentation;

            switch(param.ToString())
            {
                case "1":
                    signalRepresentation = FirstChart.SignalRepresentation;
                    FirstChart.SelectedIndex = -1;
                    break;
                case "2":
                    signalRepresentation = SecondChart.SignalRepresentation;
                    SecondChart.SelectedIndex = -1;
                    break;
                default:
                    throw new ArgumentException();
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "*All Files | *.xD"
            };
            if (dlg.ShowDialog() == false) return;
            using (BinaryReader reader = new BinaryReader(File.Open(dlg.FileName, FileMode.Open)))
            {
                SignalImplementation signalImplementation = new SignalImplementation
                {
                    Points = new List<Tuple<double, Complex>>()
                };
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    double xCoordinate = reader.ReadDouble();
                    Complex value = new Complex
                    {
                        Real = reader.ReadDouble(),
                        Imaginary = reader.ReadDouble()
                    };
                    signalImplementation.Points.Add(new Tuple<double, Complex>(xCoordinate, value));
                }

                signalImplementation.StartingMoment = signalImplementation.Points[0].Item1;
                signalImplementation.EndingMoment = signalImplementation.Points[signalImplementation.Points.Count - 1].Item1;
                signalImplementation.CalculateTraits();
                signalRepresentation.Signal = signalImplementation;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
