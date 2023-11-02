using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : ICharacter
{
    private void Awake()
    {
        hp = 150;
        armor = 40;
        speed = 7;
    }
}
