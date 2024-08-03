using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(playerData.movementVelocity * xInput);
        Movement?.CheckIfShouldFlip(xInput);
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                player.StateMachine.ChangeState(player.IdleState);
            }

        }
    }
}