using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetButtonManager : MonoBehaviour
{
    [SerializeField] private Transform Loading;

    public void Matching()
    {
        Loading.gameObject.SetActive(true);
        GameManager.Instance.StartMatching();
    }
}
