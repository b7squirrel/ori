using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCapturedNoneState : PanCapturedState
{
    public PanCapturedNoneState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Captured None");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
