using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorWire : MonoBehaviour
{
    [SerializeField] private Material wireMaterial;
    [SerializeField] private float width;
    private Transform myT;
    private List<Transform> wirePoints;
    private LineRenderer wire;

    void Start()
    {
        myT = transform;
        wirePoints = new List<Transform>();
        wirePoints.AddRange(myT.GetComponentsInChildren<Transform>());
        wire = myT.gameObject.AddComponent<LineRenderer>();
        wire.useWorldSpace = true;
        wire.material = wireMaterial;
        wire.positionCount = wirePoints.Count;
        wire.SetPosition(0, myT.position);
        wire.widthMultiplier = width;
        for(int i =1; i < wirePoints.Count; i++)
        {
            wire.SetPosition(i, wirePoints[i].position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        myT = transform;
        wirePoints = new List<Transform>();
        wirePoints.AddRange(myT.GetComponentsInChildren<Transform>());
        for(int i = 0; i < wirePoints.Count-1; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(wirePoints[i].position, wirePoints[i + 1].position);
        }
        
    }
}
