using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCaptureAnticState : PanState
{
    float lastCaptureTime;
    float captureTimeLeft;

    bool capturedRoll;
    public PanCaptureAnticState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    // �ð� üũ�ؼ� �ִϸ��̼� ������ ������ CapturedNone�̳� CapturedRoll�� ������
    public override void Enter()
    {
        base.Enter();
        lastCaptureTime = Time.time;
        captureTimeLeft = pandata.panCaptureTime;
        DetectCapturable();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (captureTimeLeft > 0)
        {
            captureTimeLeft -= Time.deltaTime;
            return;
        }
        if (capturedRoll)
        {
            pan.StateMachine.ChangeState(pan.CapturedRollState);
            
        }else
        {
            pan.StateMachine.ChangeState(pan.CapturedNoneState);
        }
    }

    /// <summary>
    /// ��Ÿ���� á���� Ȯ��
    /// </summary>
    public bool CheckIfCanCapture() => Time.time >= lastCaptureTime + pandata.panCaptureCoolTime;

    void DetectCapturable()
    {
        Collider2D hit = Physics2D.OverlapCircle(pan.capturePoint.position, 1f, pandata.whatIsCapturable);
        if (hit != null)
        {
            capturedRoll = true;
            hit.gameObject.GetComponent<ICapturable>().GetRolled(pan.CaptureSlot);
        }
        else
        {
            capturedRoll = false;
        }
    }
    void CheckWall()
    {
        // Capturable�� Capture Point ���̿� ���� �ִ��� üũ
    }
}
