using System;
using Game.CoreSystem;
using UnityEngine;
namespace Game.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponDataSO Data { get; private set; }
        [SerializeField] private float attackCounterResetCooldown = 2f;

        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnUseInput;
        public event Action<bool> OnCurrentInputChange;

        public Core Core { get; private set; }

        public Animator Anim { get; private set; }
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public float AttackStartTime { get; private set; }
        private bool currentInput;
        private bool initDone;
        public bool CanEnterAttack { get; set; }
        private AnimationEventHandler eventHandler;
        public bool CurrentInput
        {
            get => currentInput;
            set
            {
                if (currentInput != value)
                {
                    currentInput = value;
                    OnCurrentInputChange?.Invoke(value);
                }
            }
        }
        public AnimationEventHandler EventHandler
        {
            get
            {
                if (!initDone)
                {
                    GetDependencies();
                }
                return eventHandler;
            }
            private set => eventHandler = value;
        }
        private TimeNotifier attackCounterResetTimeNotifier;
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value < Data.NumberOfAttack ? value : 0;
        }
        private int currentAttackCounter;

        private void Awake()
        {
            GetDependencies();
            attackCounterResetTimeNotifier = new TimeNotifier();
        }

        private void GetDependencies()
        {
            if (initDone) return;
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            Anim = BaseGameObject.GetComponent<Animator>();
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            initDone = true;
        }

        private void ResetAttackCounter() => currentAttackCounter = 0;

        public void SetCore(Core core)
        {
            this.Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }

        public void Enter()
        {
            Anim.SetBool("Active", true);
            Anim.SetInteger("Counter", currentAttackCounter);
            attackCounterResetTimeNotifier.Disable();
            AttackStartTime = Time.time;
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            Anim.SetBool("Active", false);
            CurrentAttackCounter++;
            attackCounterResetTimeNotifier.Init(attackCounterResetCooldown);
            OnExit?.Invoke();
        }
        private void Update()
        {
            attackCounterResetTimeNotifier.Tick();
        }

        private void OnEnable()
        {
            EventHandler.OnFinish += Exit;
            EventHandler.OnUseInput += OnUseInput;
            attackCounterResetTimeNotifier.OnNotify += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= Exit;
            EventHandler.OnUseInput -= OnUseInput;
            attackCounterResetTimeNotifier.OnNotify -= ResetAttackCounter;
        }
    }
}