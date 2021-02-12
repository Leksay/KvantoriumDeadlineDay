using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance;
    private static List<IPausable> pauseables = new List<IPausable>();
    public static bool Stopped;

    private void Awake()
    {
        pauseables = new List<IPausable>();
    }
    void Start()
    {
        if (instance == null) instance = this;
        Resume();
    }

    public static void Pause()
    {
        Physics2D.autoSimulation = false;
        Time.timeScale = 0;
        pauseables.ForEach((p) => p.SetPause());
    }
    
    public static void Resume()
    {
        Physics2D.autoSimulation = true;
        Time.timeScale = 1;
        pauseables.ForEach((p) => p.Resume());
    }

    public static void AddPausableObject(IPausable pausable)
    {
        pauseables.Add(pausable);
    }
}
