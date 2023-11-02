using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : ICharacter
{
    private void Awake()
    {
        hp = 200;
        armor = 70;
        speed = 5;
    }
}
