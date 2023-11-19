using GunnersServer.Packets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : IGun
{
    private void Awake()
    {
        fireAble = true;
        fireRate = 0.5f;
        lastRate = 0;
        fireSpray = 20;
        reroadTime = 0.5f;
        bulletSpeed = 24;
        bulletCount = 5;
        bulletPellet = 8;
        bulletDamage = 7;
        bulletMaximum = 5;
    }

    private void Update()
    {
        lastRate = Mathf.Clamp(lastRate + Time.deltaTime, 0, float.MaxValue);
    }

    public override void Fire()
    {
        if (fireAble && lastRate > fireRate)
        {
            bulletCount--;
            for (int i = 0; i < bulletPellet; i++)
            {
                Bullet bullet = Instantiate(
                    base.bullet,
                    firePos.position,
                    Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, Random.Range(-fireSpray, fireSpray)))).GetComponent<Bullet>();
                bullet.Fire(bulletSpeed, bulletDamage, dummy);
            }

            ani.SetTrigger("Fire");

            if (bulletCount < 1)
            {
                Reroad();
            }

            lastRate = 0f;

            if (!dummy)
                NetworkManager.Instance.Send(new C_FirePacket());
        }
    }

    public override void Reroad()
    {
        StartCoroutine(Reroading());
    }

    private IEnumerator Reroading()
    {
        fireAble = false;
        for (int i = 0; i < (bulletMaximum - bulletCount) * reroadTime; i++){
            yield return new WaitForSeconds(reroadTime);
        }
        bulletCount = bulletMaximum;
        fireAble = true;
    }
}
