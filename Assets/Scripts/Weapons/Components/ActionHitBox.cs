using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitbox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;
        private CoreSystem.CoreComp<CoreSystem.Movement> movement;
        private Vector2 offset;
        private Collider2D[] detected;

        LayerMask layerMask;

        protected override void Start()
        {
            base.Start();
            AnimationEventHandler.OnAttackAction += HandleAttackAction;
            movement = new CoreSystem.CoreComp<CoreSystem.Movement>(Core);
            if (weapon.Core.Root.name == "Player1")
            {

                layerMask = LayerMask.GetMask("Player2Damageable");
            }
            else
            {
                layerMask = LayerMask.GetMask("Player1Damageable");
            }
        }
        private void HandleAttackAction()
        {
            offset.Set(
                    transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FacingDirection),
                    transform.position.y + currentAttackData.HitBox.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, layerMask);

            if (detected.Length == 0) return;
            OnDetectedCollider2D?.Invoke(detected);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            AnimationEventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null) return;
            foreach (var item in data.GetAllAttackData())
            {
                if (item.Debug)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
                }
            }
        }
    }
}