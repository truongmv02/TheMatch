using Game.CoreSystem;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{

    private int xInput, yInput;
    protected bool jumpInput;

    protected Vector2 detectedPos;
    protected Vector2 cornerPos;
    protected Vector2 startPos;
    protected Vector2 stopPos;
    protected Vector2 workspace;

    protected bool isHanging;
    protected bool isClimbing;
    protected bool isTouchingCeiling;

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (Movement.FacingDirection * playerData.startOffset.x),
                     cornerPos.y - playerData.startOffset.y);

        stopPos.Set(cornerPos.x + (Movement.FacingDirection * playerData.stopOffset.x),
                    cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            /* if (isTouchingCeiling)
             {
                 stateMachine.ChangeState(player.CrouchIdleState);
             }
             else*/
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;

            jumpInput = player.InputHandler.JumpInput;

            Movement?.SetVelocityZero();
            player.transform.position = startPos;

            if (xInput == Movement?.FacingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("ClimbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                player.WallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.WallJumpState);
            }
        }

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("ClimbLedge", false);
    }

    public void SetDetectedPosition(Vector2 pos) => this.detectedPos = pos;

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * Movement.FacingDirection * 0.015f),
                                            Vector2.up, playerData.standColliderHeight, CollisionSenses.GroundLayer);

        player.Anim.SetBool("IsTouchingCeiling", isTouchingCeiling);
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection,
                            CollisionSenses.WallCheckDistance, CollisionSenses.GroundLayer);

        float xDist = xHit.distance;

        workspace.Set((xDist + 0.015f) * Movement.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down,
                                              CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f,
                                              CollisionSenses.GroundLayer);

        float yDist = yHit.distance;

        workspace.Set(CollisionSenses.WallCheck.position.x + (xDist * Movement.FacingDirection),
                      CollisionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }
}