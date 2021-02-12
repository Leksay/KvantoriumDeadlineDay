using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Planet : MonoBehaviour, IAtackable
{
    private bool    moveToSolar;
    private Transform myTransform;
    [SerializeField] private float smoothness;
    private void Start()
    {
        myTransform = transform;
    }
    public void GetDamage(float damage)
    {
        print("KEK");
        moveToSolar = true;
    }

    private void Update()
    {
    }


}
