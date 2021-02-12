using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NanoChemical : MonoBehaviour, IAtackable
{
    [Range(1,15)]
    [SerializeField] private float downForce;
    public enum TypeOfChemical
    { 
        Positive,
        Negative
    }
    [SerializeField] public TypeOfChemical type;
    public void GetDamage(float damage)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.gravityScale = 1;
    }
}
