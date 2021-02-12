using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrBossFireballSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballStartPointParent;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private float fireballDamage;
    private Transform myT;
    private Transform playerT;
    private GameObject fireball;
    void Start()
    {
        myT = transform;
        playerT = FindObjectOfType<PlayerController>().transform;
    }

    public void CreateFireball()
    {
        if(fireball == null)
        {
            
            fireball = GameObject.Instantiate(fireballPrefab,
            fireballStartPointParent.position, Quaternion.identity, fireballStartPointParent);
        }
        
    }

    public void ReleaseFireball()
    {
        Vector3 dir = myT.forward;
        fireball.transform.parent = null;
        fireball.transform.up = Vector3.left * dir.x;
        fireball.GetComponent<FireballBullet>().StartFireball(fireballSpeed,dir, fireballDamage);
        Destroy(fireball, 2f);

    }
}
