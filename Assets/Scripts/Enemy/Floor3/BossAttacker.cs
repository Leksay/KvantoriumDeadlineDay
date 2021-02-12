using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BossAttacker : MonoBehaviour
{
    [Header("Attack Parametres")]

    [SerializeField] private Transform attackPoint;
    [SerializeField] private ContactFilter2D filter;
    private VRBoss boss;
    private CapsuleCollider2D capsuleCollider;
    private float damage => boss.damage;
    void Start()
    {
        capsuleCollider = attackPoint.GetComponent<CapsuleCollider2D>();
        boss = GetComponent<VRBoss>();
    }

    public void Attack()
    {
        Collider2D[] colliders = new Collider2D[1];
        if(capsuleCollider.OverlapCollider(filter, colliders) > 0)
        {
            PlayerController player = colliders[0].GetComponent<PlayerController>();
            if(player != null)
            {
                player.GetDamage(damage);
            }
        }
    }
}
