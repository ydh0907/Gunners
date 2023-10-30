using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GunSO gun;
    public CharacterSO character;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
