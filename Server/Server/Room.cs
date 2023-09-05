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
        public int num;
        public Room(Socket host, int num)
        {
            this.host = host;
            this.num = num;
        }
    }
}
