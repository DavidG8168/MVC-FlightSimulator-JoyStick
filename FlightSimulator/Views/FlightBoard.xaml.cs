using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views {
    // Class provided partly by instructor.
    // The code for the flight board xaml.
    public partial class FlightBoard : UserControl {
        FlightBoardViewModel viewModel;
        // Will hold the list of points to draw on the graph, initialize as null.
        ObservableDataSource<Point> coordinates = null;
        // The constructor.
        public FlightBoard() {
            InitializeComponent();
            DataContext = viewModel = new FlightBoardViewModel();
            viewModel.PropertyChanged += Vm_PropertyChanged;
        }
        // Connect between a point of the graph to a point in the point collection.
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            coordinates = new ObservableDataSource<Point>();
            coordinates.SetXYMapping(p => p);
            plotter.AddLineGraph(coordinates, Colors.DeepSkyBlue, 2, "Route");
        }
        // If the changed property is lon or lat add the new point after travel to the collection.
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if ( e.PropertyName.Equals("Lon") || e.PropertyName.Equals("Lat")) {
                coordinates.AppendAsync(Dispatcher, new Point(viewModel.Lat, viewModel.Lon));
            }
        }
    }
}

