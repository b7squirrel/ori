using UnityEngine;

public class PanCaptureAnticState : PanState
{
    float lastCaptureTime;
    float captureTimeLeft;
    bool capturedRoll;

    #region Constructor
    public PanCaptureAnticState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
    #endregion

    // �ð� üũ�ؼ� �ִϸ��̼� ������ ������ CapturedNone�̳� CapturedRoll�� ������
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
        Collider2D[] hit = Physics2D.OverlapBoxAll(pan.capturePoint.position, new Vector2(2.5f, 1.3f), 0f, pandata.whatIsCapturable);
        if (hit != null)
        {
            foreach (var item in hit)
            {
                item.gameObject.GetComponent<ICapturable>().GetCaptured();
            }

            capturedRoll = true;
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
