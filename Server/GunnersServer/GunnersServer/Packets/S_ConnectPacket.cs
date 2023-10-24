using Do.Net;
using System;

namespace GunnersServer.Packets
{
    public class S_ConnectPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.S_ConnectPacket;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            throw new NotImplementedException();
        }

        public override ArraySegment<byte> Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
