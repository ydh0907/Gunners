using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public CharacterListSO CharacterList;
    [SerializeField] public GunListSO GunList;

    public Action onGameWin = null;
    public Action onGameLose = null;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        PacketManager.Instance = gameObject.AddComponent<PacketManager>();
        LoadSceneManager.Instance = gameObject.AddComponent<LoadSceneManager>();
        AgentInfoManager.Instance = gameObject.AddComponent<AgentInfoManager>();
    }

    public void Win()
    {
        onGameWin?.Invoke();
    }

    public void Lose()
    {
        onGameLose?.Invoke();
    }
}
