using GunnersServer.Packets;
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
        fireSpray = 5f;
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

    public override void Fire()
    {
        if(fireAble && lastRate > fireRate)
        {
            bulletCount--;
            for(int i = 0; i < bulletPellet; i++)
            {
                Bullet bullet = Instantiate(
                    base.bullet, 
                    firePos.position, 
                    Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, Random.Range(-fireSpray, fireSpray)))).GetComponent<Bullet>();
                bullet.Fire(bulletSpeed, bulletDamage);
            }

            ani.SetTrigger("Fire");

            if (bulletCount < 1)
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
