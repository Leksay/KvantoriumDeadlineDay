using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairwayGravitySetupper : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}

