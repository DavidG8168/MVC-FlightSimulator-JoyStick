using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Server;

namespace FlightSimulator.Models {
    // The model for the manual tab.
    class ManualModel {
        // Send the commands to the server.
        public void SendCommand(string commands) {
            // If the connection is made.
            if (CommandServer.Instance.ConnectionStatus) {
                new Thread(delegate () {
                    // Send the commands.
                    CommandServer.Instance.SendCommands(commands);
                }).Start();
            }
        }
    }
}
