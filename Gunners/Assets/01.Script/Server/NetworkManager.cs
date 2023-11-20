using Do.Net;
using System.Net;
using GunnersServer.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance = null;

    public Connector connector = null;
    public ServerSession session = new();

    public Queue<Packet> packetQueue = new();
    public JobQueue JobQueue = new();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    private void Update()
    {
        JobQueue.Flush();
    }

    private void OnApplicationQuit()
    {
        session.Close();
    }

    public void Connect()
    {
        IPEndPoint endPoint = new(IPAddress.Parse("172.31.3.212"), 9070);
        connector = new Connector(endPoint, session);
        connector.StartConnect(endPoint);

        StartCoroutine(connecting());
    }

    public void Send(Packet packet)
    {
        JobQueue.Push(() => packetQueue.Enqueue(packet));
    }

    private IEnumerator connecting()
    {
        yield return new WaitUntil(() => session.Active != 0);

        session.nickname = GameObject.Find("Name").GetComponent<TMPro.TMP_InputField>().text;
        if(session.nickname.Length > 16)
            session.nickname.Remove(15);
        if (session.nickname == "") session.nickname = "unknown";

        C_ConnectPacket c_ConnectPacket = new();
        c_ConnectPacket.nickname = session.nickname;

        packetQueue.Enqueue(c_ConnectPacket);

        StartCoroutine(Flush());

        LoadSceneManager.Instance.LoadSceneAsync("MainScene", () => Debug.Log("connected : " + session.Active));
    }

    private IEnumerator Flush()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);

        while (session.Active == 1)
        {
            while(packetQueue.Count > 0)
            {
                session.Send(packetQueue.Dequeue().Serialize());
            }
            yield return wait;
        }
    }
}
