using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairway : MonoBehaviour
{
    private enum Floor
    {
        First,Second,Third
    }
    [SerializeField] private Transform stairTransform;
    [SerializeField] private Floor floor;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (floor == Floor.First)
        {
            FindObjectOfType<Floor2_Activator>().ActivateFloor2();
            floor = Floor.Second;
        }
        stairTransform.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
