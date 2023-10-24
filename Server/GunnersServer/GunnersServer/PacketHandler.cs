using System;
using System.Numerics;
using Do.Net;
using GunnersServer.Packets;

namespace GunnersServer
{
    public class PacketHandler
    {
        public static void C_ConnectPacket(Session session, Packet packet){
            ClientSession _session = session as ClientSession;
            C_ConnectPacket _packet = packet as C_ConnectPacket;

            _session.nickname = _packet.nickname;
            _session.userID = Program.NextUserID;

            S_ConnectPacket s_ConnectPacket = new();

            s_ConnectPacket.userID = _session.userID;

            _session.Send(s_ConnectPacket.Serialize());
        }
        public static void C_DisconnectPacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_DisconnectPacket _packet = packet as C_DisconnectPacket;

            Program.users.Remove(_session.userID);

            if(_session.roomID != ushort.MaxValue && Program.rooms.ContainsKey(_session.roomID))
            {
                S_GameEndPacket s_GameEndPacket = new();

                Program.rooms[_session.roomID].AddJob
                    (() => Program.rooms[_session.roomID].Broadcast(s_GameEndPacket, _session.userID));

                Program.rooms[_session.roomID].DestroyRoom();
            }
        }
        public static void C_MatchingPacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_MatchingPacket _packet = packet as C_MatchingPacket;

            _session.agent = _packet.agent;
            _session.weaponID = _packet.weaponID;

            Program.AddJob
                (() => Program.EnterRoom(_session));
        }
        public static void C_ReadyPacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_ReadyPacket _packet = packet as C_ReadyPacket;

            if (Program.rooms[_session.roomID].ready)
            {
                S_GameStartPacket s_GameStartPacket = new();

                Program.rooms[_session.roomID].AddJob
                    (() => Program.rooms[_session.roomID].BroadcastAll(_packet));
            }
            else Program.rooms[_session.roomID].ready = true;
        }
        public static void C_MovePacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_MovePacket _packet = packet as C_MovePacket;

            S_MovePacket s_MovePacket = new();

            s_MovePacket.x = _packet.x;
            s_MovePacket.y = _packet.y;
            s_MovePacket.z = _packet.z;

            Program.rooms[_session.roomID].AddJob
                (() => Program.rooms[_session.roomID].Broadcast(s_MovePacket, _session.userID));
        }
        public static void C_FirePacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_FirePacket _packet = packet as C_FirePacket;

            S_FirePacket s_FirePacket = new();

            s_FirePacket.x = _packet.x;
            s_FirePacket.y = _packet.y;
            s_FirePacket.z = _packet.z;

            Program.rooms[_session.roomID].AddJob
                (() => Program.rooms[_session.roomID].Broadcast(s_FirePacket, _session.userID));
        }
        public static void C_HitPacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_HitPacket _packet = packet as C_HitPacket;

            Vector2 hit = new Vector2(_packet.x, _packet.y);
            Vector2 target = new Vector2();
        }
        public static void C_GameEndPacket(Session session, Packet packet)
        {
            ClientSession _session = session as ClientSession;
            C_GameEndPacket _packet = packet as C_GameEndPacket;
        }
    }
}
