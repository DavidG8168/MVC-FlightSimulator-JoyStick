using System.Windows;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator.Views.Windows {
   // The xaml code for the settings window.
    public partial class Settings : Window {
        public Settings() {
            InitializeComponent();
            // Initialize the view model.
            DataContext = new SettingsWindowViewModel();
        }
        // Close the settings window when cancel is clicked.
        private void cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
