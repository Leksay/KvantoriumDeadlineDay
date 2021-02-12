using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBossFSM :StateMachine
{
    public VRBoss boss;
    
    public VRBossFSM(VRBoss boss): base()
    {
        this.boss = boss;
    }
}
