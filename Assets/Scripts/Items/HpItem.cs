using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : Item
{
    [SerializeField] private float hp;
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        var playerController = collider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.GetDamage(-hp);
            Destroy(this.gameObject);
        }
    }
}
