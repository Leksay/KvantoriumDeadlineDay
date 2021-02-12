using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy, IPausable
{
    [SerializeField] private float seekRadius;
    [SerializeField] private bool followPlayer;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float deadSize;
    [SerializeField] private GameObject explosion;

    private float startSize;
    private Transform target;
    private int right;
    private float rightX;
    private float leftX;
    private float nextAttackTime;
    private float sizeWhenDying;

    private Vector2 toPlayerV;
    private Animator animator;

    private bool stopped;
    protected override void Start()
    {
        base.Start();
        OnStart();
        right = 1;
        myTransform.eulerAngles = new Vector3(0, 180, 0);
        leftX = myTransform.position.x - seekRadius;
        rightX = myTransform.position.x + seekRadius;
        target = FindObjectOfType<PlayerController>()?.transform;
        nextAttackTime = Time.time;
        startSize = myTransform.localScale.x;
        animator = GetComponent<Animator>();
        sizeWhenDying = myTransform.localScale.x - deadSize;
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
            myTransform.eulerAngles = new Vector3(0, 180, 0);
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
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;
        }
        float distanceToPlayer = Vector2.Distance(target.position, myTransform.position);
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
        toPlayerV = (target.position - myTransform.position);
        if(toPlayerV.magnitude < attackRange * 1.5f)
        {
            Attack();
        }
        float x = toPlayerV.x;
        float rotateY = myTransform.eulerAngles.y;
        x = Mathf.Clamp(x, -1, 1);
        if (x > 0)
        {
            myTransform.eulerAngles = Vector3.up * 180f;
        }
        else
        {
            myTransform.eulerAngles = Vector3.zero;
        }

        myTransform.position += (Vector3)toPlayerV.normalized * speed * Time.deltaTime;
    }

    protected override void Attack()
    {
        if (Time.time > nextAttackTime && isAlive)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(myTransform.position,attackRange);
            foreach(Collider2D collider in colliders)
            {
                if(collider.tag == "Player")
                {
                    var playerController = collider.GetComponent<PlayerController>();
                    if(playerController != null)
                    {
                        playerController.GetDamage(damage);
                        nextAttackTime = Time.time + 1.0f / attackRate;
                    }
                }
            }
            
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        animator.SetTrigger("attacked");
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
        GameObject.Destroy(this.gameObject, 1f);
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Destroy(GameObject.Instantiate(explosion,myTransform.position,Quaternion.identity),1f);
        isAlive = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seekRadius);
        Gizmos.color = Color.red;
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
