using FlightSimulator.Models;
using FlightSimulator.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModel.Windows {
    // The view model code for the settings window.
    public class SettingsWindowViewModel : BaseNotify {
        // Keep a settings model of the given interface.
        private ISettingsModel model;
        // The constructor.
        public SettingsWindowViewModel() {
            // Initialize the member.
            this.model = new ApplicationSettingsModel();
        }
        // Information Server Port property.
        public int FlightInfoPort {
            // Return the port.
            get { return model.FlightInfoPort; }
            set {
                // Set the information port.
                model.FlightInfoPort = value;
                // Notify of change.
                NotifyPropertyChanged("FlightInfoPort");
            }
        }
        // Commands Server Port property.
        public int FlightCommandPort {
            // Return the port.
            get { return model.FlightCommandPort; }
            set {
                // Set the commands port.
                model.FlightCommandPort = value;
                // Notify of change.
                NotifyPropertyChanged("FlightCommandPort");
            }
        }
        // Server IP property.
        public string FlightServerIP {
            get { return model.FlightServerIP; }
            set {
                // Set the server IP.
                model.FlightServerIP = value;
                // Notify of change.
                NotifyPropertyChanged("FlightServerIP");
            }
        }
        // Save the settings.
        public void SaveSettings(){
            model.SaveSettings();
        }
        // Update the settings from the model.
        public void ReloadSettings(){
            model.ReloadSettings();
            FlightServerIP = model.FlightServerIP;
            FlightCommandPort = model.FlightCommandPort;
            FlightInfoPort = model.FlightInfoPort;
        }
    }
}

