using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using FlightSimulator.Models;
using FlightSimulator.ViewModel;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views {
    // The view of the flight board xaml.
    public partial class FlightBoard : UserControl {
        // Keep the view model as a member.
        FlightBoardViewModel viewModel;
        // This data source will keep the points we will draw on the board.
        ObservableDataSource<Point> coordinates = null;
        // The constructor.
        public FlightBoard() {
            InitializeComponent();
            DataContext = viewModel = new FlightBoardViewModel();
            viewModel.PropertyChanged += Vm_PropertyChanged;
        }
        // Add the lines to thte graph.
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // Map between a point on the board and a point inside the data source.
            coordinates = new ObservableDataSource<Point>();
            coordinates.SetXYMapping(p => p);
            // Add the line to the graph.
            plotter.AddLineGraph(coordinates, Colors.SkyBlue, 2, "Route");
        }
        // Add point to the graph.
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            // If the changed property is lat or lon we will add the point to the data source.
            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon") ) {
                coordinates.AppendAsync(Dispatcher, new Point(viewModel.Lat, viewModel.Lon));
            }
        }
    }
}

