using FlightSimulator.Server;
using FlightSimulator.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FlightSimulator.Models {
    // The model for the flight board.
    class FlightBoardModel : BaseNotify {
        // The information server for updating our lon and lat values.
        private InformationServer informationServer;
        // The lat member.
        private double lat;
        // The lon member.
        private double lon;
        // The event notifier.
        public new event PropertyChangedEventHandler PropertyChanged;
        // The constructor initializes the information server.
        public FlightBoardModel(InformationServer infoServer) {
            this.informationServer = infoServer;
        }
        // The lat property.
        public double Lat {
            // Return the lat.
            get {
                return lat;
            }
            // Set the lat and notify the view model of change.
            set {
                lat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lat"));
            }
        }
        // The lon property.
        public double Lon {
            // Return the lon.
            get {
                return lon;
            }
            // Set the lon and notify the view model of change.
            set {
                lon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lon"));
            }
        }
        // Returns the connection status.
        public bool ConnectionExists() {
            return informationServer.ConnectionExists;
        }
        // Stop reading from the server by setting the connection status to false.
        public void HaltConnection() {
            informationServer.HaltConnection = true;
        }
        // Open the information server with the given IP and PORT.
        public void StartConnection(string serverIP, int serverPort) {
            // Start the connection to the server.
            informationServer.StartConnection(serverIP, serverPort);
            // Start reading information from the server.
            StartRead();
        }
        // Read from the server and notify the view model of the changing lon and lat values.
        void StartRead() {
            // Read in a new thread.
            new Task(delegate () {
                // While the connection is still up.
                while (!informationServer.HaltConnection) {
                    // Receive a string of information from the server.
                    string[] serverOutput = informationServer.ReadFromServer();
                    // Convert the values to doubles from string.
                    // The lon and lat are stored in the first two indexes.
                    Lon = Convert.ToDouble(serverOutput[0]);
                    Lat = Convert.ToDouble(serverOutput[1]);
                }
            }).Start();
        }
    }
}
