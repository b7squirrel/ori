public class PanCapturedState : PanState
{
    bool captureInput;
    bool hitRollInput;

    #region Constructor
    public PanCapturedState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }


    #endregion

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ���ĵ���� �����ϸ� �Է��� ����
        if (canCancelAniamtion == false)
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
        if (!isAnimationFinished)
            return;

        if (PanManager.instance.NumberOfRolls == 0)
        {
            pan.StateMachine.ChangeState(pan.IdleState);
        }
        else
        {
            pan.StateMachine.ChangeState(pan.PanningState);
        }
    }
}
