using GunnersServer.Packets;
using System;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public static EnemyDummy Instance = null;

    public static void Make(ICharacter character, IGun gun, bool host)
    {
        if (Instance != null) throw new Exception("instance is not null");

        Instance = Instantiate(character).gameObject.AddComponent<EnemyDummy>();
        Instance.gun = Instantiate(gun, Instance.transform).GetComponent<IGun>();
        Instance.character = Instance.GetComponent<ICharacter>();
        Instance.host = host;

        Instance.gun.dummy = true;

        if (host)
            Instance.transform.position = Vector3.left * 5;
        else
            Instance.transform.position = Vector3.right * 5;
    }

    public IGun gun;
    public ICharacter character;

    public bool host;

    private Rigidbody2D rb;
    private SpriteRenderer characterSR;
    private SpriteRenderer gunSR;

    private float time = 0;

    private float sdelta = 0;
    private float cdelta = 0;
    private float delta => cdelta - sdelta;

    Vector3 targetPos => currentPos - pastPos;
    Vector3 targetDir => currentDir - pastDir + currentDir;

    Vector3 currentPos;
    Vector3 currentDir;
    Vector3 pastPos;
    Vector3 pastDir;

    private float Z => gun.transform.eulerAngles.z > 180 ? gun.transform.eulerAngles.z - 360f : gun.transform.eulerAngles.z;

    private void Start()
    {
        currentPos = transform.position;
        currentDir = transform.eulerAngles;
        pastPos = transform.position;
        pastDir = transform.eulerAngles;

        rb = character.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        gunSR = gun.GetComponent<SpriteRenderer>();
        characterSR = character.transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        gun.transform.eulerAngles = Vector3.Lerp(currentDir, targetDir, time);

        cdelta += Time.fixedDeltaTime;
        time += Time.fixedDeltaTime * (1 / delta);
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
        pastPos = currentPos;
        pastDir = currentDir;

        currentPos.x = x;
        currentPos.y = y;
        currentDir = new Vector3(0, 0, angleZ);

        time = 0f;
        sdelta = cdelta;

        if (Z > 90 || Z < -90)
        {
            characterSR.flipX = true;
            gunSR.flipY = true;
        }
        else
        {
            characterSR.flipX = false;
            gunSR.flipY = false;
        }

        rb.velocity = targetPos;
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
