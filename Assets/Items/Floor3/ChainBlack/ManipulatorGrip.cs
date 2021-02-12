using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatorGrip : MonoBehaviour, IActivatable
{
    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float DPS;
    [SerializeField] private float moveSmoothness;
    [SerializeField] private Transform part1T;
    [SerializeField] private float gripAttackDistance;
    [SerializeField] private Animator gripAnimator;
    [SerializeField] private ParticleSystem attackEffect;
    private IAtackable playerAttackable;
    private Transform playerT;
    private Transform myT;
    private Transform grip;
    private bool isActive;
    void Start()
    {
        isActive = false;
        myT = transform;
        playerT = FindObjectOfType<PlayerController>().transform;
        grip = myT.GetChild(0);
        playerAttackable = FindObjectOfType<PlayerController>().GetComponent<IAtackable>();
        attackEffect = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (!isActive) return;
        float distnaceToPlayer = Vector3.Distance(part1T.position, playerT.position);
        Vector3 moveV;
        if(distnaceToPlayer < maxDistanceToPlayer)
        {
            moveV = Vector3.Lerp(myT.position, playerT.position, moveSmoothness * Time.deltaTime);
        }
        else
        {
            moveV = Vector3.Lerp(myT.position, part1T.position, moveSmoothness * Time.deltaTime);
        }
        var distanceVec = playerT.position - myT.position;
        myT.up = -distanceVec;
        if(distanceVec.magnitude < gripAttackDistance)
        {
            Attack();
            if (!attackEffect.gameObject.activeSelf)
                attackEffect.gameObject.SetActive(true);
        }
        else
        {
            if (attackEffect.gameObject.activeSelf)
                attackEffect.gameObject.SetActive(false);
        }
        distanceVec.z = 0;

        myT.position = moveV;
        gripAnimator.SetFloat("distance", distanceVec.magnitude);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, maxDistanceToPlayer);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gripAttackDistance);
    }

    private void Attack()
    {
        playerAttackable.GetDamage(DPS * Time.deltaTime);
    }
    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
