using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor2_Activator : MonoBehaviour
{
    [SerializeField] private Transform floor2_object;

    public void ActivateFloor2()
    {
        floor2_object.gameObject.SetActive(true);
        FindObjectOfType<FloorManager>().NextFloor();
    }

    public void DeactiveFloor2()
    {
        floor2_object.gameObject.SetActive(false);
        FindObjectOfType<FloorManager>().NextFloor();
    }
}
