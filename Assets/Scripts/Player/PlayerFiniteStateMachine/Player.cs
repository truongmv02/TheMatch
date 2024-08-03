using Game.CoreSystem;
using Game.Weapons;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerAttackState TertiaryAttackState { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components

    public Core Core { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public SpriteRenderer SpriteSr { get; private set; }

    #endregion

    #region Other Variables
    private Vector2 workspace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    private Weapon tertiaryWeapon;
    public Stats Stats { get; private set; }


    #endregion


    // Methods ----------------------------------------------------------------


    #region Unity callback methods
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        tertiaryWeapon = transform.Find("TertiaryWeapon").GetComponent<Weapon>();
        SpriteSr = GetComponent<SpriteRenderer>();

        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);
        tertiaryWeapon.SetCore(Core);

        Stats = Core.GetCoreComponent<Stats>();

        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "InAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "WallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "WallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "WallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "InAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "LedgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "InAir");
        StunState = new PlayerStunState(this, StateMachine, playerData, "Stun");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", primaryWeapon, CombatInput.Primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", secondaryWeapon, CombatInput.Secondary);
        TertiaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", tertiaryWeapon, CombatInput.Tertiary);

    }

    private void Start()
    {

        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        //     DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        StateMachine.Initialize(IdleState);
        InputHandler = GetComponent<PlayerInputHandler>();
        Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
    }

    #endregion

    #region Other methods

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;
        MovementCollider.size = workspace;

        MovementCollider.offset = center;
    }

    private void HandlePoiseCurrentValueZero()
    {
        StateMachine.ChangeState(StunState);
    }

    public void ResetPlayer()
    {
        gameObject.SetActive(true);
        Stats.ResetStats();
    }


    private void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    #endregion

}