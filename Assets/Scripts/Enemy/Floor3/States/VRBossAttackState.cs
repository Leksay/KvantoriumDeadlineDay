using UnityEngine;

public class VRBossAttackState : VRBossState
{
    public VRBossAttackState(VRBossFSM stateMachine, VRBoss boss) : base(stateMachine, boss)
    {}

    private float endAttackTime;
    private float attackLength = 2;

    private float callAttackTime;
    private float nextCallAttackTime;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("VRBoss Attack");
        Boss.animator.SetBool("closeAttack",true);
        endAttackTime = Time.time + attackLength;
    }

    public override void Exit()
    {
        Debug.Log("VRBoss Exit Attack");
        base.Exit();
        Boss.animator.SetBool("closeAttack", false);
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Boss.ToPlayerDistance > Boss.attackDistance && Time.time >= endAttackTime)
        {
            bossFSM.ChangeState(Boss.FollowState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
