using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInfoManager : MonoBehaviour
{
    public static AgentInfoManager Instance;

    public Action OnValueChange = null;

    private GunSO gun;
    private CharacterSO character;

    public CharacterListSO CharacterList => GameManager.Instance.CharacterList;
    public GunListSO GunList => GameManager.Instance.GunList;

    public CharacterSO Character
    {
        get => Instance.character;
        set
        {
            Instance.character = value;
            OnValueChange.Invoke();
        }
    }

    public GunSO Gun
    {
        get => Instance.gun;
        set
        {
            Instance.gun = value;
            OnValueChange.Invoke();
        }
    }

    public void ChangeCharacter(int index)
    {
        Character = CharacterList.characters[index];
    }

    public void ChangeGun(int index)
    {
        Gun = GunList.guns[index];
    }
}
