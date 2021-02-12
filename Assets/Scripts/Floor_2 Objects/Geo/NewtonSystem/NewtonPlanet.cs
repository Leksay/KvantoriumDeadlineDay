using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class NewtonPlanet : MonoBehaviour,IAtackable
{
    private NewtonSystem system;
    public Rigidbody2D myRB;
    public Collider2D myCollider;
    public float xDirection;
    public bool checkingPlanet;
    private void Awake()
    {
        system = GetComponentInParent<NewtonSystem>();
        myRB = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        checkingPlanet = false;
    }
    private void Start()
    {
        system.AddPlanet(this);
    }

    public void SetDirection(float xDir)
    {
        xDirection = xDir;
    }

    public void GetDamage(float damage)
    {
        system.AttackImpulse(myCollider, Mathf.Clamp(transform.position.x - PlayerController.instance.myTransform.position.x, -1, 1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(checkingPlanet && collision.transform.GetComponent<PassivePlanet>())
        {
            system.TriggerEnterManagament();
            checkingPlanet = false;
        }
    }
}
