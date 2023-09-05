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
    internal enum PacketState
    {
        MakeRoom,
        EnterRoom,
        Move,
        Fire,
        ChangeGun,
        Continue
    }

    internal class Packet
    {
        public int num;
        public PacketState state;
        public Packet(PacketState state)
        {
            num = 0;
            this.state = state;
        }
        public Packet(PacketState state , int num)
        {
            this.state = state;
            this.num = num;
        }
    }
}
