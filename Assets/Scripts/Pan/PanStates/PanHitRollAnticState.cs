using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanHitRollAnticState : PanState
{
    float lastHitRollTime;

    public PanHitRollAnticState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        lastHitRollTime = Time.time;
        Debug.Log("Entered Hit Roll Antic State");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {

        }
    }

    /// <summary>
    /// ��Ÿ���� á���� Ȯ��
    /// </summary>
    public bool CheckIfCanHitRoll() => Time.time >= lastHitRollTime + pandata.panCaptureCoolTime;

}
