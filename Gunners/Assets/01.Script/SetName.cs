using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetName : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitUntil(() => EnemyDummy.Instance != null);
        yield return new WaitUntil(() => EnemyDummy.Instance.nickname != "");

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = text.text + EnemyDummy.Instance.nickname;
    }
}
