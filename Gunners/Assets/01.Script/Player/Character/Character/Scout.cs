using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : ICharacter
{
    private void Awake()
    {
        maxHp = 100;
        hp = maxHp;
        armor = 20;
        speed = 10;
    }
}
