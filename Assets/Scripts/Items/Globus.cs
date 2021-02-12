using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globus : MonoBehaviour
{
    [SerializeField] private float damage;
    void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.GetDamage(damage);
        }
    }
}
