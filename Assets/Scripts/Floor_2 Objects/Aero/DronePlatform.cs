using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlatform : MonoBehaviour
{
    private enum Direction
    {
        Horizontal, Vertical
    }
    [SerializeField] private Direction direction;
    [SerializeField] private Transform start, finish;
    [SerializeField] private float speed;
    
    private Vector3 dir;
    private Transform myT;
    
    void Start()
    {
        myT = transform;
        dir = direction == Direction.Horizontal ? Vector3.right : Vector3.up;
        if(start == null || finish == null)
        {
            start = myT.Find("start");
            finish = myT.Find("finish");
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        myT.position += dir * speed * Time.deltaTime;
        if(direction == Direction.Horizontal)
        {
            if (myT.position.x >= finish.position.x || myT.position.x <= start.position.x)
                dir *= -1;
        }
        else
        {
            if (myT.position.y >= finish.position.y || myT.position.y <= start.position.y)
                dir *= -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start.position, finish.position);
        Gizmos.DrawWireSphere(start.position, 0.1f);
        Gizmos.DrawWireSphere(finish.position, 0.1f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<PlayerController>())
        {
            collision.transform.SetParent(myT);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<PlayerController>())
        {
            collision.transform.SetParent(null);
        }
    }
}
