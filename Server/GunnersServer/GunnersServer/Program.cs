using System.Net;
using System.Net.Sockets;
using Do.Net;
using GunnersServer.Packets;

namespace GunnersServer
{
    public class Program
    {
        public static Listener listener;
        public static Dictionary<ushort, ClientSession> users = new();
        public static Dictionary<ushort, GameRoom> rooms = new();
        public static GameRoom matchingRoom = new(NextRoomID);
        public static JobQueue jobQueue = new();

        private static ushort nextUserID = 0;
        private static ushort nextRoomID = 0;

        public static ushort NextUserID
        {
            get
            {
                if (nextUserID == ushort.MaxValue) nextUserID = ushort.MinValue;
                while (users.ContainsKey(nextUserID))
                {
                    nextUserID++;
                    if (nextUserID == ushort.MaxValue) nextUserID = ushort.MinValue;
                }
                return nextUserID;
            }
        }
        public static ushort NextRoomID
        {
            get
            {
                Console.WriteLine($"[Program] Room count : {rooms.Count}");

                if (nextRoomID == ushort.MaxValue) nextRoomID = ushort.MinValue;
                while (rooms.ContainsKey(nextRoomID))
                {
                    nextRoomID++;
                    if (nextRoomID == ushort.MaxValue) nextRoomID = ushort.MinValue;
                }
                return nextRoomID;
            }
        }

        public static object matchingLocker = new();

        static void Main(string[] args)
        {
            IPEndPoint endPoint = new(IPAddress.Any, 9070);
            listener = new(endPoint);

            if (listener.Listen(10))
                listener.StartAccept(OnAccepted);

            FlushLoop(20);
        }

        static void FlushLoop(int delay)
        {
            int lastFlushTime = Environment.TickCount;
            int currentTime;
            while (true)
            {
                currentTime = Environment.TickCount;
                if(currentTime - lastFlushTime > delay)
                {
                    foreach(GameRoom room in rooms.Values) 
                    {
                        room.Flush();
                    }
                    lastFlushTime = currentTime;
                }
            }
        }

        static void OnAccepted(Socket socket)
        {
            ClientSession session = new ClientSession();
            session.Open(socket);
            session.OnConnected(socket.RemoteEndPoint);
        }

        public static void EnterRoom(ClientSession user)
        {
            lock(matchingLocker)
            {
                if(matchingRoom.host == null)
                {
                    matchingRoom.host = user;
                }
                else
                {
                    matchingRoom.enterer = user;
                    rooms.Add(matchingRoom.roomID, matchingRoom);

                    S_MatchedPacket s_MatchedPacket = new();
                    s_MatchedPacket.roomID = matchingRoom.roomID;

                    s_MatchedPacket.host = true;
                    s_MatchedPacket.name = matchingRoom.enterer.nickname;
                    s_MatchedPacket.agent = matchingRoom.enterer.agent;
                    s_MatchedPacket.weaponID = matchingRoom.enterer.weaponID;
                    matchingRoom.AddJob
                        (() => matchingRoom.Broadcast(s_MatchedPacket, matchingRoom.enterer.userID));

                    s_MatchedPacket.host = false;
                    s_MatchedPacket.name = matchingRoom.host.nickname;
                    s_MatchedPacket.agent = matchingRoom.host.agent;
                    s_MatchedPacket.weaponID = matchingRoom.host.weaponID;
                    matchingRoom.AddJob
                        (() => matchingRoom.Broadcast(s_MatchedPacket, matchingRoom.host.userID));

                    Console.WriteLine($"[Room] {matchingRoom.host.userID} : {matchingRoom.host.endPoint} and {matchingRoom.enterer.userID} : {matchingRoom.enterer.endPoint} is Matching");

                    matchingRoom = new(NextRoomID);
                }
            }
        }

        public static void AddJob(Action action) => jobQueue.Push(action);
    }
}