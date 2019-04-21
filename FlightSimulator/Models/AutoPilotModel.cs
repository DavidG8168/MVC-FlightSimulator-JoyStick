using FlightSimulator.Server;
using System.Threading;

namespace FlightSimulator.Models {
    // The model for the auto pilot tab.
    class AutoPilotModel {
        // Sending the string command from the text box to the server.
        public void SendCommandsToSimulator(string commands) {
            // If a connection has been established we can send commands to the simulator.
            if (CommandsServer.Instance.ConnectionExists) {
                new Thread(delegate () {
                    // Send the command.
                    CommandsServer.Instance.SendCommandsToSimulator(commands);
                }).Start();
            }
        }
    }
}

