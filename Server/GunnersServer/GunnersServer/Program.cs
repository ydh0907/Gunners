﻿using System;
using System.Net;
using System.Net.Sockets;
using Do.Net;

namespace GunnersServer
{
    public class Program
    {
        public static Listener listener;
        public static Dictionary<ushort, ClientSession> users = new();
        public static Dictionary<ushort, GameRoom> rooms = new();
        public static GameRoom matchingRoom = new(NextRoomID);

        private static ushort nextUserID = 0;
        private static ushort nextRoomID = 0;

        public static ushort NextUserID
        {
            get
            {
                if (nextUserID == ushort.MaxValue) nextUserID = ushort.MinValue;
                while(users.ContainsKey(nextUserID)) nextUserID++;
                return nextUserID;
            }
        }
        public static ushort NextRoomID
        {
            get
            {
                if(nextRoomID == ushort.MaxValue) nextRoomID = ushort.MinValue;
                while(rooms.ContainsKey(nextRoomID)) nextRoomID++;
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
            while(true)
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

        static void EnterRoom(ClientSession user)
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

                    Console.WriteLine($"[Room] {matchingRoom.host.userID} : {matchingRoom.host.endPoint} and {matchingRoom.enterer.userID} : {matchingRoom.enterer.endPoint} is Matching");

                    matchingRoom = new(NextRoomID);
                }
            }
        }
    }
}