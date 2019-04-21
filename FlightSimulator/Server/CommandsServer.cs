using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Server {
    // The class handles sending commands to the simulator.
    class CommandsServer {
        // The TCP client.
        private TcpClient tcpClient;
        // The binary writer.
        private BinaryWriter bWriter;
        // Instance of the class.
        private static CommandsServer m_Instance = null;
        // Used to check if there is an active connection to the server.
        public bool ConnectionExists { get; set; } = false;
        // Allow only a single instance to exists.
        public static CommandsServer Instance {
            get {
                if (m_Instance == null) {
                    m_Instance = new CommandsServer();
                }
                return m_Instance;
            }
        }
        // Connect to the simulator.
        public void ConnectToSimulator(string serverIP, int serverPort) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            tcpClient = new TcpClient();
            // Try to connect until succesful.
            while (!tcpClient.Connected) {
                try { tcpClient.Connect(ep); }
                catch (Exception) { }
            }
            // Set the connection to true once we connect and initialize the writer.
            ConnectionExists = true;
            bWriter = new BinaryWriter(tcpClient.GetStream());
        }
        // Send commands to the simulator.
        public void SendCommandsToSimulator(string inputCommands) {
            // Do nothing if there are no commands.
            if (string.IsNullOrEmpty(inputCommands)) {
                return;
            }
            // Get the commands from the input and put them in the buffer.
            string[] commandsToSend = inputCommands.Split('\n');
            // Send the commands.
            foreach (string command in commandsToSend) {
                string tmp = command + "\r\n";
                bWriter.Write(System.Text.Encoding.ASCII.GetBytes(tmp));
                System.Threading.Thread.Sleep(2000);
            }
        }
        // When the connection stops we reset the instance.
        public void ResetConnection() {
            m_Instance = null;
        }
    }
}
