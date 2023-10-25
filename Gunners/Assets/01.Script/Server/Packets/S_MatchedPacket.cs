﻿using Do.Net;
using System;

namespace GunnersServer.Packets
{
    public class S_MatchedPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.S_MatchedPacket;

        public ushort roomID;

        public bool host;

        public string name;
        public ushort agent;
        public ushort weaponID;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            int process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadUShortData(buffer, process, out roomID);
            host = BitConverter.ToBoolean(buffer.Array, buffer.Offset + process);
            process += sizeof(bool);
            process += PacketUtility.ReadStringData(buffer, process, out name);
            process += PacketUtility.ReadUShortData(buffer, process, out agent);
            process += PacketUtility.ReadUShortData(buffer, process, out weaponID);
        }

        public override ArraySegment<byte> Serialize()
        {
            ushort process = 0;
            ArraySegment<byte> buffer = UniqueBuffer.Open(32);

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(ID, buffer, process);
            Buffer.BlockCopy(BitConverter.GetBytes(host), 0, buffer.Array, buffer.Offset + process, sizeof(bool));
            process += sizeof(bool);
            process += PacketUtility.AppendStringData(name, buffer, process);
            process += PacketUtility.AppendUShortData(agent, buffer, process);
            process += PacketUtility.AppendUShortData(weaponID, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}