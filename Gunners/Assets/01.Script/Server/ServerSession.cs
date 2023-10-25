using Do.Net;
using System;
using System.Net;

public class ServerSession : Session
{
    public ushort userID = ushort.MaxValue;
    public ushort roomID = ushort.MaxValue;
    public string nickname = "";

    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"[Session] Connected on {endPoint}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Console.WriteLine($"[Session] Disconnected on {endPoint}");
    }

    public override void OnPacketReceived(ArraySegment<byte> buffer)
    {
        Console.WriteLine($"[Session] {buffer.Count} of byte Received");
        PacketManager.Instance.HandlePacket(this, PacketManager.Instance.CreatePacket(buffer));
    }

    public override void OnSent(int length)
    {
        Console.WriteLine($"[Session] {length} of byte Sent");
    }
}
