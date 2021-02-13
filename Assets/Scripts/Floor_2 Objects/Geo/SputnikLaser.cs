using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SputnikLaser : MonoBehaviour
{
    [SerializeField]private Vector2 ceilingXFloorY;
    private Transform myTransform;
    private LineRenderer lr;
    private Transform crossTransform;

    private enum LaserState { Spread, Attack, Fade };
    [SerializeField] private LaserState state;

    [Header("Attack")]
    [SerializeField] private float attakTime;
    [SerializeField] private Transform attackElement;
    private float spreadSpeed;
    [Header("Effects")]
    [SerializeField] private GameObject spaceHolePrefab;

    Vector3 lrPos;
    public void Activate()
    {

        myTransform = transform;
        ceilingXFloorY.x = GameObject.Find("CeilingFloor2").transform.position.y;
        ceilingXFloorY.y = Floor_2Positioner.myY;
        crossTransform = myTransform.parent.transform;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        state = LaserState.Spread;
        CalculateSpreadSpeed();
    }

    private void CalculateSpreadSpeed()
    {
        spreadSpeed = Mathf.Abs(ceilingXFloorY.x - ceilingXFloorY.y) / attakTime;
    }

    private void Update()
    {
        if(state == LaserState.Spread)
        {
            lr.SetPosition(0, startPos);
            if (lrPos.y > ceilingXFloorY.y)
            {
                lrPos += Vector3.down * spreadSpeed * Time.deltaTime;
                lr.SetPosition(1, lrPos);
                attackElement.position = lrPos;
            }
            else
            {
                lrPos.y = ceilingXFloorY.y;
                lr.SetPosition(1, lrPos);
                lr.enabled = false;
                state = LaserState.Attack;
                attackElement.gameObject.SetActive(false);
            }
        }
    }
    private Vector3 startPos;
    public void StartAttack()
    {
        state = LaserState.Spread;
        startPos = crossTransform.position;
        startPos.y = ceilingXFloorY.x;
        lrPos = startPos;
        lr.SetPosition(0, lrPos);
        lr.SetPosition(1, lrPos);
        var hole = GameObject.Instantiate(spaceHolePrefab, lrPos, Quaternion.identity);
        Destroy(hole, 0.5f);
        lr.enabled = true;
        attackElement.gameObject.SetActive(true);
    }

    private void Attack()
    {
       
    }

    private void OnDrawGizmos()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, transform.parent.position);
    }
}
