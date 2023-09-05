using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static Queue<Socket> sockets = new();
        static Queue<Play> players = new();
        static Dictionary<int, Socket> rooms = new();

        static bool isListen = true;
        static object lockObj = new();
        static void Main(string[] args)
        {
            Socket listener = CreateListenSocket();

            Thread CreateClinetThread = new(new ParameterizedThreadStart(CreateClientSocket));
            CreateClinetThread.IsBackground = true;
            CreateClinetThread.Start(listener);

            Thread CreateRoomThread = new(new ThreadStart(CreateClientPlayer));
            CreateRoomThread.IsBackground = true;
            CreateRoomThread.Start();

            while (isListen)
            {
                
            }
        }
        private static Socket CreateListenSocket()
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddress = ipHost.AddressList[1];
            IPEndPoint endPoint = new IPEndPoint(ipAddress, 9070);

            Socket socket = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);

            Console.WriteLine($"Server opened on IP : {endPoint.Address}");
            Console.WriteLine($"Server opened on port : {endPoint.Port}");

            return socket;
        }
        private static void CreateClientSocket(object obj)
        {
            Socket listener = obj as Socket;
            bool isNotError = true;
            while(isNotError)
            {
                Socket socket;
                try
                {
                    socket = listener.Accept();
                }
                catch(Exception err)
                {
                    Console.WriteLine("Error On CreateClientSocket : " + err.Message);
                    continue;
                }
                lock (lockObj) sockets.Enqueue(socket);
            }
        }
        private static void CreateClientPlayer()
        {
            while (true)
            {
                Socket socket = sockets.Dequeue();
                try
                {
                    if(sockets.Count > 0)
                    {
                        byte[] buffer = new byte[1024];
                        socket.Receive(buffer);

                    }
                }
                catch(Exception err)
                {
                    Console.WriteLine("Error On CreateClientPlayer : " + err.Message);
                    DisConnectClient(socket);
                    continue;
                }
            }
        }
        private static void DisConnectClient(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}