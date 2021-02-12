using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fanblade : MonoBehaviour
{
    [Range(0,1200)]
    [SerializeField] private float speed;
    private Transform myT;
    void Start()
    {
        myT = transform;
    }


    void Update()
    {
        myT.localEulerAngles += Vector3.forward * speed * Time.deltaTime;
    }
}
