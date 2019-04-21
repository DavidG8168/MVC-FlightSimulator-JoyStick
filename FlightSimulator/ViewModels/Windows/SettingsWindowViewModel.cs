using FlightSimulator.Models;
using FlightSimulator.Models.Interface;

namespace FlightSimulator.ViewModels.Windows {
    // Class provided by instructor.
    // The class is the view model for the settings window.
    public class SettingsWindowViewModel : BaseNotify {
        // Keep a member of the interface type.
        private ISettingsModel model;
        // The constructor.
        public SettingsWindowViewModel() {
            this.model = new ApplicationSettingsModel();
        }
        // Get and set the server IP for the simulator.
        public string FlightServerIP {
            get { return model.FlightServerIP; }
            set {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }
        public int FlightCommandPort {
            // Get and set the command port.
            get { return model.FlightCommandPort; }
            set {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }
        // Get and set the information port.
        public int FlightInfoPort{
            get { return model.FlightInfoPort; }
            set {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }
        // Save the settings in the model.
        public void SaveSettings(){
            model.SaveSettings();
        }
        // The settings in the model for the settings window are set.
        public void ReloadSettings(){
            model.ReloadSettings();
            FlightServerIP = model.FlightServerIP;
            FlightInfoPort = model.FlightInfoPort;
            FlightCommandPort = model.FlightCommandPort;
        }
    }
}

