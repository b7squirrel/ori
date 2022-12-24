using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanHitRollState : PanState
{

    public PanHitRollState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PanManager.instance.ReleaseRoll();
    }
}
