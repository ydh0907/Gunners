using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGun : MonoBehaviour
{
    [SerializeField] protected Transform bullet;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Animator ani;

    public bool fireAble { get; protected set; }
    public float fireRate { get; protected set; }
    public float lastRate { get; protected set; }
    public float fireSpray { get; protected set; }
    public float reroadTime { get; protected set; }
    public float bulletSpeed { get; protected set; }
    public ushort bulletCount { get; protected set; }
    public ushort bulletPellet { get; protected set; }
    public ushort bulletDamage { get; protected set; }
    public ushort bulletMaximum { get; protected set; }

    public abstract void Fire();
    public abstract void Reroad();
}
