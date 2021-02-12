using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHorizontal : MonoBehaviour, IPausable
{


    [SerializeField] private Transform myTransform;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float DPS;

    [SerializeField] private Transform left, right;

    private float leftX, rightX;
    private int rightI;

    private bool stopped;
    void Start()
    {
        myTransform = transform;
        leftX = left.position.x;
        rightX = right.position.x;
        rightI = 1;
        stopped = false;
        OnStart();
    }


    void Update()
    {
        if (stopped) return;
        if (rightI == 1 && myTransform.position.x > rightX)
        {
            rightI = -1;
        }
        if (rightI == -1 && myTransform.position.x < leftX)
        {
            rightI = 1;
        }

        myTransform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        myTransform.position += Vector3.right * rightI * speed * Time.deltaTime;

        ChekPlayer();
    }

    private void ChekPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(myTransform.position, attackRange);
        foreach (var collider in colliders)
        {
            var atackable = collider.GetComponent<IAtackable>();
            if (atackable != null)
            {
                atackable.GetDamage(DPS * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SetPause()
    {
        stopped = true;
    }

    public void Resume()
    {
        stopped = false;
    }

    public void OnStart()
    {
        PauseSystem.AddPausableObject(this);
    }
}