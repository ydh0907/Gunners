using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcpInterface : MonoBehaviour
{
    public static TcpInterface handler = null;

    public bool IsServer = true;
    public bool IsActive = false;
    private void Awake() {
        if(handler != null) Destroy(gameObject);
        handler = this;
    }
    public void Init(bool IsServer){
        this.IsServer = IsServer;
        IsActive = true;
    }
    public void Send(Packet packet){
        if(IsServer){
            if(IsActive) ServerHandler.handler.writeQueue.Enqueue(packet);
        }
        else{
            if(IsActive) ClientHandler.handler.writeQueue.Enqueue(packet);
        }
    }
    public Packet Read(){
        if(IsServer){
            if(IsActive && ServerHandler.handler.readQueue.Count > 0) return ServerHandler.handler.readQueue.Dequeue();
        }
        else{
            if(IsActive && ClientHandler.handler.readQueue.Count > 0) return ClientHandler.handler.readQueue.Dequeue();
        }
        return null;
    }
}
