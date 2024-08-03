using Game.Combat.Damage;
using UnityEngine;
namespace Game.Weapons.Components
{
    public class DamageOnHitBoxAction : WeaponComponent<DamageOnHitBoxActionData, AttackDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            CombatDamageUtilities.TryDamage(colliders, new DamageData(currentAttackData.Amount, Core.Root), out _);
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}