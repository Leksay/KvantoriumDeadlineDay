using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoSputnikCross : MonoBehaviour, IActivatable
{
    private enum SputnikCrossState { Activating, Follow, Focus};
    [SerializeField] private SputnikCrossState state;

    private Transform myTransform;
    private Transform target;
    private SputnikLaser laser;
    

    [Header("Sputnik")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float shootDistance;

    [Header("Size")]
    [SerializeField] private float activateStartSize;
    [SerializeField] private float focusSize;
    [SerializeField] private float avgDifference;
    [SerializeField] private float activateSpeed;
    [SerializeField] private float focusSpeed;


    private float size;
    
    public void Activate()
    {
        myTransform = transform;
        target = FindObjectOfType<PlayerController>().transform;
        state = SputnikCrossState.Activating;
        size = myTransform.localScale.x;
        myTransform.localScale = activateStartSize * Vector3.one;
        laser = myTransform.GetComponentInChildren<SputnikLaser>();
        laser.Activate();
        gameObject.SetActive(true);
        shootDistance *= shootDistance;
    }

    public void Deactivate()
    {
        this.enabled = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(state == SputnikCrossState.Activating)
        {
            if(myTransform.localScale.x < size )
            {
                myTransform.localScale += activateSpeed * Vector3.one * Time.deltaTime;
            }
            else
            {
                state = SputnikCrossState.Follow;
            }
        }
        else if(state == SputnikCrossState.Follow)
        {
            FollowTarget();
        }
        else if(state == SputnikCrossState.Focus)
        {
            Focus();
        }
    }

    private void Focus()
    {
        if (myTransform.localScale.x >= focusSize)
        {
            myTransform.localScale -= focusSpeed * Vector3.one * Time.deltaTime;
            if((myTransform.position - target.position).sqrMagnitude > avgDifference)
            {
                state = SputnikCrossState.Activating;
            }
        }
        else
        {
            if((myTransform.position - target.position).sqrMagnitude <= avgDifference)
            {
                Attack();
                state = SputnikCrossState.Activating;
            }
        }
    }

    private void Attack()
    {
        laser.StartAttack();
    }
    private void FollowTarget()
    {
        Vector3 direction = target.position - myTransform.position;
        myTransform.position += direction.normalized * speed * Time.deltaTime;
        if (direction.sqrMagnitude < shootDistance)
        {
            state = SputnikCrossState.Focus;
        }
    }
}
