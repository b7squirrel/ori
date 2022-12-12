using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    float dashTimeLeft;
    float lastDashTime = -100f;
    float defaultGravityScale;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        defaultGravityScale = playerData.defaultGravityScale;
    }

    public override void Enter()
    {
        base.Enter();
        dashTimeLeft = playerData.dashTime;
        player.SetVelocityX(playerData.dashVelocity * player.FacingDirection);
        player.SetVelocityY(0f);
        player.SetGravity(0f);
    }
    public override void Exit()
    {
        base.Exit();
        player.SetGravity(defaultGravityScale);
        lastDashTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (dashTimeLeft <= 0)
        {
            isAbilityDone = true;
        }
        else
        {
            dashTimeLeft -= Time.deltaTime;
        }
    }

    public bool CheckIfCanDash() => Time.time >= lastDashTime + playerData.dashCoolDown;
}
