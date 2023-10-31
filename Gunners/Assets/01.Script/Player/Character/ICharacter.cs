using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    public ushort hp { get; protected set; }
    public float armor { get; protected set; }
    public float speed { get; protected set; }

    public Action<ushort> onHit = null;

    public void SetHP(ushort hp)
    {
        this.hp = hp;
        onHit?.Invoke(hp);
    }
}
