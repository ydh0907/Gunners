using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInfoManager : MonoBehaviour
{
    public static AgentInfoManager Instance;

    public Action OnValueChange = null;

    private GunSO gun = null;
    private CharacterSO character = null;

    public CharacterListSO CharacterList => GameManager.Instance.CharacterList;
    public GunListSO GunList => GameManager.Instance.GunList;

    public CharacterSO Character
    {
        get => Instance.character;
        set
        {
            if (Instance.character == null)
                Instance.character = CharacterList.characters[0];
            Instance.character = value;
            OnValueChange?.Invoke();
        }
    }

    public GunSO Gun
    {
        get => Instance.gun;
        set
        {
            if (Instance.gun == null)
                Instance.gun = GunList.guns[0];
            Instance.gun = value;
            OnValueChange?.Invoke();
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
