using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGunImage : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        AgentInfoManager.Instance.OnValueChange += SetImage;
    }

    private void OnDisable()
    {
        AgentInfoManager.Instance.OnValueChange -= SetImage;
    }

    private void SetImage(CharacterSO character, GunSO gun)
    {
        image.sprite = gun.sprite;
    }
}
