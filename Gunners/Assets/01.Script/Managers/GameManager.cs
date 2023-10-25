using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        PacketManager.Instance = gameObject.AddComponent<PacketManager>();
        LoadSceneManager.Instance = gameObject.AddComponent<LoadSceneManager>();
    }
}
