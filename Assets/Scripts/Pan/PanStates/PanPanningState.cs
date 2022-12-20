using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanPanningState : PanReadyState
{
    public PanPanningState(Pan pan, PanStateMachine stateMachine, PanData panData, string animBoolName) : base(pan, stateMachine, panData, animBoolName)
    {
    }
}
