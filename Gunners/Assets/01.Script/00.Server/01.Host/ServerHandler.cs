using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerHandler : MonoBehaviour
{
    public static ServerHandler handler = null;

    TcpListener tcpListener = null;
    TcpClient tcpClient = null;
    NetworkStream ns = null;

    public Thread readThread = null;
    public Thread writeThread = null;

    public Queue<Packet> readQueue = null;
    public Queue<Packet> writeQueue = null;

    public bool IsThreadWork = true;
    public bool IsComAble = false;
    public object locking = new();
    private void Awake() {
        if(handler == null) handler = this;
        else gameObject.SetActive(false);
        Connect();
    }
    private void Connect(){
        Disconnect();
        IsComAble = false;
        tcpListener = new TcpListener(IPAddress.Any, 9070);
        readThread = new(new ThreadStart(Connecting));
    }
    private void OnDestroy() {
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
        tcpListener?.Stop();
        tcpListener = null;
    }
    private void Connecting(){
        tcpClient = tcpListener.AcceptTcpClient();
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
                lock(locking) readQueue.Enqueue(JsonUtility.FromJson<Packet>(Encoding.ASCII.GetString(message)));
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
