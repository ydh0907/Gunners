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
            Instance.transform.position = Vector3.left * 5;
        else
            Instance.transform.position = Vector3.right * 5;
    }

    public IGun gun;
    public ICharacter character;

    public bool host;
    public string nickname;

    private Animator ani;
    public Rigidbody2D rb;
    private SpriteRenderer characterSR;
    private SpriteRenderer gunSR;

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

        rb = character.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        gunSR = gun.GetComponent<SpriteRenderer>();
        characterSR = character.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        ani = characterSR.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun?.gameObject.SetActive(true);
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
            characterSR.flipX = true;
            gunSR.flipY = true;
        }
        else
        {
            characterSR.flipX = false;
            gunSR.flipY = false;
        }
    }

    private void Update()
    {
        SetAni();
    }

    private void SetAni()
    {
        if (rb.velocity.magnitude > 0.1f) ani.SetBool("Run", true);
        else ani.SetBool("Run", false);

        if (rb.velocity.y > 0.1f || rb.velocity.y < -0.1f) ani.SetBool("Jump", true);
        else ani.SetBool("Jump", false);

        if (ani.GetBool("Jump"))
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.6f, 1 << 7))
            {
                characterSR.flipX = true;
                ani.SetBool("Slide", true);
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -0.5f, float.MaxValue));
            }
            else if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, 1 << 7))
            {
                characterSR.flipX = false;
                ani.SetBool("Slide", true);
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -0.5f, float.MaxValue));
            }
            else
            {
                ani.SetBool("Slide", false);
            }
        }
        else
        {
            ani.SetBool("Slide", false);
        }
    }

    public void Hit(ushort damage)
    {
        character.SetHP((int)Mathf.Clamp((character.hp - (damage * (character.armor / 100))), 0, ushort.MaxValue));

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
