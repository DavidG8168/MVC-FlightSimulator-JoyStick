using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Input;

namespace FlightSimulator.ViewModels {
    // The class represents the auto pilot view model.
    class AutoPilotViewModel : INotifyPropertyChanged {
        // Create a new model and initialize it.
        private AutoPilotModel model = new AutoPilotModel();
        // This will be the string of commands we write in the window.
        private string commands;
        // Create the brush and set it as white for the default color.
        private Brush color = Brushes.White;
        // The command for pressing the CLEAR button.
        private ICommand clearCommand;
        // The command for pressing the OK button.
        private ICommand okCommand;
        // The event notifier.
        public event PropertyChangedEventHandler PropertyChanged;
        // The commands property for the string input.
        public string Commands {
            get { return commands; }
            set {
                commands = value;
                // Check if the string is not null we draw a pink window 
                if (!string.IsNullOrEmpty(Commands)) {
                    if (Background == Brushes.White) {
                        Background = Brushes.LightPink;
                    }
                }
                // Else the window is going to be white
                else {
                    if (string.IsNullOrEmpty(Commands)) {
                        Background = Brushes.White;
                    }
                }
            }
        }
        // The backgournd property for the background color of the box.
        public Brush Background {
            get {
                return color;
            }
            set {
                // Change the color and notify of change.
                color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Background"));
            }
        }
        // The CLEAR button command.
        public ICommand ClearCommand {
            get {
                // Return the command if it exists.
                if (clearCommand != null) {
                    return clearCommand;
                }
                // Otherwise create it.
                else {
                    return clearCommand = new CommandHandler(() => {
                        // Reset the commands.
                        Commands = "";
                        // Color the background white.
                        Background = Brushes.White;
                        // Notify the view.
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Commands));
                    });
                }
            }
        }
        // The OK button command.
        public ICommand OkCommand {
            get {
                // Return the command if it exists.
                if (okCommand != null) {
                    return okCommand;
                }
                // Otherwise create it.
                else {
                    return okCommand = new CommandHandler(() => {
                        // The string we will send to the simulator.
                        string toBeSent = Commands;
                        // Clear the commands.
                        Commands = "";
                        // Notify the view.
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Commands));
                        // Reset the background to white.
                        Background = Brushes.White;
                        // Send the commands to the simulator.
                        model.SendCommandsToSimulator(toBeSent);
                    });
                }
            }
        }
    }
}
