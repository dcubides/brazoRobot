using CommonLibrary.Entities.Angle;
using CommonLibrary.Entities.Arm;
using Newtonsoft.Json;
using Server.Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        private static List<Socket> Clients = new List<Socket>();
        private static Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 11000;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        private static int CentroX = 265;
        private static int CentroY = 378;
        private static OperationArm actual = null;

        public static OperationArm Orchestrator
        {
            get
            {
                if (actual == null)
                {
                    actual = new OperationArm(new Point(CentroX, CentroY));
                }
                return actual;
            }
        }

        private static void Main()
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            Server.Bind(new IPEndPoint(IPAddress.Any, PORT));
            Server.Listen(0);
            Server.BeginAccept(AcceptCallback, null);
            Orchestrator.ManipulateArm();
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in Clients)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            Server.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = Server.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            Clients.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            Server.BeginAccept(AcceptCallback, null);
            string jsonString;
            jsonString = JsonConvert.SerializeObject(Orchestrator.ActualArm);

            byte[] data = Encoding.ASCII.GetBytes(jsonString);
            socket.Send(data);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                Clients.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            string[] RecivedObj = text.Split('_');
            for (int i = 0; i < RecivedObj.Length - 1; i++)
            {
                Controls _Controls = JsonConvert.DeserializeObject<Controls>(RecivedObj[i]);

                Orchestrator.Angle = _Controls.Angle != 0 ? _Controls.Angle : Orchestrator.Angle;
                Orchestrator.Angle2 = _Controls.Angle2 != 0 ? _Controls.Angle2 : Orchestrator.Angle2;
                Orchestrator.Angle3 = _Controls.Angle3 != 0 ? _Controls.Angle3 : Orchestrator.Angle3;
                Orchestrator.Angle4 = _Controls.Angle4 != 0 ? _Controls.Angle4 : Orchestrator.Angle4;
                Orchestrator.Angle5 = _Controls.Angle5 != 0 ? _Controls.Angle5 : Orchestrator.Angle5;

                Orchestrator.ManipulateArm();
            }

            string jsonString;
            jsonString = JsonConvert.SerializeObject(Orchestrator.ActualArm);

            byte[] data = Encoding.ASCII.GetBytes(jsonString);
            foreach (var client in Clients)
            {
                int x = client.Send(data);
                Console.WriteLine("Send Alter " + jsonString);
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}