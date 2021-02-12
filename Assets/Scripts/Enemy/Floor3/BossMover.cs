using UnityEngine;

[RequireComponent(typeof(VRBoss))]
public class BossMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform floor;
    [Header("Raycast")]
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask groundMask;

    private float speed => boss.speed;
    private Animator animator;
    private Vector3 toPlayerVector;
    private const float gravity = -9.81f;
    private VRBoss boss;
    private Transform myT;


    private Vector3 toLeftRotation = new Vector3(0, 270, 0);
    private Vector3 toRightRotation = new Vector3(0, 90, 0);
    private void Awake()
    {
        boss = GetComponent<VRBoss>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        myT = transform;
    }

    public void Move()
    {
        myT.position += NextMovePoint();
    }
    private Vector3 NextMovePoint()
    {
        toPlayerVector.x = 0;
        toPlayerVector.x = boss.player.position.x - myT.position.x;
        RotateBossToPlayer(toPlayerVector.x);
        toPlayerVector.x = Mathf.Clamp(toPlayerVector.x, -1, 1) * speed * Time.deltaTime;
        if(ToFloorDistance() > raycastDistance)
        {
            toPlayerVector.y += gravity * Time.deltaTime * Time.deltaTime;
        }
        else
        {
            toPlayerVector.y = 0;
        }
        return toPlayerVector;
    }

    private float ToFloorDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, Vector2.down, raycastDistance, groundMask);
        return ((Vector2)raycastPoint.position - hit.point).magnitude;
    }

    public void RotateBossToPlayer(float x)
    {
        if(x == 0)
        {
            x = boss.player.position.x - myT.position.x;
        }
        else
        {
            if(x > 0)
            {
                myT.localEulerAngles = toRightRotation;
            }
            else
            {
                myT.localEulerAngles = toLeftRotation;
            }
        }
    }

    public float GetToPlayerDistance()
    {
        return Mathf.Abs(boss.player.position.x - myT.position.x); 
    }
    private void OnDrawGizmosSelected()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, Vector2.down, raycastDistance, groundMask);
        if(hit.point != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(raycastPoint.position, hit.point);
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(raycastPoint.position, raycastPoint.position+Vector3.down * raycastDistance);
        }
    }
}
