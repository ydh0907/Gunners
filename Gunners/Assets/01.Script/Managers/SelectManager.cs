using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    private static SelectManager instance;
    public static SelectManager Instance
    {
        get => instance;
        set
        {
            instance = value;
            instance.Init();
        }
    }

    public Agent agent;
    [SerializeField]

    private void Init()
    {
        agent = GameObject.Find("Agent").GetComponent<Agent>();
    }

    public void ChangeCharacter(int index)
    {

    }

    public void ChangeGun(int index)
    {

    }
}
