using Game.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int inputIndex;
    private bool canInterrupt;
    private bool checkFlip;

    WeaponGenerator weaponGenerator;

    public bool CanTransitionToAttackState => weapon.CanEnterAttack;
    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Weapon weapon,
        CombatInput input
        ) : base(player, stateMachine, playerData, animBoolName)
    {
        weaponGenerator = weapon.GetComponent<WeaponGenerator>();
        this.weapon = weapon;
        inputIndex = (int)input;
        weapon.OnExit += ExitHandler;
        weapon.OnUseInput += HandleUseInput;
        weapon.EventHandler.OnFinish += HandleFinish;
        weapon.EventHandler.OnEnableInterrupt += HandleEnableInterrupt;
        weapon.EventHandler.OnFlipSetActive += HandleFlipSetActive;
    }

    private void HandleWeaponGenerating()
    {
        stateMachine.ChangeState(player.IdleState);
    }

    public override void Enter()
    {
        base.Enter();
        checkFlip = true;
        canInterrupt = false;
        weaponGenerator.OnWeaponGenerating += HandleWeaponGenerating;
        weapon.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        weaponGenerator.OnWeaponGenerating -= HandleWeaponGenerating;
        weapon.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var playerInputHandler = player.InputHandler;
        var xInput = playerInputHandler.NormInputX;
        var attackInputs = playerInputHandler.AttackInputs;

        weapon.CurrentInput = player.InputHandler.AttackInputs[inputIndex];

        if (checkFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }

        if (!canInterrupt) return;

        if (xInput != 0 || attackInputs[0] || attackInputs[1] || attackInputs[2])
        {
            isAbilityDone = true;
        }

    }

    private void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }

    private void HandleUseInput()
    {
        player.InputHandler.UseAttackInput(inputIndex);
    }

    private void ExitHandler()
    {
        player.InputHandler.UseAttackInput(inputIndex);
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

    private void HandleEnableInterrupt() => canInterrupt = true;

    private void HandleFinish()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

}