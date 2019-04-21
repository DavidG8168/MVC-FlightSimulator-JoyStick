using FlightSimulator.Server;
using FlightSimulator.Models;
using FlightSimulator.Views.Windows;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace FlightSimulator.ViewModel {
    // The class handles the flight board view model.
    public class FlightBoardViewModel : BaseNotify {
        // Creating the settings window.
        private Settings settingMenu = new Settings();
        // Creating the model.
        private FlightBoardModel model;
        // The event notifier.
        public new event PropertyChangedEventHandler PropertyChanged;
        // The settings window.
        private ICommand settingCommand;
        // The connect command using the Connect button.
        private ICommand connectCommand;
        // The thread we run the connection on.
        private Thread newthread;
        // The constructor.
        public FlightBoardViewModel() {
            // Create the model and connect it to the information server.
            model = new FlightBoardModel(new InformationServer());
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                // Update the lon.
                if (e.PropertyName == "Lon") {
                    Lon = model.Lon;
                }
                // Update the lat.
                else if (e.PropertyName == "Lat") {
                    Lat = model.Lat;
                }
                // Notify everyone of changes.
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
            };
        }
        // The lon property.
        public double Lon { get; set; }
        // The lat property.
        public double Lat { get; set; }
        // Get the settings command if it exists otherwise create it by opening a window.
        public ICommand SettingsCommand {
            get {
                // If the command exist return it.
                if (settingCommand != null) {
                    return settingCommand;
                }
                // Otherwise create it.
                return settingCommand = new CommandHandler(() => OpenSettingWindow());
            }
        }
        // Open the settings window.
        void OpenSettingWindow() {
            // Prevent multiple windows from being open.
            if (!settingMenu.IsLoaded) {
                settingMenu = new Settings();
                settingMenu.Show();
            }
            else settingMenu.Show();
        }
        // Create the connect command if it doesn't exist.
        public ICommand ConnectsCommand { get { 
                // If the command exists return it.
                if (connectCommand != null) {
                    return connectCommand;
                }
                // Otherwise create it.
                return connectCommand = new CommandHandler(() => UpdateServer());
            }
        }
        void UpdateServer() {
            // If there is a connection, reset it and reconnect.
            if (model.IsConnected()) {
                // Stop the connection.
                model.StopRead();
                // Reset it.
                CommandServer.Instance.Reset();
                // Let the thread finish.
                System.Threading.Thread.Sleep(1000);
            }
            // Create a new thread, and run the new connection on it.
            newthread = new Thread(() => CommandServer.Instance.Connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort));
            newthread.Start();
            model.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
        }
    }
}
