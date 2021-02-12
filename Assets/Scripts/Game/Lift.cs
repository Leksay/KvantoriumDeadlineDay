using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;

    void Start()
    {
        if(teleportPoint == null && transform.childCount > 0)
        {
            teleportPoint = transform.GetChild(0);
        }
    }

    void OnTriggerEnter2D(Collider2D inner)
    {
        if (inner.GetComponent<PlayerController>() != null)
        {
            inner.transform.position = teleportPoint.position;
            FindObjectOfType<FloorManager>().NextFloor();
        }
    }
}
