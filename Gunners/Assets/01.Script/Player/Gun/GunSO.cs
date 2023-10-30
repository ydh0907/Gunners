using System;
using UnityEngine;

[Serializable]
public class GunSO : ScriptableObject
{
    public float fireRate;
    public float bulletSpeed;
    public ushort bulletDamage;
    public Sprite gunSprite;
    public Sprite bulletSprite;
}
