using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool dummy = false;
    private float speed;
    private ushort damage;

    public void Fire(float speed, ushort damage, bool dummy)
    {
        this.speed = speed;
        this.damage = damage;
        this.dummy = dummy;
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyDummy enemy) && !dummy)
        {
            enemy.Hit(damage);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
