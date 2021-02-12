using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{
    public AbstractFSMState CurrentState;
    public virtual void Initialize(AbstractFSMState state)
    {
        if(CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = state;
        CurrentState.Enter();
    }

    public virtual void ChangeState(AbstractFSMState state)
    {
        if(CurrentState != null)
        {
            CurrentState.Exit();
        }
        else
        {
            throw new System.Exception("Pass null state");
        }
        CurrentState = state;
        CurrentState.Enter();
    }
}
