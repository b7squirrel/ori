using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanHitRollState : PanState
{
    bool captureInput;
    bool hitRollInput;

    public PanHitRollState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PanManager.instance.ReleaseRoll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ���ĵ���� �����ϸ� �Է��� ����
        if (canCancelAniamtion)
        {
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

        // �ִϸ��̼��� �����ٸ� ���� ������ ���� ���� ����
        if (isAnimationFinished == false)
            return;

        if (PanManager.instance.NumberOfRolls == 0)
        {
            stateMachine.ChangeState(pan.IdleState);
        }
        else
        {
            stateMachine.ChangeState(pan.PanningState);
        }
    }
}
