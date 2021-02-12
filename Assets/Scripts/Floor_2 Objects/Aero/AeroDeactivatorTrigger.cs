using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroDeactivatorTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> aeroObjects;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() && Vector3.Dot(Vector3.left, (collision.transform.position - transform.position).normalized) > 0)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            aeroObjects.ForEach((go) => go.GetComponent<IActivatable>()?.Deactivate());
        }
    }
}
