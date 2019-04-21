using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Server {
    // The class handles receiving information from the simulator.
    class InformationServer {
        // The TCP listener.
        private TcpListener tcpListener;
        // The TCP client.
        private TcpClient tcpClient;
        // The binary reader.
        private BinaryReader reader;
        // Property to check if there is a connection going on currently.
        public bool ConnectionExists { get; set; } = false;
        // Property to halt the connection when needed.
        public bool HaltConnection { get; set; } = false;
        // Halt the connection and close the listener and client.
        public void StopConnection() {
            // Handle null error.
            if (tcpClient != null) {
                tcpClient.Close();
            }
            tcpListener.Stop();
            ConnectionExists = false;
        }
        // Start the server connection with the given IP and PORT.
        public void StartConnection(string serverIP, int serverPort) {
            if (tcpListener != null) {
                StopConnection();
            }
            tcpListener = new TcpListener(IPAddress.Parse(serverIP), serverPort);
            tcpListener.Start();
        }
        // Read from the server and send it to the model for processing.
        public string[] ReadFromServer() {
            if (!ConnectionExists) {
                ConnectionExists = true;
                // Try to connect, throw exception if failed.
                try {
                    tcpClient = tcpListener.AcceptTcpClient();
                }
                catch (Exception e) {
                     throw new System.ArgumentException("Connection Error !", "tcpClient");
                }
                reader = new BinaryReader(tcpClient.GetStream());
            }
            // Will hold the information from the server.
            string serverOutput = "";
            // Used to read from the server.
            char i;
            // Read until we reach the end of the line and store inside the string.
            while ((i = reader.ReadChar()) != '\n') {
                serverOutput += i;
            }
            // Split the string.
            string[] splitStr = serverOutput.Split(',');
            // Return the values at index 0 and 1, being the lon an lat respectively.
            string[] lonAndLat = { splitStr[0], splitStr[1] };
            return lonAndLat;
        }
    }
}
