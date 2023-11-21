using GunnersServer.Packets;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public static EnemyDummy Instance = null;

    public static void Make(ICharacter character, IGun gun, bool host, string name)
    {
        if (Instance != null) throw new Exception("instance is not null");

        Instance = Instantiate(character).gameObject.AddComponent<EnemyDummy>();
        Instance.gun = Instantiate(gun, Instance.transform).GetComponent<IGun>();
        Instance.character = Instance.GetComponent<ICharacter>();
        Instance.host = host;
        Instance.nickname = name;

        Instance.gun.dummy = true;

        if (host)
            Instance.transform.position = Map.Host;
        else
            Instance.transform.position = Map.Enterer;
    }

    public IGun gun;
    public ICharacter character;

    public bool host;
    public string nickname = "";

    private Animator ani;
    public Rigidbody2D rb;
    private SpriteRenderer sr;

    private float delta => Agent.Instance.time;

    Vector3 currentPos;
    float currentDir;
    Vector3 pastPos;

    private float Z => gun.transform.eulerAngles.z > 180 ? gun.transform.eulerAngles.z - 360f : gun.transform.eulerAngles.z;

    private void Start()
    {
        currentPos = transform.position;
        currentDir = transform.eulerAngles.z;
        pastPos = transform.position;

        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        rb = character.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void OnDisable()
    {
        gun?.gameObject.SetActive(false);
    }

    public void Move(float x, float y, float angleZ)
    {
        pastPos = currentPos;

        currentPos.x = x;
        currentPos.y = y;
        currentDir = angleZ;

        transform.position = pastPos;
        rb.velocity = (currentPos - pastPos) * (1 / delta);
        gun.transform.eulerAngles = new Vector3(0, 0, currentDir);

        if (Z > 90 || Z < -90)
        {
            sr.flipX = true;
            gun.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            sr.flipX = false;
            gun.transform.localScale = Vector3.one;
        }
    }

    public void Hit(ushort damage)
    {
        character.SetHP((int)Mathf.Clamp(character.hp - damage * (100 - character.armor) * 0.01f, 0, ushort.MaxValue));

        C_HitPacket c_HitPacket = new C_HitPacket();
        c_HitPacket.hp = (ushort)character.hp;

        NetworkManager.Instance.Send(c_HitPacket);

        if(character.hp <= 0)
        {
            GameManager.Instance.End();
        }
    }

    public void Fire()
    {
        ++Instance.gun.bulletCount;
        gun.Fire();
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
