using FlightSimulator.ViewModel.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

// The view code for the the manual xaml.
namespace FlightSimulator.Views {
    public partial class Manual : UserControl {
        public Manual() {
            // Intialize the component
            InitializeComponent();
            // Create a new manual.
            this.DataContext = new ManualViewModel();
        }
    }
}
