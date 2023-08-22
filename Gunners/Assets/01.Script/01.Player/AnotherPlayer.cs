using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayer : MonoBehaviour
{
    private void Update() {
        if(ConnectInfo.info.IsServer){
            if(ServerHandler.handler != null && ServerHandler.handler.IsComAble){
                Packet packet = ServerHandler.handler.writeQueue.Dequeue();
                if(packet != null){
                    transform.position = packet.pos;
                }
            }
            else if(ClientHandler.handler != null && ClientHandler.handler.IsComAble){
                Packet packet = ClientHandler.handler.writeQueue.Dequeue();
                if(packet != null){
                    transform.position = packet.pos;
                }
            }
        }
    }
}
