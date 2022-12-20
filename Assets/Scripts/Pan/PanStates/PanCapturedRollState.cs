using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCapturedRollState : PanCapturedState
{
    public PanCapturedRollState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Captured Roll");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(pan.PanningState);
        }
    }
}
