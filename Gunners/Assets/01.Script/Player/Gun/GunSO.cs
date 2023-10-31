using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "SO/GunSO")]
public class GunSO : ScriptableObject
{
    public IGun gun;
}
