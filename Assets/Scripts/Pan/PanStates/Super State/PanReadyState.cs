using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanReadyState : PanState
{
    bool captureInput;
    bool hitRollInput;

    public PanReadyState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        hitRollInput = pan.PanInputHandler.HitRollInput;

        if (captureInput && pan.CaptureAnticState.CheckIfCanCapture())
            stateMachine.ChangeState(pan.CaptureAnticState);

        if (hitRollInput && pan.HitRollAnticState.CheckIfCanHitRoll())
        {
            if (PanManager.instance.NumberOfRolls == 0)
                return;

            stateMachine.ChangeState(pan.HitRollAnticState);
        }
    }
}
