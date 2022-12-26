using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCapturedState : PanState
{
    public PanCapturedState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
}
