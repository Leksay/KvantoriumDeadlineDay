using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossAttacker))]
[RequireComponent(typeof(BossMover))]
[RequireComponent(typeof(VrBossFireballSpawner))]
public class VRBoss : Enemy
{

    [Header("FSM")]
    private VRBossFSM stateMachine;
    public VRBossIdleState IdleState;
    public VRBossFollowState FollowState;
    public VRBossAttackState AttackState;
    public VRBossFireballState FireballState;
   
    [Header("Movement")]
    [HideInInspector] public BossMover mover;

    [Header("Fighting")]
    public float attackDistance;
    public bool followTarget;
    private BossAttacker attacker;

    [Header("Fireball")]
    [HideInInspector] public float nextFireballTime;
    [SerializeField] private float fireballReloadTime;
    private VrBossFireballSpawner fireballSpawner;
    public float fireballReleaseTime = 2;
    public float fireballAnimationTime = 3.37f;

    [HideInInspector] public Animator animator;
    [HideInInspector] public float ToPlayerDistance => mover.GetToPlayerDistance();

    [Header("Doors")]
    [SerializeField] private GameObject[] Doors;
    private void Awake()
    {
        stateMachine = new VRBossFSM(this);
        IdleState = new VRBossIdleState(this,stateMachine);
        FollowState = new VRBossFollowState(stateMachine, this);
        AttackState = new VRBossAttackState(stateMachine, this);
        FireballState = new VRBossFireballState(stateMachine, this);
        stateMachine.Initialize(IdleState);
        mover = GetComponent<BossMover>();
        followTarget = false;
        animator = GetComponent<Animator>();
        attacker = GetComponent<BossAttacker>();
        fireballSpawner = GetComponent<VrBossFireballSpawner>();
    }

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }
    public void ActivateBoss(Transform player)
    {
        this.player = player;
        followTarget = true;
        gameObject.SetActive(true);
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].SetActive(true);
        }
    }

    private void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
        stateMachine.CurrentState.Update();
    }

    public void Move()
    {
        mover.Move();
    }

    public void MakeAttack() => Attack();
    protected override void Attack()
    {
        attacker.Attack();
    }

    public void CreateFireball()
    {
        nextFireballTime = Time.time + fireballReloadTime;
        fireballSpawner.CreateFireball();
    }

    public void SetAnimatorTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void ReleaseFireball() => fireballSpawner.ReleaseFireball();
    public void RotateBoss() => mover.RotateBossToPlayer(0);

    public override void GetDamage(float damage)
    {
        Debug.Log("Boss Get Damage");
        base.GetDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float upDist = 0;
        for (int i = 0; i < 10; i++)
        {
            Gizmos.DrawLine(transform.position + Vector3.up * upDist, transform.position + Vector3.up * upDist + transform.forward * attackDistance);
            upDist += 0.2f;
        }

    }

    protected override void Die()
    {
        for(int i=0;i<Doors.Length;i++)
        {
            Doors[i].SetActive(false);
        }
        base.Die();
    }
}
