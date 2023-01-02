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

        // 모션캔슬이 가능하면 입력을 받음
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

        // 애니메이션이 끝났다면 롤의 유무에 따라 상태 결정
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
