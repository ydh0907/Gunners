using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGun : MonoBehaviour
{
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform firePos;

    public bool fireAble { get; protected set; }
    public float fireRate { get; protected set; }
    public float lastRate { get; protected set; }
    public float reroadTime { get; protected set; }
    public float bulletSpeed { get; protected set; }
    public ushort bulletCount { get; protected set; }
    public ushort bulletPellet { get; protected set; }
    public ushort bulletDamage { get; protected set; }
    public ushort bulletMaximum { get; protected set; }

    public abstract void Fire(Vector3 dir);
    public abstract void Reroad();
}
