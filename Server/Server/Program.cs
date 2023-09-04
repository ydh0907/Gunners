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
        static Queue<Socket> players = new();
        static Queue<Room> rooms = new();
        static void Main(string[] args)
        {
            Socket listener = CreateListenScoket();
            Socket socket = listener.Accept();
        }
        private static Socket CreateListenScoket()
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
    }
}