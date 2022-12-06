using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }

    [SerializeField] PlayerData playerData;
    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator anim { get; private set; }
    #endregion

    #region Check Transform
    [SerializeField] Transform groundCheck;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    #endregion

    #region Unity CallBack Functions
    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "PlayerIdle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "PlayerMove");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "PlayerJump");
        LandState = new PlayerLandState(this, StateMachine, playerData, "PlayerLand");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "PlayerFall");

        FacingDirection = 1;
    }

    void Start()
    {
        StateMachine.InitState(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float newVelocity)
    {
        CurrentVelocity = new Vector2(newVelocity, Rb.velocity.y);
        Rb.velocity = CurrentVelocity;
    }

    public void SetVelocityY(float newVelocity)
    {
        CurrentVelocity = new Vector2(Rb.velocity.x, newVelocity);
        Rb.velocity = CurrentVelocity;
    }
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public void CheckIfshouldFlip(Vector2 input)
    {
        int xInput = (int)input.normalized.x;
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
}
