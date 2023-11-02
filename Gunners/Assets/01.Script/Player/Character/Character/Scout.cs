using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : ICharacter
{
    private void Awake()
    {
        hp = 100;
        armor = 20;
        speed = 10;
    }
}
