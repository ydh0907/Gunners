﻿using Do.Net;
using System;
using System.Numerics;

namespace GunnersServer.Packets
{
    public class C_ReadyPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_ReadyPacket;

        public ushort userID;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadUShortData(buffer, process, out userID);
        }

        public override ArraySegment<byte> Serialize()
        {
            ushort process = 0;
            ArraySegment<byte> buffer = UniqueBuffer.Open(256);

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(ID, buffer, process);
            process += PacketUtility.AppendUShortData(userID, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return buffer;
        }
    }
}