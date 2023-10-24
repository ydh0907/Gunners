using Do.Net;
using System;

namespace GunnersServer.Packets
{
    public class S_GameEndPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.S_GameEndPacket;

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
