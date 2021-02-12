using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChain : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private LineRenderer lr;
    private Transform gearObject;
    private Transform my;
    private float startDistance;
    private float endDistance;
    
    void Start()
    {
        gearObject = transform.GetChild(0);
        lr = GetComponent<LineRenderer>();
        startDistance = Vector3.Distance(gearObject.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(1, gearObject.localPosition);
    }
}
