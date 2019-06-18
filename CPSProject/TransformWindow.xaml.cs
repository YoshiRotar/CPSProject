using CPSProject.Data;
using CPSProject.Data.Filters;
using CPSProject.Data.Signal;
using CPSProject.Data.Transforms;
using CPSProject.Data.WindowFunctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CPSProject
{
    public partial class TransformWindow : Window
    {
        private ObservableCollection<SignalTransform> _transforms = new ObservableCollection<SignalTransform>();
        private SignalTransform _selectedTransform;
        private bool _absoluteValue;

        public SignalTransform SelectedTransform { get => _selectedTransform; set => _selectedTransform = value; }
        public ObservableCollection<SignalTransform> Transforms { get => _transforms; set => _transforms = value; }
        public bool AbsoluteValue { get => _absoluteValue; set => _absoluteValue = value; }

        public TransformWindow()
        {
            DataContext = this;
            Transforms.Add(new DefinitionDFT());
            Transforms.Add(new DecimationInTimeFFT());
            Transforms.Add(new DCT());
            Transforms.Add(new FCT());
            InitializeComponent();
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
