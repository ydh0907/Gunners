using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public static EnemyDummy Instance;

    public static void Make(ICharacter charater, IGun gun)
    {
        Instantiate(gun, Instantiate(charater, Vector3.zero, Quaternion.identity).AddComponent<EnemyDummy>().transform);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Hit(ushort damage)
    {

    }

    public void Move(float x, float y, float angleZ)
    {

    }
}
