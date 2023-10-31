using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private ushort damage;

    public void Fire(float speed, ushort damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        transform.position += Vector3.down * 9.8f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyDummy>(out EnemyDummy enemy))
        {
            enemy.Hit(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
