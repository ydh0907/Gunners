using System;
using System.Net;
using Do.Net;

namespace GunnersServer
{
    public class ClientSession : Session
    {
        public EndPoint endPoint = null;

        public ushort userID = ushort.MaxValue;
        public ushort roomID = ushort.MaxValue;
        public string nickname = null;

        public float x;
        public float y;
        public float z;

        public ushort agent;
        public ushort weaponID;

        public void Reset()
        {
            x = 0; y = 0; z = 0;
            agent = 0; weaponID = 0;
            roomID = ushort.MaxValue;
        }

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
            PacketManager.Instance.CreatePacket(buffer);
        }

        public override void OnSent(int length)
        {
            Console.WriteLine($"[Session] {length} of byte Sent");
        }
    }
}
