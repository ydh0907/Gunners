using GunnersServer.Packets;
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

        if (host)
            Instance.transform.position = Vector3.left * 5;
        else
            Instance.transform.position = Vector3.right * 5;
    }

    public IGun gun;
    public ICharacter character;
    public JobQueue JobQueue;

    public bool host;

    private SpriteRenderer characterSR;
    private SpriteRenderer gunSR;

    [SerializeField] private float time = 0.2f;
    private float current = 0;

    public bool move = true;

    private float moveX;
    private Rigidbody2D rb;

    private Camera cam;
    private Vector2 mouseDir;

    private float PastZ = 0;
    private Vector2 PastPos = Vector2.zero;

    private float Z => gun.transform.eulerAngles.z > 180 ? gun.transform.eulerAngles.z - 360f : gun.transform.eulerAngles.z;

    private void Start()
    {
        characterSR = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        gunSR = gun.GetComponent<SpriteRenderer>();

        cam = Camera.main;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Fire()
    {
        gun.Fire();
    }

    private void FixedUpdate()
    {
        current += Time.fixedDeltaTime;

        if(current > time)
        {
            current = 0;

            C_MovePacket c_MovePacket = new C_MovePacket();
            c_MovePacket.x = transform.position.x;
            c_MovePacket.y = transform.position.y;
            c_MovePacket.z = Z;

            NetworkManager.Instance.Send(c_MovePacket);
        }
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        mouseDir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        Move(moveX * character.speed);
        Dir(mouseDir);

        if (Input.GetMouseButton(0)) Fire();

        JobQueue?.Flush();
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
        gun.transform.right = pos;

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

    public void SetHP(ushort hp)
    {
        character.SetHP(hp);
    }
}
