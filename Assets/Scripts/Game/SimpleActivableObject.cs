using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActivableObject : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if(this?.gameObject != null)
            Destroy(gameObject != null? gameObject : null);
    }
}
