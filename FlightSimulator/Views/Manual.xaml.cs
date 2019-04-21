using System.Windows.Controls;
using FlightSimulator.ViewModels.Windows;

// The view code of the the manul xaml.
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
