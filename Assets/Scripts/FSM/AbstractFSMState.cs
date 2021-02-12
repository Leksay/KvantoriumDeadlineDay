using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFSMState 
{
    public StateMachine StateMachine;
    public AbstractFSMState(StateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void HandleInput() { }
    public virtual void HandlePhysics() { }
    public virtual void LogicUpdate() { }
}
