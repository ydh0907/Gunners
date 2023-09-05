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
    internal class Room
    {
        public Socket host;
        public Socket client;
        public int num;
        public Room(Socket host, int num)
        {
            this.host = host;
            this.num = num;
        }
        public void StartGame(Socket client)
        {
            this.client = client;
        }
    }
    internal class Player
    {
        public Socket player;
        public int num;
        public Player(Socket player, int num)
        {
            this.player = player;
            this.num = num;
        }
    }
}
