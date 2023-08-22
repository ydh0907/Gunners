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
    public Thread connectThread = null;

    public Queue<Packet> readQueue = new();
    public Queue<Packet> writeQueue = new();

    public string RoomNumber;

    public bool IsThreadWork = true;
    public bool IsComAble = false;
    public object locking = new();
    private void Awake() {
        if(handler == null) handler = this;
        else gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
        Connect();
    }
    private void Connect(){
        Disconnect();
        IsComAble = false;
        RoomNumber = ConnectInfo.info.RoomNumber;
        connectThread = new(new ThreadStart(Connecting));
        connectThread.IsBackground = true;
        connectThread.Start();
        StartCoroutine(WaitConnect());
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
        ns?.Close();
        ns = null;
        tcpClient?.Close();
        tcpClient = null;
    }
    private void Connecting(){
        tcpClient = new TcpClient(ConnectInfo.info.RoomNumber, 9070);
        if(!tcpClient.Connected){
            Disconnect();
            SceneManager.LoadScene(0);
        }
        Debug.Log(tcpClient.Connected);
        ns = tcpClient.GetStream();
        IsThreadWork = true;
        readThread = new(new ThreadStart(Reading));
        readThread.IsBackground = true;
        readThread.Start();
        IsComAble = true;
    }
    private IEnumerator WaitConnect(){
        yield return new WaitUntil(() => IsComAble);
        StartCoroutine(Writing());
        gameObject.AddComponent<TcpInterface>().Init(true);
    }
    private void Update() {
        if(tcpClient != null)
        if(!tcpClient.Connected && IsComAble){
            Disconnect();
            SceneManager.LoadScene(0);
        }
    }
    private void Reading(){
        while(true){
            while(IsComAble){
                byte[] message = new byte[256];
                ns.Read(message);
                lock(locking) readQueue.Enqueue(JsonUtility.FromJson<Packet>(Encoding.ASCII.GetString(message)));
            }
            Thread.Sleep(10);
            if(!IsThreadWork) break;
        }
    }
    private IEnumerator Writing(){
        while(true){
            while(IsComAble){
                while(writeQueue.Count > 0){
                    lock(locking) ns.Write(Encoding.ASCII.GetBytes(JsonUtility.ToJson(writeQueue.Dequeue())));
                }
                yield return null;
            }
            yield return null;
            if(!IsThreadWork) break;
        }
    }
}
