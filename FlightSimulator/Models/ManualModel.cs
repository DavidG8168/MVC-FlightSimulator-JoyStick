using System.Threading;
using FlightSimulator.Server;

namespace FlightSimulator.Models {
    // The model for the manual tab.
    class ManualModel {
        // Send the commands to the server.
        public void SendCommandToSimulator(string flightCommands) {
            // If the connection has been established we can send the commands to the simulator.
            if (CommandsServer.Instance.ConnectionExists) {
                new Thread(delegate () {
                    // Send the commands to the server.
                    CommandsServer.Instance.SendCommandsToSimulator(flightCommands);
                }).Start();
            }
        }
    }
}
