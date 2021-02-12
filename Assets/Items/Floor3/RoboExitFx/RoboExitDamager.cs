using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboExitDamager : MonoBehaviour
{
    private ParticleSystem damagePS;
    [SerializeField] private Transform damagePSTransform;
    [SerializeField] private float DPS;
    void Start()
    {
        damagePS = damagePSTransform.GetComponent<ParticleSystem>();
        damagePS.Stop();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var PC = collision.transform.GetComponent<PlayerController>();
        if (PC)
        {
            if (damagePS.isStopped)
                damagePS.Play();
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        var PC = collision.transform.GetComponent<PlayerController>();
        if(PC)
        {
            damagePSTransform.position = collision.contacts[0].point;
            PC.GetDamage(DPS * Time.deltaTime);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var PC = collision.transform.GetComponent<PlayerController>();
        if (PC) damagePS.Stop();
    }
}
