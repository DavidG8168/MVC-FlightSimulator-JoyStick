using FlightSimulator.ViewModel;
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

namespace FlightSimulator.Views {
    // The view code of the auto pilot xaml.
    public partial class AutoPilot : UserControl {
        // Keep the view model as a member of the view.
        AutoPilotViewModel viewModel;
        // The constructor.
        public AutoPilot() {
            // Intialize the component
            InitializeComponent();
            // Initialize the view model.
            viewModel = new AutoPilotViewModel();
            DataContext = viewModel;
        }
    }
}
