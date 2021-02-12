using System.Collections;
using UnityEngine;

public class RoboActivator : MonoBehaviour
{
    [SerializeField] private int robotCount;
    [SerializeField] private GameObject robotPrefab;

    private bool activated;

    void OnTriggerEnter2D(Collider2D inner)
    {
        if (!activated && inner.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(WaitAndCreate(0.25f));
            Destroy(this.gameObject,0.25f * robotCount);
            activated = true;
        }
    }

    private IEnumerator WaitAndCreate(float time)
    {
        for (int i = 0; i < robotCount; i++)
        {
            yield return new WaitForSeconds(time);
            GameObject.Instantiate(robotPrefab, transform.position, Quaternion.identity);
        }
    }
}
