using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(BoxCollider2D))]
public class Activator : MonoBehaviour
{
    [SerializeField] private List<GameObject> activableObjects;
    [SerializeField] protected bool isActivated;
    [SerializeField] protected bool activatedOneTime;
    [SerializeField] private List<IActivatable> activatables;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        isActivated = false;
        if(activableObjects.Count > 0)
        {
            activatables = new List<IActivatable>(activableObjects.Select((a)=>a.GetComponent<IActivatable>()));
        }
        Debug.Log(activatables.Count);
    }
    public void ActivateAll() => activatables.ForEach((a)=> a.Activate());
    public void DeactivateAll() => activatables.ForEach((a)=> a.Deactivate()); 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(activatedOneTime && isActivated) 
        {
            gameObject.SetActive(false);
            return;
        }
        if(other.GetComponent<PlayerController>())
        {
            isActivated = true;
            ActivateAll();
        }
    }
}

