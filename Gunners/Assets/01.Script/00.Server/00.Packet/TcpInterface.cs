using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcpInterface : MonoBehaviour
{
    public static TcpInterface handler = null;

    public bool IsServer;
    public string RoomNumber;

    private bool IsActive = false;
    private Queue<Packet> readQueue = null;
    private Queue<Packet> writeQueue = null;

    private void Awake() {
        if(handler != null) Destroy(gameObject);
        handler = this;
    }
    public void Init(bool IsServer, string RoomNumber){
        this.IsServer = IsServer;
        this.RoomNumber = RoomNumber;
        IsActive = true;
        if(IsServer){
            readQueue = ServerHandler.handler.readQueue;
            writeQueue = ServerHandler.handler.writeQueue;
        }
        else{
            readQueue = ClientHandler.handler.readQueue;
            writeQueue = ClientHandler.handler.writeQueue;
        }
    }
    public void Send(Packet packet){
        if(IsActive) writeQueue.Enqueue(packet);
    }
    public Packet Read(){
        if(IsActive) return readQueue.Dequeue();
        return null;
    }
}
