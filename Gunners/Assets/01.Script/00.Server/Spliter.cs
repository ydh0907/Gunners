using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spliter : MonoBehaviour
{
    private void Awake() {
        if(ConnectInfo.info.IsServer){
            gameObject.AddComponent<ServerHandler>();
            GameObject player = GameObject.Find("HostPlayer");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<Player>();
            GameObject.Find("ClientPlayer").AddComponent<AnotherPlayer>();
        }
        else{
            gameObject.AddComponent<ClientHandler>();
            GameObject player = GameObject.Find("ClientPlayer");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<Player>();
            GameObject.Find("HostPlayer").AddComponent<AnotherPlayer>();
        }
    }
}
