using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandler : MonoBehaviour
{
    public static ClientHandler handler = null;

    TcpClient tcpClient = null;
    NetworkStream ns = null;

    public Thread readThread = null;
    public Thread writeThread = null;

    public Queue<Socket> readQueue = null;
    public Queue<Socket> writeQueue = null;

    public bool IsThreadWork = true;
    public bool IsComAble = false;
    public object locking = new();
    private void Awake() {
        if(handler != null) Destroy(gameObject);
        handler = this;
        Connect();
    }
    private void Connect(){
        Disconnect();
        IsComAble = false;
        readThread = new(new ThreadStart(Connecting));
    }
    private void OnDisable() {
        Disconnect();
        handler = null;
    }
    private void Disconnect(){
        IsThreadWork = false;
        IsComAble = false;
        readQueue?.Clear();
        writeQueue?.Clear();
        readQueue = null;
        writeQueue = null;
        readThread?.Join();
        readThread?.Abort();
        readThread = null;
        ns?.Close();
        ns = null;
        tcpClient?.Close();
        tcpClient = null;
    }
    private void Connecting(){
        tcpClient = new TcpClient(TcpInterface.handler.HostIPAddress, 9070);
        if(!tcpClient.Connected){
            Disconnect();
            SceneManager.LoadScene(0);
        }
        ns = tcpClient.GetStream();
        IsThreadWork = true;
        readQueue = new();
        writeQueue = new();
        readThread = new(new ThreadStart(Reading));
        readThread.IsBackground = true;
        readThread.Start();
        StartCoroutine(Writing());
        IsComAble = true;
    }
    private void Update() {
        if(!tcpClient.Connected && IsComAble){
            Disconnect();
            SceneManager.LoadScene(0);
        }
    }
    private void Reading(){
        while(IsThreadWork){
            while(IsComAble){
                byte[] message = new byte[64];
                if(ns.CanRead) ns.Read(message);
                lock(locking) readQueue.Enqueue(JsonUtility.FromJson<Socket>(Encoding.ASCII.GetString(message)));
            }
        }
    }
    private IEnumerator Writing(){
        while(IsThreadWork){
            while(IsComAble){
                while(writeQueue.Count > 0 && ns.CanWrite){
                    lock(locking) ns.Write(Encoding.ASCII.GetBytes(JsonUtility.ToJson(writeQueue.Dequeue())));
                }
                yield return null;
            }
            yield return null;
        }
    }
}
