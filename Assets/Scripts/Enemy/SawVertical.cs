using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawVertical : MonoBehaviour, IPausable
{


    [SerializeField]private Transform myTransform;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float DPS;

    [SerializeField] private Transform down, up;

    private float downY, upY;
    private int downI;

    private bool stopped;
    void Start()
    {
        myTransform = transform;
        downY = down.position.y;
        upY = up.position.y;
        downI = 1;
        DPS = 10;
        OnStart();
    }


    void Update()
    {
        if (stopped) return;
        if (downI == 1 && myTransform.position.y > upY)
        {
            downI = -1;
        }
        if (downI == -1 && myTransform.position.y < downY)
        {
            downI = 1;
        }

        myTransform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        myTransform.position += Vector3.up * downI * speed * Time.deltaTime;

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
        Gizmos.DrawWireSphere(transform.position,attackRange);
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
