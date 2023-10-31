using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : IGun
{
    private void Awake()
    {
        fireAble = true;
        fireRate = 0.1f;
        lastRate = 0;
        reroadTime = 2;
        bulletSpeed = 20;
        bulletCount = 30;
        bulletPellet = 1;
        bulletDamage = 10;
        bulletMaximum = 30;
    }

    private void Update()
    {
        lastRate = Mathf.Clamp(lastRate + Time.deltaTime, 0, float.MaxValue);
    }

    public override void Fire(Vector3 dir)
    {
        if(fireAble && lastRate > fireRate)
        {
            bulletCount--;
            Bullet bullet = Instantiate(base.bullet, firePos.position, transform.rotation).GetComponent<Bullet>();
            bullet.Fire(bulletSpeed, bulletDamage);

            if(bulletCount < 1)
            {
                Reroad();
            }
        }
    }

    public override void Reroad()
    {
        StartCoroutine(Reroading());
    }

    private IEnumerator Reroading()
    {
        fireAble = false;
        yield return new WaitForSeconds(reroadTime);
        fireAble = true;
    }
}
