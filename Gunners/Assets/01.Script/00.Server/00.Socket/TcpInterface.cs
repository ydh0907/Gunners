using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcpInterface : MonoBehaviour
{
    public static TcpInterface handler = null;

    public string HostIPAddress;
    public bool IsServer;

    private void Awake() {
        if(handler != null) Destroy(gameObject);
        handler = this;
        HostIPAddress = GetIP();
    }
    private void OnDestroy() {
        handler = null;
    }
    public string GetIP(){
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        for (int i = 0; i < host.AddressList.Length; i++)
        {
            if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                System.Net.IPAddress hostAddress = host.AddressList[i];
                return hostAddress.ToString();
            }
        }
        return null;
    }
    
}
