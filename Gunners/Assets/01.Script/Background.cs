using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<Transform> backgrounds;

    private void Awake()
    {
        backgrounds.ForEach((b) => b.gameObject.SetActive(false));

        backgrounds[Random.Range(0, backgrounds.Count)].gameObject.SetActive(true);
    }
}
