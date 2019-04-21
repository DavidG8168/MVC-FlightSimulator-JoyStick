using System.Windows;

namespace FlightSimulator {
    // The main window xaml.
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
// Handle some error when starting the program.
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
        }
    }
}
