using UnityEngine;

public class VRBossFireballState : VRBossState
{
    private float releaseTime;
    private bool released;
    private float endOfAnimatonTime;
    public VRBossFireballState(VRBossFSM stateMachine, VRBoss boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Fireball Staet");
        releaseTime = Time.time + Boss.fireballReleaseTime;
        endOfAnimatonTime = Time.time + Boss.fireballAnimationTime;
        released = false;
        Boss.CreateFireball();
        Boss.SetAnimatorTrigger("fireball");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!released && Time.time >= releaseTime)
        {
            Boss.ReleaseFireball();
            released = true;
        }
        else if(released && Time.time >= endOfAnimatonTime)
        {
            bossFSM.ChangeState(Boss.FollowState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
