using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knopka_gvozd : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        var playerC = coll.GetComponent<PlayerController>();
        if (playerC != null)
        {
            playerC.GetDamage(damage);
        }
    }
}
