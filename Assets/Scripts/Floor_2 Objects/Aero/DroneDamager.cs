using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDamager : MonoBehaviour
{
    [SerializeField] private float DPS;
    private float defaultDPS = 15;
    private void Start()
    {
        if(DPS <= 0)
        {
            DPS = defaultDPS;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var pc = collision.GetComponent<PlayerController>();
        if(pc != null)
        {
            pc.GetDamage(DPS * Time.deltaTime);
        }
    }
}
