using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ability State는 State만 상속하므로 이동, Flip의 기능이 없다. 
/// </summary>
public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone; // Ability 상태가 끝나는 시점
    bool isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
