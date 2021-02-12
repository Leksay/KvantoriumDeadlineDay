using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coronabot : Enemy, IPausable
{
    [SerializeField] private float seekRadius;
    [SerializeField] private bool followPlayer;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float deadSize;

    private float startSize;
    private int right;
    private float rightX;
    private float leftX;
    private float nextAttackTime;
    private float sizeWhenDying;

    private Animator animator;

    private bool stopped;
    protected override void Start()
    {
        base.Start();
        stopped = false;
        right = 1;
        myTransform.eulerAngles = new Vector3(0, 180, 0);
        leftX = myTransform.position.x - seekRadius;
        rightX = myTransform.position.x + seekRadius;
        nextAttackTime = Time.time;
        startSize = myTransform.localScale.x;
        animator = GetComponent<Animator>();
        sizeWhenDying = myTransform.localScale.x - deadSize;
        OnStart();
    }


    void Update()
    {
        if (stopped) return;
        if (!isAlive)
        {
            myTransform.localScale -= Vector3.one * sizeWhenDying * Time.deltaTime;
            return;
        }
        
        if (myTransform.position.x > rightX)
        {
            myTransform.eulerAngles = Vector3.zero;
            right = -1;
        }

        if (myTransform.position.x < leftX)
        {
            myTransform.eulerAngles = new Vector3(0,180,0);
            right = 1;
        }

        if (followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            myTransform.position += Vector3.right * speed * right * Time.deltaTime;
        }
        ChekPlayerDistance();
    }

    private void ChekPlayerDistance()
    {
        if (!isAlive)
        {
            return;
        }
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
        Vector3 distanceVector = (player.position - myTransform.position);
        distanceVector.z = 0;
        float distanceToPlayer = distanceVector.magnitude;
        if (distanceToPlayer < seekRadius)
        {
            followPlayer = true;
        }
        else
        {
            followPlayer = false;
        }
    }

    private void FollowPlayer()
    {
        if (!isAlive)
        {

            return;
        }
        Vector3 toPlayerV = (player.position - myTransform.position);
        toPlayerV.z = 0;
        if (toPlayerV.magnitude < attackRange)
        {
            Attack();
            return;
        }
        float x = toPlayerV.x;
        float rotateY = myTransform.eulerAngles.y;
        x = Mathf.Clamp(x,-1,1);
        if (x > 0 )
        {
            myTransform.eulerAngles = Vector3.up * 180f;
        }
        else
        {
            myTransform.eulerAngles = Vector3.zero;
        }

        myTransform.position += Vector3.right * x * speed * Time.deltaTime;
    }

    protected override void Attack()
    {
        if (Time.time > nextAttackTime && isAlive)
        {
            myTransform.localScale = Vector3.one * startSize * 1.2f;
            player.GetComponent<IAtackable>().GetDamage(damage);
            nextAttackTime = Time.time + 1.0f / attackRate;
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        animator.SetTrigger("attacked");
        //TODO: Анимация атаки
    }

    private void OnCollisionEnter2D(Collision2D innerCollision)
    {
        var playerController = innerCollision.transform.GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            Attack();
        }
    }
    protected override void Die()
    {
        base.Die();
        GameObject.Destroy(this.gameObject,1f);
        GetComponent<CircleCollider2D>().enabled = false;
        isAlive = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,seekRadius);
        Gizmos.color = Color.red;
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
