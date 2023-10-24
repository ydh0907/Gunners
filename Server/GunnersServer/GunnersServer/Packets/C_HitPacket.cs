using Do.Net;
using System;

namespace GunnersServer.Packets
{
    public class C_HitPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_HitPacket;

        public ushort userID;
        public ushort damage;
        public float x, y;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadUShortData(buffer, process, out userID);
            process += PacketUtility.ReadUShortData(buffer, process, out damage);
            process += PacketUtility.ReadFloatData(buffer, process, out x);
            process += PacketUtility.ReadFloatData(buffer, process, out y);
        }

        public override ArraySegment<byte> Serialize()
        {
            ushort process = 0;
            ArraySegment<byte> buffer = UniqueBuffer.Open(256);

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(ID, buffer, process);
            process += PacketUtility.AppendUShortData(userID, buffer, process);
            process += PacketUtility.AppendUShortData(damage, buffer, process);
            process += PacketUtility.AppendFloatData(x, buffer, process);
            process += PacketUtility.AppendFloatData(y, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return buffer;
        }
    }
}