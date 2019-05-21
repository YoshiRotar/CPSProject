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
        public string CutoffFrequency
        {
            get { return cutoffFrequency.Text; }
        }

        public string FilterDegree
        {
            get { return filterDegree.Text; }
        }

        public FilterWindow()
        {
            InitializeComponent();
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
