using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform left, right;
    [SerializeField] private float speed;
    [SerializeField] private float castDistance;
    [SerializeField] private Transform laserPoint;
    [SerializeField] private float DPS;

    private Transform myTransform;
    private float leftX, rightX;
    private int rightI;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        myTransform = transform;
        leftX = left.position.x;
        rightX = right.position.x;
        rightI = 1;
    }

    void Update()
    {
        if (rightI == 1 && myTransform.position.x > rightX)
        {
            rightI = -1;
        }
        if (rightI == -1 && myTransform.position.x < leftX)
        {
            rightI = 1;
        }
        myTransform.position += Vector3.right * rightI * speed * Time.deltaTime;

        RenderLine();
    }

    private void RenderLine()
    {
        RaycastHit2D endPoisition = RaycastEndPoint();
        laserPoint.position = endPoisition.point;
        Vector3[] positions = new Vector3[] {myTransform.position, laserPoint.position};
        var controller = endPoisition.transform.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.GetDamage(DPS * Time.deltaTime);
        }
        lineRenderer.SetPositions(positions);
    }

    private RaycastHit2D RaycastEndPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(myTransform.position, Vector2.down,castDistance);
        return hit;
    }
}
