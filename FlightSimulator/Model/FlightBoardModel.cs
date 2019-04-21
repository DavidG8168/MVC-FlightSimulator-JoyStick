using FlightSimulator.Server;
using FlightSimulator.ViewModel;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models {
    // The model for the flight board.
    class FlightBoardModel : BaseNotify {
        // The information server for updating our lon and lat values.
        private InformationServer info;
        // The event notifier.
        public event PropertyChangedEventHandler PropertyChanged;
        // The constructor sets the information server.
        public FlightBoardModel(InformationServer info) {
            this.info = info;
        }
        // The lon member.
        private double lon;
        // The lon property.
        public double Lon {
            // Return the lon.
            get { return lon; }
            // Set the lon and notify the view model of change.
            set {
                lon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lon"));
            }
        }
        // The lat member.
        private double lat;
        // The lat property.
        public double Lat {
            // Return the lat.
            get { return lat; }
            // Set the lat and notify the view model of change.
            set {
                lat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lat"));
            }
        }
        // Open the information server with the given IP and PORT.
        public void Open(string ip, int port) {
            // Open the server.
            info.Open(ip, port);
            // Start reading.
            StartRead();
        }
        // Read from the server and notify the view model of the changing lon and lat values.
        void StartRead() {
            // Read in a new thread.
            new Task(delegate () {
                while (!info.Stop) {
                    // Get string of information from the server.
                    string[] args = info.Read();
                    // Convert the values to doubles from string.
                    Lon = Convert.ToDouble(args[0]);
                    Lat = Convert.ToDouble(args[1]);
                }
            }).Start();
        }
        // True if server is connected.
        public bool IsConnected() { return info.ConnectionStatus; }
        // Stop the server connection.
        public void StopRead() { info.Stop = true; }
    }
}
