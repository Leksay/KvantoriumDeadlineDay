using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inviziblelatform : MonoBehaviour
{
    private Collider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }
    //void OnTriggerEnter2D(Collider2D innerCollider)
    //{
    //    var rb = innerCollider.GetComponentInParent<Rigidbody2D>();
    //    var cheker = innerCollider.GetComponent<GroundCheker>();
    //    if (rb != null && rb.velocity.y < 0 && cheker!= null)
    //    {
    //        myCollider.isTrigger = false;
    //    }
    //}

    public void SetSolid(bool solid)
    {
        myCollider.isTrigger = !solid;
    }

    void OnCollisionExit2D(Collision2D outerCollision)
    {
        var rb = outerCollision.transform.GetComponent<PlayerController>();
        if (rb != null)
        {
            myCollider.isTrigger = true;
        }
    }
}
