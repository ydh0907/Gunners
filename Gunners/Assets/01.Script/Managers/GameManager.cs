using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Do.Net;
using GunnersServer.Packets;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public CharacterListSO CharacterList;
    [SerializeField] public GunListSO GunList;

    public Action onMatchingStart = null;
    public Action onMatched = null;

    public Action onGameWin = null;
    public Action onGameLose = null;
    public Action onGameStart = null;

    public JobQueue JobQueue = new JobQueue();

    public bool isWin = false;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        PacketManager.Instance = gameObject.AddComponent<PacketManager>();
        LoadSceneManager.Instance = gameObject.AddComponent<LoadSceneManager>();
        AgentInfoManager.Instance = gameObject.AddComponent<AgentInfoManager>();
    }

    public void StartMatching()
    {
        onMatchingStart?.Invoke();

        C_MatchingPacket c_MatchingPacket = new C_MatchingPacket();
        c_MatchingPacket.agent = (ushort)CharacterList.characters.IndexOf(AgentInfoManager.Instance.Character);
        c_MatchingPacket.weapon = (ushort)GunList.guns.IndexOf(AgentInfoManager.Instance.Gun);

        NetworkManager.Instance.Send(c_MatchingPacket);
    }

    public void Kill()
    {
        NetworkManager.Instance.Send(new C_GameEndPacket());
    }

    public void OnStart()
    {
        onGameStart?.Invoke();
    }

    public void Win()
    {
        onGameWin?.Invoke();
    }

    public void Lose()
    {
        onGameLose?.Invoke();
    }

    public void Matched(bool host, ushort character, ushort gun)
    {
        LoadSceneManager.Instance.LoadSceneAsync("GameScene", () =>
        {
            onMatched?.Invoke();

            Agent.Make(AgentInfoManager.Instance.Character.character, AgentInfoManager.Instance.Gun.gun, host);
            EnemyDummy.Make(CharacterList.characters[character].character, GunList.guns[gun].gun, host);

            NetworkManager.Instance.Send(new C_ReadyPacket());
        });
    }
}
