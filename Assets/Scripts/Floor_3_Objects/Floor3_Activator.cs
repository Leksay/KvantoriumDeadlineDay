using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Floor3_Activator : MonoBehaviour
{
    [SerializeField] private float downColorMult;

    [SerializeField] private SpriteRenderer floor3_SR;
    [SerializeField] private GameObject floor3_platforms;
    [SerializeField] private GameObject floor3;
    [SerializeField] private GlobalActivatorFloor floor2GlobalActivator;
    private void Start()
    {
        if(SpawnPointPosition.instance.floor3Activated)
        {
            ActiveteFloor3();
        }
        else
        {
            DeactiveteFloor3();
        }
    }
    private void ActiveteFloor3()
    {
        floor3_SR.color = Color.white;
        floor2GlobalActivator.DeactivateAll();
        floor2GlobalActivator.TurnOffLights();
        foreach (var activable in floor3.transform.GetComponentsInChildren<IActivatable>())
        {
            activable.Activate();
        }

    }
    private void DeactiveteFloor3()
    {
        floor3_SR.color = Color.white * downColorMult;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ActiveteFloor3();
    }
}
