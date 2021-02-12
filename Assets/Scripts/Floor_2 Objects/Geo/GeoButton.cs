using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoButton : MonoBehaviour
{
    [SerializeField] private GeoSputnikCross sputnik;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<NewtonPlanet>())
        {
            animator.SetBool("active", false);
            sputnik.Deactivate();
        }
    }
}
