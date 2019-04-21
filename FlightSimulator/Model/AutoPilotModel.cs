using FlightSimulator.Server;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models {
    // The model for the auto pilot tab.
    class AutoPilotModel {
        // Sending the string command from the text box to the server.
        public void SendCommands(string commands) {
            // If the server is connected.
            if (CommandServer.Instance.ConnectionStatus) {
                new Thread(delegate () {
                    // Send the command.
                    CommandServer.Instance.SendCommands(commands);
                }).Start();
            }
        }
    }
}

