using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour, IActivatable
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    [SerializeField] private bool showGizmos;
    private float[] planetAngle;
    private Transform center;
    private Transform[] planets;
    void Start()
    {
        center = transform;
        planets = new Transform[center.childCount];
        planetAngle = new float[center.childCount];
        for (int i =0; i < center.childCount; i++)
        {
            planets[i] = center.GetChild(i);
            planetAngle[i] = i * 60;
        }
    }

    private void Update()
    {
        center.RotateAroundLocal(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if(showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
