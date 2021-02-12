using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour, IPausable
{
    public void OnStart()
    {
        PauseSystem.AddPausableObject(this);
    }

    public void Resume()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetPause()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Start()
    {
        OnStart();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
