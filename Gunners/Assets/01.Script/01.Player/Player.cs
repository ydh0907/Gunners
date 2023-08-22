using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2D;
    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        float x = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(x * 2, rb2D.velocity.y);
    }
    private void FixedUpdate() {
        if(ConnectInfo.info.IsServer){
            Debug.Log("send");
            if(ServerHandler.handler != null && ServerHandler.handler.IsComAble){
                Packet packet = new Packet(1, transform.position, transform.eulerAngles);
                ServerHandler.handler.writeQueue.Enqueue(packet);
                Debug.Log(packet);
            }
        }
        else{
            Debug.Log("send");
            if(ClientHandler.handler != null && ClientHandler.handler.IsComAble){
                Packet packet = new Packet(1, transform.position, transform.eulerAngles);
                ClientHandler.handler.writeQueue.Enqueue(packet);
                Debug.Log(packet);
            }
        }
    }
}
