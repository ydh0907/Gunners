using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayer : MonoBehaviour
{
    private void Update() {
        if(ConnectInfo.info.IsServer){
            Debug.Log("read");
            if(ServerHandler.handler != null && ServerHandler.handler.IsComAble){
                Packet packet = null;
                if(ServerHandler.handler.readQueue.Count > 0)
                    packet = ServerHandler.handler.readQueue.Dequeue();
                if(packet != null){
                    Debug.Log(packet);
                    transform.position = packet.pos;
                }
            }
        }
        else{
            Debug.Log("read");
            if(ClientHandler.handler != null && ClientHandler.handler.IsComAble){
                Packet packet = null;
                if(ClientHandler.handler.readQueue.Count > 0)
                    packet = ClientHandler.handler.readQueue.Dequeue();
                if(packet != null){
                    Debug.Log(packet);
                    transform.position = packet.pos;
                }
            }
        }
    }
}
