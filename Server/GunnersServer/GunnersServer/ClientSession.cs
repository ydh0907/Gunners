using System;
using System.Net;
using Do.Net;

namespace GunnersServer
{
    public class ClientSession : Session
    {
        public EndPoint endPoint = null;

        public ushort userID;
        public string nickname = "";

        public float x;
        public float y;
        public float z;

        public ushort weaponID;
        public ushort agent;

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"[Session] {endPoint} is Connected");
            this.endPoint = endPoint;
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"[Session] {endPoint} is Disconnected");
        }

        public override void OnPacketReceived(ArraySegment<byte> buffer)
        {
            Console.WriteLine($"[Session] {buffer.Count} of byte Received");
            _
        }

        public override void OnSent(int length)
        {
            Console.WriteLine($"[Session] {length} of byte Sent");
        }
    }
}
