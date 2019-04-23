using System;
using System.Collections.Generic;
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
    public partial class QuantizeWindow : Window
    {
        public string SamplingFrequency
        {
            get { return Sampling.Text; }
        }

        public string LevelsOfQuantization
        {
            get { return Levels.Text; }
        }

        public QuantizeWindow()
        {
            InitializeComponent();
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
