using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PlayerNanoSetup))]
[RequireComponent(typeof(CameraFollow))]
public class PlayerController : MonoBehaviour, IAtackable, IPausable
{
    public static PlayerController instance;
    public float speed;
    [SerializeField] private float health;
    [SerializeField] private float jumpForce;
    [SerializeField] private float damage;
    [SerializeField] public float radius;
    [SerializeField] public bool isGrounded;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRate;

    public CameraFollow cameraFollow;
    
    private float nextAttackTime;
    [HideInInspector] public Transform myTransform;
    private Rigidbody2D rb;
    private Animator animator;
    private MotivationBar motivationBar;
    private float maxHealth;
    private SpriteRenderer sr;

    private float animationTime;
    private float curAnimationTime;
    private bool animateDamage;
    private Color damageColor;
    [SerializeField] private bool startJumping;

    private bool stopped;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        maxHealth = health;
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nextAttackTime = Time.time;
        motivationBar = FindObjectOfType<MotivationBar>();
        sr = GetComponent<SpriteRenderer>();
        animationTime = 0.5f;
        cameraFollow = GetComponent<CameraFollow>();
        OnStart();
    }

    void Update()
    {
        if (stopped) return;

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0 && myTransform.eulerAngles.y == 180f)
        {
            myTransform.eulerAngles = Vector3.zero;
        }
        else if (h < 0 && myTransform.eulerAngles.y == 0)
        {
            myTransform.eulerAngles = new Vector3(0,180,0);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        animator.SetFloat("speed",Mathf.Abs(h * speed));

        if (Time.time > nextAttackTime && (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(KeyCode.O)))
        {
            //Attack();
        }

        if (animateDamage) AnimateDamage();
        myTransform.position += new Vector3(1,0,0) * speed * h * Time.deltaTime;
    }

    private void Jump()
    {
        if (startJumping || !isGrounded) return;
        animator.SetTrigger("jump");
        startJumping = true;
        StartCoroutine(WaitAndJump());
    }

    private IEnumerator WaitAndJump()
    {
        yield return new WaitForSeconds(0.1f);
        if(isGrounded && startJumping)
        {
            rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        startJumping = false;
    }
    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }

    public bool GetGrounded()
    {
        return isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,radius);
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        motivationBar.SetBarValuePercent(health / maxHealth);
        if (health < 0)
        {
            Deadline();
        }
        if (health > maxHealth)
            health = maxHealth;

        if (damage > 0)
        {
            damageColor = Color.red;
        }
        else
        {
            damageColor = Color.green;
        }

        curAnimationTime = 0.01f;
        sr.color = damageColor;
        AnimateDamage();
    }

    private void AnimateDamage()
    {
        animateDamage = true;
        Color tempColor = Color.Lerp(sr.color, Color.white, curAnimationTime);
        sr.color = tempColor;
        curAnimationTime += Time.deltaTime;
        if (curAnimationTime > animationTime)
        {
            animateDamage = false;
        }
    }
    private void Deadline()
    {
        SceneManager.LoadScene("Level_1");  
    }

    public void SetPause()
    {
        stopped = true;
        animator.speed = 0;
    }

    public void Resume()
    {
        stopped = false;
        animator.speed = 1;
    }

    public void OnStart()
    {
        PauseSystem.AddPausableObject(this);
    }
}
