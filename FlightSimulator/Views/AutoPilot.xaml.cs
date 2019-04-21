using System.Windows.Controls;
using FlightSimulator.ViewModels;

// the view code of the auto pilot xaml.
namespace FlightSimulator.Views {
    public partial class AutoPilot : UserControl { 
        // Keep the view model as a member.
        AutoPilotViewModel viewModel;
        public AutoPilot() {
            // Intialize the component
            InitializeComponent();
            // Initialize the view model.
            viewModel = new AutoPilotViewModel();
            DataContext = viewModel;
        }
    }
}
