using CommonLibrary.Entities.Angle;
using CommonLibrary.Entities.Arm;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace brazoRobot.Business
{
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;

        // Size of receive buffer.
        public const int BufferSize = 2048;

        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class ConnectionSocket
    {
        private const int PORT = 11000;
        private static Mutex mut = new Mutex();

        private static readonly Socket ClientSocket = new Socket
           (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Arm ActualArm = null;
        public Controls controlObject = null;
        private static Thread reciveThread;

        public ConnectionSocket()
        {
            controlObject = new Controls();
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;

                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }

            reciveThread = new Thread(new ThreadStart(Run));

            reciveThread.Start();
        }

        private void Run()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(10);
                    ReceiveResponse();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        public void SendRequest()
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(controlObject) + "_";
            SendString(jsonString);
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), ClientSocket);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveResponse()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = ClientSocket;

                // Begin receiving the data from the remote device.
                ClientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    string text = state.sb.ToString();
                    ActualArm = JsonConvert.DeserializeObject<Arm>(text);
                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.
                    if (state.sb.Length > 1)
                    {
                        string text = state.sb.ToString();
                        ActualArm = JsonConvert.DeserializeObject<Arm>(text);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}