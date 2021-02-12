using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoActivator : MonoBehaviour
{
    [SerializeField] private List<IActivatable> GetActivatables;
    [SerializeField] private List<GameObject> geoObjects;

    private void Start()
    {
        geoObjects.ForEach((go) => go.SetActive(false));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            geoObjects.ForEach((go) => go.GetComponent<IActivatable>().Activate());
            gameObject.SetActive(false);
        }
    }
}
