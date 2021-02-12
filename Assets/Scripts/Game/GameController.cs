using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    void Start()
    {
        Camera.main.orthographicSize = 5f;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseSystem.Pause();
        }
    }
}
