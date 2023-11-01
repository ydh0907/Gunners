using Do.Net;
using GunnersServer.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public static Agent Instance = null;

    public static void Make(ICharacter character, IGun gun, bool host)
    {
        Instance = Instantiate(character).gameObject.AddComponent<Agent>();
        Instantiate(gun, Instance.transform);

        Instance.gun = gun;
        Instance.host = host;
        Instance.character = character;
    }

    public IGun gun;
    public JobQueue JobQueue;
    public ICharacter character;

    public bool host;

    [SerializeField] private float max = 0.1f;
    private float current = 0;

    private void Awake()
    {
        // 여기서 위치 정하기
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Fire()
    {
        gun.Fire();

        NetworkManager.Instance.Send(new C_FirePacket());
    }

    private void FixedUpdate()
    {
        current += Time.deltaTime;

        if(current > max)
        {
            current = 0;

            C_MovePacket c_MovePacket = new C_MovePacket();
            c_MovePacket.x = transform.position.x;
            c_MovePacket.y = transform.position.y;
            c_MovePacket.z = transform.eulerAngles.z;

            NetworkManager.Instance.Send(c_MovePacket);
        }
    }

    public void Move(Vector2 dir)
    {

    }

    public void Dir(Vector2 pos)
    {

    }

    public void SetHP(ushort hp)
    {
        JobQueue.Push(() =>
        {
            character.SetHP(hp);
        });
    }
}
