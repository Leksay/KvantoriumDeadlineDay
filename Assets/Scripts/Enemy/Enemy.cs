using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour, IAtackable
{
    [SerializeField] protected float health;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] protected float timeToDeath = 1.0f;
    
    [HideInInspector] public Transform player;
    protected Transform myTransform; 
    protected bool isAlive;
    protected float maxHealth;

    [SerializeField] protected EnemyHealthBar enemyBar;

    protected virtual void Start()
    {
        maxHealth = health;
        myTransform = transform;
        player = FindObjectOfType<PlayerController>().transform;
        isAlive = true;
        enemyBar = myTransform.GetComponentInChildren<EnemyHealthBar>();
        gameObject.layer = LayerMask.NameToLayer("Walkable");
    }

    public virtual void GetDamage(float damage)
    {
        health -= damage;
        enemyBar.SetBarValuePercent(health/maxHealth);
        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Die()
    {
        isAlive = true;
        Destroy(this?.gameObject,timeToDeath);
    }

}
