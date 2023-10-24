using System;
using System.Xml.Serialization;
using Do.Net;

namespace GunnersServer
{
    public class PacketManager
    {
        private static PacketManager instance;
        public static PacketManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new PacketManager();
                return instance;
            }
        }

        private Dictionary<ushort, Func<ArraySegment<byte>, Packet>> packetFactories = new();
        private Dictionary<ushort, Action<Session, Packet>> packetHandlers = new();

        public PacketManager()
        {
            packetFactories.Clear();
            packetHandlers.Clear();

            RegisterHandler();
        }

        public void RegisterHandler()
        {

        }

        public Packet CreatePacket(ArraySegment<byte> buffer)
        {
            ushort id = PacketUtility.ReadPacketID(buffer);

            if(packetFactories.ContainsKey(id))
                return packetFactories[id]?.Invoke(buffer);
            else Console.WriteLine($"[Manager] Packet ID not Found - CreatePacket");

            return null;
        }

        public void HandlePacket(Session session, Packet packet)
        {
            if (packet != null)
                if (packetHandlers.ContainsKey(packet.ID))
                    packetHandlers[packet.ID]?.Invoke(session, packet);
                else Console.WriteLine($"[Manager] Packet ID not Found - HandlePacket");
            else Console.WriteLine($"[Manager] Packet is null - HandlePacket");
        }
    }
}
