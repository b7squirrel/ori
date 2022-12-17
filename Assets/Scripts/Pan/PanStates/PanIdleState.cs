using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanIdleState : PanReadyState
{
    bool captureInput;

    public PanIdleState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
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
        captureInput = pan.PanInputHandler.CaptureInput;
        if (captureInput && pan.CaptureAnticState.CheckIfCanCapture())
        {
            stateMachine.ChangeState(pan.CaptureAnticState);
        }
    }
}
