using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : ICharacter
{
    private void Awake()
    {
        base.Awake();
        maxHp = 200;
        hp = maxHp;
        armor = 70;
        speed = 5;
    }
}
