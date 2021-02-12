using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingDrone : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject explosionPrefab;
    private Transform myT;
    private Vector3 direction;
    void Start()
    {
        myT = transform;
        direction = (FindObjectOfType<PlayerController>().transform.position - myT.position).normalized;
        Destroy(myT.gameObject,5f);
    }

    void Update()
    {
        myT.position += direction *  speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var pc = collision.transform.GetComponent<PlayerController>();
        if(pc != null)
        {
            pc.GetDamage(damage);
            Destroy(GameObject.Instantiate(explosionPrefab, myT.position, Quaternion.identity),1f);
            Destroy(myT.gameObject);
        }
    }
}
