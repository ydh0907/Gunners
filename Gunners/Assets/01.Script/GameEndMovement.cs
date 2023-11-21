using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndMovement : MonoBehaviour
{
    private Camera cam;
    private bool follow = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameWin += Move;
        GameManager.Instance.onGameLose += Move;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameWin -= Move;
        GameManager.Instance.onGameLose -= Move;
    }

    private void Update()
    {
        if(follow) transform.position = Agent.Instance.transform.position + new Vector3(0, 0, -10);
    }

    private void Move()
    {
        transform.position = Agent.Instance.transform.position + new Vector3(0, 0, -10);
        cam.orthographicSize = 3;
        follow = true;
    }
}
