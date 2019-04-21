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
using FlightSimulator.ViewModel.Windows;

namespace FlightSimulator.Views.Windows {
    // The view code of the settings xaml.
    public partial class Settings : Window {
        public Settings() {
            // Intitinlaize the component from the xaml
            InitializeComponent();
            // Create a new view model
            DataContext = new SettingsWindowViewModel();
        }
        // Close the settings window on click. 
        private void cancel_Click(object sender, RoutedEventArgs e) {
            // Close THIS window.
            this.Close();
        }
    }
}
