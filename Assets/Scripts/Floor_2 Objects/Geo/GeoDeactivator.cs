using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoDeactivator : MonoBehaviour
{
    [SerializeField] private List<IActivatable> GetActivatables;
    [SerializeField] private List<GameObject> geoObjects;
    [SerializeField] private List<GameObject> aeroObjects;

    private void Start()
    {
        geoObjects.ForEach((go) => go.SetActive(false));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() && Vector3.Dot(Vector3.left,(collision.transform.position - transform.position).normalized) > 0)
        {
            geoObjects.ForEach((go) => go.GetComponent<IActivatable>().Deactivate());
            //GetComponent<BoxCollider2D>().isTrigger = false;
            aeroObjects.ForEach((go) => go.GetComponent<IActivatable>()?.Activate());
        }
    }
}
