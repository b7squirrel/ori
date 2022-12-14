using UnityEngine;

public class PanCaptureAnticState : PanState
{
    float lastCaptureTime;
    float captureTimeLeft;

    #region Constructor
    public PanCaptureAnticState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
    #endregion

    // 시간 체크해서 애니메이션 끝나면 무조건 CapturedNone이나 CapturedRoll로 가도록
    public override void Enter()
    {
        base.Enter();
        lastCaptureTime = Time.time;
        captureTimeLeft = pandata.panCaptureTime;
        DetectCapturable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (captureTimeLeft > 0)
        {
            captureTimeLeft -= Time.deltaTime;
            return;
        }
        if (PanManager.instance.NumberOfRolls == 0)
        {
            pan.StateMachine.ChangeState(pan.CapturedNoneState);
        }
        else
        {
            pan.StateMachine.ChangeState(pan.CapturedRollState);
        }
    }

    /// <summary>
    /// 쿨타임이 찼는지 확인
    /// </summary>
    public bool CheckIfCanCapture() => Time.time >= lastCaptureTime + pandata.panCaptureCoolTime;

    void DetectCapturable()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(pan.capturePoint.position, new Vector2(2.5f, 1.3f), 0f, pandata.whatIsCapturable);
        if (hit != null)
        {
            int index = Mathf.Min(hit.Length, 3);
            for (int i = 0; i < index; i++) // 3개까지만 캡쳐가 가능하도록
            {
                hit[i].gameObject.GetComponent<ICapturable>().GetCaptured();
            }
        }
    }
    void CheckWall()
    {
        // Capturable과 Capture Point 사이에 벽이 있는지 체크
    }
}
