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
            jsonString = JsonConvert.SerializeObject(controlObject);
            SendString(jsonString);
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private void SendString(string text)
        {
            mut.WaitOne();
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            mut.ReleaseMutex();
        }

        private void ReceiveResponse()
        {
            try
            {
                var buffer = new byte[2048];
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.ASCII.GetString(data);
                ActualArm = JsonConvert.DeserializeObject<Arm>(text);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}