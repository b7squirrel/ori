using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public PanStateMachine StateMachine { get; private set; }
    public PanIdleState IdleState { get; private set; }
    public PanCaptureAnticState CaptureAnticState { get; private set; }
    public PanCapturedNoneState CapturedNoneState { get; private set; }
    public PanCapturedRollState CapturedRollState { get; private set; }
    public PanPanningState PanningState { get; private set; }
    public PanHitRollAnticState HitRollAnticState { get; private set; }
    public PanHitRollState HitRollState { get; private set; }

    [SerializeField] PanData panData;
    public Transform capturePoint;

    public PanInputHandler PanInputHandler { get; private set; }
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        PanInputHandler = GetComponent<PanInputHandler>();

        StateMachine = new PanStateMachine();

        IdleState = new PanIdleState(this, StateMachine, panData, "PanIdle");
        CaptureAnticState = new PanCaptureAnticState(this, StateMachine, panData, "PanCaptureAntic");
        CapturedNoneState = new PanCapturedNoneState(this, StateMachine, panData, "PanCapturedNone");
        CapturedRollState = new PanCapturedRollState(this, StateMachine, panData, "PanCapturedRoll");
        PanningState = new PanPanningState(this, StateMachine, panData, "PanPan");
        HitRollAnticState = new PanHitRollAnticState(this, StateMachine, panData, "PanHitRollAntic");
        HitRollState = new PanHitRollState(this, StateMachine, panData, "PanHitRoll");

    }
    void Start()
    {
        StateMachine.InitState(IdleState);
    }
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

}
