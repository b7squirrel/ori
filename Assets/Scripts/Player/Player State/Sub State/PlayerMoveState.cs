using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfshouldFlip(input);

        if (player.InputHandler.MovementInput.x != player.PlayerMouseDirection.GetMouseHorizontalDirection())
        {
            player.anim.SetFloat("RunFB", 0f);
        }
        else
        {
            player.anim.SetFloat("RunFB", 1f);
        }

        player.SetVelocityX(playerData.movementVelocity * input.x);

        if (input.x == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
