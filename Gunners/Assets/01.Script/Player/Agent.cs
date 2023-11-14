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
        Instance.gun = Instantiate(gun, Instance.transform).GetComponent<IGun>();
        Instance.character = Instance.GetComponent<ICharacter>();
        Instance.rb = Instance.GetComponent<Rigidbody2D>();
        Instance.host = host;
    }

    public IGun gun;
    public JobQueue JobQueue;
    public ICharacter character;

    public bool host;

    [SerializeField] private float max = 0.1f;
    private float current = 0;

    public bool move;
    private float moveX;
    private Rigidbody2D rb;
    private Vector2 mouseDir;

    private void Start()
    {
        if (host)
            transform.position = Vector3.left * 5;
        else 
            transform.position = Vector3.right * 5;


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
        current += Time.fixedDeltaTime;

        if(current >= max)
        {
            current = 0;

            C_MovePacket c_MovePacket = new C_MovePacket();
            c_MovePacket.x = transform.position.x;
            c_MovePacket.y = transform.position.y;
            c_MovePacket.z = transform.eulerAngles.z;

            NetworkManager.Instance.Send(c_MovePacket);
        }
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        Move(moveX);
        Dir(mouseDir);

        JobQueue.Flush();
    }

    public void Move(float x)
    {
        if (move)
        {
            rb.velocity = new Vector2(x, rb.velocity.y);
        }
    }

    public void Dir(Vector2 pos)
    {

    }

    public void SetHP(ushort hp)
    {
        character.SetHP(hp);
    }
}
