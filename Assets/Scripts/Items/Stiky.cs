using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

public class Stiky : MonoBehaviour,IAtackable
{
    [SerializeField] private Vector2 rightUpCorner, leftDownCorner;
    [SerializeField] private float speed = 4;
    [SerializeField] private float health;
    [SerializeField] private float DPS = 5;
    private Vector3 destination;
    private Vector3 toDestinationV;
    private Transform myTransform;
    void Start()
    {
        myTransform = transform;
        rightUpCorner = FindObjectOfType<Kovorking>().GetRightUp();
        leftDownCorner = FindObjectOfType<Kovorking>().GetLeftDown();
        GenerateNewDestination();
    }


    void Update()
    {
        MoveToDestination();
    }

    private void GenerateNewDestination()
    {
        float rndX = Random.Range(leftDownCorner.x, rightUpCorner.x);
        float rndY = Random.Range(leftDownCorner.y, rightUpCorner.y);
        destination = new Vector3(rndX, rndY, 0);
        toDestinationV = (destination - myTransform.position).normalized;
    }
    private void MoveToDestination()
    {
        myTransform.position += toDestinationV * speed * Time.deltaTime;
        float toDestDistance = (myTransform.position - destination).magnitude;
        if (toDestDistance < 1f)
        {
            GenerateNewDestination();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var playerC = coll.GetComponent<PlayerController>();
        if (playerC != null)
        {
            playerC.GetDamage(DPS);
        }
    }
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
