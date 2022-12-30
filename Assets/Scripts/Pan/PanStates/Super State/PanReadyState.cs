public class PanReadyState : PanState
{
    bool captureInput;
    bool hitRollInput;

    #region Constructor
    public PanReadyState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
    #endregion

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
