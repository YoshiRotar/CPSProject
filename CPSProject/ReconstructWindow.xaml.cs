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
    /// Interaction logic for ReconstructWindow.xaml
    /// </summary>
    public partial class ReconstructWindow : Window
    {
        public ReconstructWindow()
        {
            InitializeComponent();
        }

        public string FrequencyToReconstruct
        {
            get { return Frequency.Text; }
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
