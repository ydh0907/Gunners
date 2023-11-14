using Do.Net;
using GunnersServer.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public static EnemyDummy Instance = null;

    public static void Make(ICharacter charater, IGun gun, bool host)
    {
        if (Instance != null) throw new Exception("instance is not null");

        EnemyDummy enemy = Instantiate(charater, Vector3.zero, Quaternion.identity).AddComponent<EnemyDummy>();
        Instantiate(gun, enemy.transform);

        Instance = enemy;
        Instance.gun = gun;
        Instance.host = host;
        Instance.character = charater;
    }

    public IGun gun;
    public JobQueue jobQueue = new();
    public ICharacter character;

    public bool host;

    private void Update()
    {
        jobQueue.Flush();
    }

    public void Hit(ushort damage)
    {
        character.SetHP((ushort)(character.hp - (damage * (character.armor / 100))));

        C_HitPacket c_HitPacket = new C_HitPacket();
        c_HitPacket.hp = character.hp;

        NetworkManager.Instance.Send(c_HitPacket);

        if(character.hp <= 0)
        {
            GameManager.Instance.Kill();
        }
    }

    public void Move(float x, float y, float angleZ)
    {
        jobQueue.Push(() =>
        {
            transform.position = new Vector2(x, y);
            gun.transform.eulerAngles = new Vector3(0, 0, angleZ);

            if(-90 <= angleZ && angleZ < 90)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        });
    }

    public void Fire()
    {
        jobQueue.Push(() =>
        {
            gun.Fire();
        });
    }

    public void Reroad()
    {
        gun.Reroad();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
