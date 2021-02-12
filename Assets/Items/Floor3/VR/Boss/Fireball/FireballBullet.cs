using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FireballBullet : MonoBehaviour
{
    private Transform myT;
    private float speed;
    private Vector3 direction;
    private float damage;
    private bool isActive;
    void Start()
    {
        myT = transform;
    }

    public void StartFireball(float speed, Vector3 direction, float damage)
    {
        isActive = true;
        this.speed = speed;
        this.direction = direction;
        direction.y = 0;
        direction.z = 0;
        this.direction = Vector3.right * direction.normalized.x;
        this.damage = damage;
    }
    
    void Update()
    {
        if (!isActive) return;
        myT.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            player.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
