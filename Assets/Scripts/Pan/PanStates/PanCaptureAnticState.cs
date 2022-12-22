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

    // 시간 체크해서 애니메이션 끝나면 무조건 CapturedNone이나 CapturedRoll로 가도록
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
    /// 쿨타임이 찼는지 확인
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
        // Capturable과 Capture Point 사이에 벽이 있는지 체크
    }
}
