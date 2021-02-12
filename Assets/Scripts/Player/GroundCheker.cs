using UnityEngine;

public class GroundCheker : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance; // Grounded Distance
    [SerializeField] private LayerMask groundMask;
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        this.gameObject.tag = "GroundCheker";
    }

    
    void FixedUpdate()
    {
        if(ToFloorDistance() < raycastDistance)
        {
            controller.SetGrounded(true);
        }
        else
        {
            controller.SetGrounded(false);
        }
    }

    public float ToFloorDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, Vector2.down, raycastDistance, groundMask);
        if(hit)
        {
            var softPlatform = hit.transform.GetComponent<Inviziblelatform>();
            if(softPlatform)
            {
                softPlatform.SetSolid(true);
            }
        }
        return ((Vector2)raycastPoint.position - hit.point).magnitude;
    }


    private void OnDrawGizmosSelected()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, Vector2.down, raycastDistance, groundMask);
        if (hit.point != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(raycastPoint.position, hit.point);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastPoint.position, raycastPoint.position + Vector3.down * raycastDistance);
        }
    }
}
