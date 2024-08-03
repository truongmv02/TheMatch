using Game.Weapons.Components;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.Movement coreMovement;
        private float velocity;
        private Vector2 direction;


        private void HandlerStartMovement()
        {
            velocity = currentAttackData.Velocity;
            direction = currentAttackData.Direction;

            SetVelocity();
        }

        private void HandlerStopMovement()
        {
            velocity = 0f;
            direction = Vector2.zero;
            SetVelocity();
        }

        protected override void HandlerEnter()
        {
            base.HandlerEnter();
            velocity = 0f;
            direction = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (!isAttackActive) return;

            SetVelocityX();
        }

        private void SetVelocity()
        {
            coreMovement.SetVelocity(velocity, direction, coreMovement.FacingDirection);
        }

        private void SetVelocityX()
        {
            coreMovement.SetVelocityX((direction * velocity).x * coreMovement.FacingDirection);
        }

        protected override void Start()
        {
            base.Start();
            coreMovement = Core.GetCoreComponent<CoreSystem.Movement>();
            AnimationEventHandler.OnStartMovement += HandlerStartMovement;
            AnimationEventHandler.OnStopMovement += HandlerStopMovement;

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnStartMovement -= HandlerStartMovement;
            AnimationEventHandler.OnStopMovement -= HandlerStopMovement;
        }

    }
}