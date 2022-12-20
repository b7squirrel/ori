using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    float dashTimeLeft;
    float lastDashTime = -100f; // 처음 시작 시에는 무조건 대시가 가능하도록
    float defaultGravityScale;
    float currentDashDirection;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        defaultGravityScale = playerData.defaultGravityScale;
    }

    public override void Enter()
    {
        base.Enter();
        dashTimeLeft = playerData.dashTime;
        
        player.SetVelocityY(0f);
        player.SetGravity(0f);

        currentDashDirection = player.InputHandler.MovementInput.x;
        
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocityX(0f);
        player.SetGravity(defaultGravityScale);
        lastDashTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        

        if (dashTimeLeft <= 0)
        {
            isAbilityDone = true;
            return;
        }

        if (currentDashDirection == 0f)
        {
            player.SetVelocityX(playerData.dashVelocity * player.FacingDirection); // Idle상태에서는 바라보는 방향으로 DashTurn
        }
        else
        {
            player.SetVelocityX(playerData.dashVelocity * currentDashDirection); // Run 상태에서는 이동 방향으로 DashTurn
        }
        dashTimeLeft -= Time.deltaTime;
    }

    public bool CheckIfCanDash() => Time.time >= lastDashTime + playerData.dashCoolDown;
}
