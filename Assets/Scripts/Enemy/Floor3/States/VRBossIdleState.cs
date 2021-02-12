using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBossIdleState : VRBossState
{
    public VRBossIdleState(VRBoss boss, VRBossFSM stateMachine) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Boss.followTarget)
        {
            bossFSM.ChangeState(Boss.FollowState);
        }
    }
}
