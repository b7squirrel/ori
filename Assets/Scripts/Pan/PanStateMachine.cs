using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanStateMachine
{
    public PanState CurrentState { get; private set; }
    public void InitState(PanState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }
    public void ChangeState(PanState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
