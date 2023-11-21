using System;
using UnityEngine;

public abstract class IGun : MonoBehaviour
{
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform firePos;
    [SerializeField] public Animator ani;

    public bool dummy = false;
    public bool fireAble;
    public float fireRate;
    public float lastRate;
    public float fireSpray;
    public float reroadTime;
    public float bulletSpeed;
    public ushort bulletCount;
    public ushort bulletPellet;
    public ushort bulletDamage;
    public ushort bulletMaximum;
    public Action<float> OnReroad = null;

    public abstract void Fire();
    public abstract void Reroad();
}
