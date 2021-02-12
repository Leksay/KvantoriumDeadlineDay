using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IAtackable
{
    [SerializeField] private GameObject explosionPS;
    [SerializeField] private List<GameObject> generatorDeactivateObjects;
    private bool isActive;
    public void GetDamage(float damage)
    {
        if(isActive)
        {
            isActive = false;
            DestroyGenerator();
        }
        
    }

    private void DestroyGenerator()
    {
        explosionPS.SetActive(true);
        explosionPS.GetComponent<ParticleSystem>().Play();
        foreach(var obj in generatorDeactivateObjects)
        {
            obj.GetComponentInChildren<IActivatable>()?.Deactivate();
            obj.GetComponent<IActivatable>()?.Deactivate();
        }
        Destroy(gameObject, 1f);
    }

    void Start()
    {
        isActive = true;
    }
}
