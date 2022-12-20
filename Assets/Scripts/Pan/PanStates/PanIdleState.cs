using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanIdleState : PanReadyState
{
    public PanIdleState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }

}
