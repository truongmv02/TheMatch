using Game.Combat.Damage;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class DamageOnBlock : WeaponComponent<DamageOnBlockData, AttackDamage>
    {
        private Block block;
        private void HandleBlock(GameObject blockedGameObject)
        {
            CombatDamageUtilities.TryDamage(blockedGameObject, new DamageData(currentAttackData.Amount, Core.Root), out _);
        }

        protected override void Start()
        {
            base.Start();
            block = GetComponent<Block>();
            block.OnBlock += HandleBlock;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            block.OnBlock -= HandleBlock;
        }
    }
}