using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Test : MonoBehaviour
{
    private void Start()
    {
        LoadSceneManager.Instance.LoadSceneAsync("GameScene", () => { });
    }
}
