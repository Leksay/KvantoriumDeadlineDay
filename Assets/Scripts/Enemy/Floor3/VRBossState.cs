using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBossState : AbstractFSMState
{
    public VRBossFSM bossFSM;
    public VRBoss Boss;
    public VRBossState(VRBossFSM stateMachine, VRBoss boss) : base(stateMachine)
    {
        this.Boss = boss;
        this.bossFSM = stateMachine;
    }
}
