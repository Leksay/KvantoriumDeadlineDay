using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOne : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform part2Transform;
    private Transform myTransform;

    [SerializeField] private LineRenderer parentLr;
    void Start()
    {
        myTransform = transform;
        if (parent == null) parent = myTransform.parent;
        parentLr = parent.GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        parentLr.SetPosition(1, myTransform.localPosition);
        parentLr.SetPosition(2, part2Transform.localPosition);
    }
}
