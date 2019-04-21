using FlightSimulator.Server;
using FlightSimulator.Models;
using FlightSimulator.Views.Windows;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace FlightSimulator.ViewModels {
    // The class representingt the flightboard view model.
    public class FlightBoardViewModel : BaseNotify {
        // Creating the model.
        private FlightBoardModel model;
        // Creating the settings window and initalizing it.
        private Settings settingWindow = new Settings();
        // The command that connects to the simulator.
        private ICommand connectToSimulatorCommand;
        // The thread we will run the connection on.
        private Thread newthread;
        // The command that opens the settings window.
        private ICommand openSettingsWindowCommand;
        // The event notifier.
        public new event PropertyChangedEventHandler PropertyChanged;
        // The lon property.
        public double Lon { get; set; }
        // The lat property.
        public double Lat { get; set; }
        // The constructor.
        public FlightBoardViewModel() {
            // Create the model and connect it to the information server.
            model = new FlightBoardModel(new InformationServer());
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                // Update the lat.
                if (e.PropertyName == "Lat") {
                    Lat = model.Lat;
                }
                // Update the lon.
                else if (e.PropertyName == "Lon") {
                    Lon = model.Lon;
                }
                // Notify everyone of changes.
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
            };
        }
        // Create the connect command if it doesn't exist.
        public ICommand ConnectsCommand { get {
                // If the command isn't null return it.
                if (connectToSimulatorCommand != null) {
                    return connectToSimulatorCommand;
                }
                // Otherwise create it and return it.
                else {
                    return connectToSimulatorCommand = new CommandHandler(() => ConnectToSimulator());
                }
            }
        }
        // Connect to the simulator using a new thread.
        void ConnectToSimulator() {
            // If there is a connection, reset it and reconnect.
            if (model.ConnectionExists()) {
                // Stop the connection.
                model.HaltConnection();
                // Reset it.
                CommandsServer.Instance.ResetConnection();
                // Let the thread finish.
                System.Threading.Thread.Sleep(1000);
            }
            // Create a new thread, and run the new connection on it.
            newthread = new Thread(() => CommandsServer.Instance.ConnectToSimulator(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort));
            newthread.Start();
            model.StartConnection(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
        }
        // Get the window if it exists otherwise create it.
        public ICommand SettingsCommand { get {
                // If the command isn't null return it.
                if (openSettingsWindowCommand != null) {
                    return openSettingsWindowCommand;
                }
                // Otherwise create it and return it.
                else {
                    return openSettingsWindowCommand = new CommandHandler(() => OpenSettingWindow());
                }
            }
        }
        // Open the setting window.
        void OpenSettingWindow() {
            // Show it if it's loaded.
            if (settingWindow.IsLoaded) {
                settingWindow.Show();
            }
            // Otherwise create a new one and show it.
            else if (!settingWindow.IsLoaded) {
                settingWindow = new Settings();
                settingWindow.Show();
            }
        }    
    }
}
