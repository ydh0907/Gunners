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
        public Socket player1;
        public Socket player2;
        public Room(Socket player1, Socket player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }
    }
}
