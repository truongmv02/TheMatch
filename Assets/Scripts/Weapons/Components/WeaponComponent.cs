using Game.CoreSystem;
using UnityEngine;

namespace Game.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected AnimationEventHandler AnimationEventHandler => weapon.EventHandler;

        protected Core Core => weapon.Core;

        protected float attackStartTime => weapon.AttackStartTime;

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void Start()
        {
            weapon.OnEnter += HandlerEnter;
            weapon.OnExit += HandleExit;
        }
        public virtual void Init()
        {

        }
        protected virtual void HandlerEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandlerEnter;
            weapon.OnExit -= HandleExit;
        }

    }
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;


        protected override void HandlerEnter()
        {
            base.HandlerEnter();

            currentAttackData = data.GetAttackData(weapon.CurrentAttackCounter);
        }

        public override void Init()
        {
            base.Init();

            data = weapon.Data.GetData<T1>();
        }
    }
}