using System;
using UnityEngine;

[Serializable]
public class CharacterSO : ScriptableObject
{
    public ushort hp;
    public float armor;
    public float speed;
    public Sprite sprite;
}
