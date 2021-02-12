using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRacing : MonoBehaviour, IActivatable
{
    [SerializeField] private int timeToStart = 3;
    [SerializeField] private Transform droneStartPoint;
    [SerializeField] private GameObject dronePrefab;
    private int timer;
    void Start()
    {
        timer = timeToStart;
        StartCoroutine(WaitAndLoop(1f));
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void StartDrone()
    {
        GameObject.Instantiate(dronePrefab, droneStartPoint.position, Quaternion.identity);
    }
    private void ChangeTime()
    {
        DroneRacingTMP.SetText(timer.ToString());
    }

    private IEnumerator WaitAndLoop(float time)
    {
        yield return new WaitForSeconds(time);
        timer--;
        ChangeTime();
        if (timer <= 0)
        {
            StartDrone();
            timer = timeToStart + 1;
        }
        StartCoroutine(WaitAndLoop(time));

    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
