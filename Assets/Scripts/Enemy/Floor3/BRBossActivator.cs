using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRBossActivator : MonoBehaviour
{
    [SerializeField] private VRBoss boss;
    [SerializeField] private Transform bossCheckPoint;

    private void Awake()
    {
        boss = FindObjectOfType<VRBoss>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>())
        {   
            SpawnPointPosition.instance.position = bossCheckPoint.position;
            SpawnPointPosition.instance.floor3Activated = true;
            boss.ActivateBoss(collision.transform);
            gameObject.SetActive(false);
        }
    }
}
