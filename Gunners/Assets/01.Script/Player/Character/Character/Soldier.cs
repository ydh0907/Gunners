using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : ICharacter
{
    private void Awake()
    {
        base.Awake();
        maxHp = 150;
        hp = maxHp;
        armor = 40;
        speed = 7;
    }
}
