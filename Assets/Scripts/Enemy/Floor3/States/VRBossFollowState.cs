using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBossFollowState : VRBossState
{
    public VRBossFollowState(VRBossFSM stateMachine, VRBoss boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Follow State");
        Boss.animator.SetFloat("speed", 1);
    }

    public override void Exit()
    {
        base.Exit();
        Boss.animator.SetFloat("speed", 0);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Boss.ToPlayerDistance <= Boss.attackDistance)
        {
            Debug.Log("Changing");
            bossFSM.ChangeState(Boss.AttackState);
         
        }
        else if(Boss.ToPlayerDistance >= Boss.attackDistance && Boss.nextFireballTime <= Time.time)
        {
            bossFSM.ChangeState(Boss.FireballState);
        }
    }

    public override void Update()
    {
        base.Update();
        bossFSM.boss.Move();
    }
}
