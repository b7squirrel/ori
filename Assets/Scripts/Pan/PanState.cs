using UnityEngine;

public class PanState
{
    protected Pan pan;
    protected PanStateMachine stateMachine;
    protected PanData pandata;

    protected bool isAnimationFinished;

    protected float startTime;

    string animBoolName;

    public PanState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName)
    {
        this.pan = pan;
        this.stateMachine = stateMachine;
        this.pandata = panData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        pan.anim.Play(animBoolName);
        isAnimationFinished = false;
        startTime = Time.time;
    }
    public virtual void Exit()
    {

    }
    public virtual void LogicUpdate()
    {
        DoChecks();
    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
