using CPSProject.Data;
using CPSProject.Data.Filters;
using CPSProject.Data.Signal;
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
    /// <summary>
    /// Interaction logic for QuentizeWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        private ObservableCollection<IWindow> _windowFunctions = new ObservableCollection<IWindow>();

        private IWindow _selectedWindow;

        private ObservableCollection<Filter> _filters = new ObservableCollection<Filter>();

        private Filter _selectedFilter;

        public string CutoffFrequency
        {
            get { return cutoffFrequency.Text; }
        }

        public string FilterDegree
        {
            get { return filterDegree.Text; }
        }
        
        public ObservableCollection<IWindow> WindowFunctions
        {
            get { return _windowFunctions; }
            set { _windowFunctions = value; }
        }

        public IWindow SelectedWindowFunction
        {
            get { return _selectedWindow; }
            set { _selectedWindow = value; }
        }

        public ObservableCollection<Filter> Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }

        public Filter SelectedFilter
        {
            get { return _selectedFilter; }
            set { _selectedFilter = value; }
        }

        public FilterWindow()
        {
            DataContext = this;
            WindowFunctions.Add(new RectangleWindowFunction());
            WindowFunctions.Add(new HanningWindowFunction());
            Filters.Add(new LowPassFilter());
            Filters.Add(new HighPassFilter());
            InitializeComponent();
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
