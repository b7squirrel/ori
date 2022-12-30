public class PanCapturedState : PanState
{
    #region Constructor
    public PanCapturedState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
    #endregion

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished) // 애니메이션이 끝나고 나면
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
