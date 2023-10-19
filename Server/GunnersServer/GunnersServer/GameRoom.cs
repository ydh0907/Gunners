using System;
using Do.Net;

namespace GunnersServer
{
    public class GameRoom
    {
        public ushort roomID;

        public ClientSession host = null;
        public ClientSession enterer = null;

        public JobQueue jobQueue = new JobQueue();
        public Queue<Tuple<ushort, Packet>> packetQueue = new();

        public void AddJob(Action job) => jobQueue.Push(job);

        public void Flush()
        {
            if(host.Active + enterer.Active == 2)
            {
                while(packetQueue.Count > 0)
                {
                    Tuple<ushort, Packet> tuple = packetQueue.Dequeue();
                    ushort id = tuple.Item1;
                    Packet packet = tuple.Item2;

                    if (id == host.userID) enterer.Send(packet.Serialize());
                    else host.Send(packet.Serialize());
                }
            }
            else Console.WriteLine("[Room] User not Found - Flush");
        }

        public Session GetUser(ushort id)
        {
            if(host.userID == id)
                return host;
            if(enterer.userID == id)
                return enterer;

            Console.WriteLine($"[Room] User not Found - Attempted ID : {id} - GetUser");
            return null;
        }

        public Session GetUser(bool isHost)
        {
            if(isHost && host.Active == 1) return host;
            else if(!isHost && enterer.Active == 1) return enterer;

            Console.WriteLine("[Room] User not Found - GetUser");
            return null;
        }

        public void DestroyRoom()
        {
            Program.rooms.Remove(roomID);

            // send exit packet
        }

        public GameRoom(ushort id)
        {
            roomID = id;
        }
    }
}
