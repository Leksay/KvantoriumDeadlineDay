using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikAttackElement : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAtackable atackable = collision.GetComponent<IAtackable>();
        if (atackable != null)
        {
            atackable.GetDamage(damage);
        }
    }

    public void SetDatame(float damage)
    {
        this.damage = damage;
    }
}
