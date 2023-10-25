using Do.Net;
using GunnersServer.Packets;
using System;
using UnityEngine;

public class PacketHandler
{
    public static void S_ConnectPacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_ConnectPacket _packet = packet as S_ConnectPacket;

        _session.userID = _packet.userID;
    }

    public static void S_FirePacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_FirePacket _packet = packet as S_FirePacket;
    }

    public static void S_GameEndPacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_GameEndPacket _packet = packet as S_GameEndPacket;
    }

    public static void S_GameStartPacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_GameStartPacket _packet = packet as S_GameStartPacket;
    }

    public static void S_HitPacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_HitPacket _packet = packet as S_HitPacket;
    }

    public static void S_MatchedPacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_MatchedPacket _packet = packet as S_MatchedPacket;
    }

    public static void S_MovePacket(Session session, Packet packet)
    {
        ServerSession _session = session as ServerSession;
        S_MovePacket _packet = packet as S_MovePacket;
    }
}
