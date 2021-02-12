using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0.001f,1f)]
    [SerializeField] private float somothness;
    [SerializeField] private Transform target;

    public Transform cameraT;
    void Start()
    {
        cameraT = Camera.main.transform;
        target = FindObjectOfType<CameraTarget>().transform;
    }

    void Update()
    {
        Vector3 newCameraPosition = target.position;
        newCameraPosition.z = cameraT.position.z;

        newCameraPosition = Vector3.Lerp(cameraT.position, newCameraPosition, somothness);
        cameraT.position = newCameraPosition;
    }
}
