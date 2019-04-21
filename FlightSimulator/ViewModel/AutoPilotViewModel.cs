using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulator.ViewModel {
    // The AutoPilotViewModel handles the auto pilot tab.
    class AutoPilotViewModel : INotifyPropertyChanged {
        // The event notifier.
        public event PropertyChangedEventHandler PropertyChanged;
        // Create a new model.
        private AutoPilotModel model = new AutoPilotModel();
        // Create the brush and set it as white for the default color.
        private Brush background = Brushes.White; // Background color
        // This will be the string of commands we write in the window.
        private string commands;
        // This will be the clear button command.
        private ICommand clearCommand;
        // This will be the ok button command.
        private ICommand okCommand;
        // The backgournd color property.
        public Brush Background {
            get {
                return background;
            }
            set {
                background = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Background"));
            }
        }
        // The commands string property.
        public string Commands {
            get { return commands; }
            set {
                commands = value;
                //Check if the string is not null we draw a pink window 
                if (!string.IsNullOrEmpty(Commands)) {
                    if (Background == Brushes.White) {
                        Background = Brushes.LightPink;
                    }
                }
                //else the window is going to be white
                else {
                    if (string.IsNullOrEmpty(Commands)) {
                        Background = Brushes.White;
                    }
                }
            }
        }
        // The CLEAR button property.
        public ICommand ClearCommand {
            get {
                // If the command exists, return it.
                if (clearCommand != null) {
                    return clearCommand;
                }
                // Otherwise create a new one and return it.
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
        // The OK button property.
        public ICommand OkCommand {
            get {
                // If the command is null create it.
                if ( okCommand == null) {
                    return okCommand = new CommandHandler(() => {
                        // The string we will send to the simulator.
                        string sentCommands = Commands;
                        // Clear the commands.
                        Commands = "";
                        // Notify the view.
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Commands));
                        // Reset the background to white.
                        Background = Brushes.White;
                        // Send the commands to the simulator.
                        model.SendCommands(sentCommands);
                    });
                }
                // Otherwise return the existing one.
                return okCommand;
            }
        }
    }
}
