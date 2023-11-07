using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetButtonManager : MonoBehaviour
{
    public void Matching()
    {
        GameManager.Instance.StartMatching();
    }
}
