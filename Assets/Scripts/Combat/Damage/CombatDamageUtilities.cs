using UnityEngine;
using System.Collections.Generic;

namespace Game.Combat.Damage
{
    public static class CombatDamageUtilities
    {
        public static bool TryDamage(GameObject gameObject, DamageData damageData, out IDamageable damageable)
        {
            if (gameObject.TryGetComponentInChildren(out damageable))
            {
                damageable.Damage(damageData);
                return true;
            }
            return false;
        }

        public static bool TryDamage(Collider2D[] colliders, DamageData damageData, out List<IDamageable> damageables)
        {
            var hasDamage = false;
            damageables = new List<IDamageable>();

            foreach (var collider in colliders)
            {
                if (TryDamage(collider.gameObject, damageData, out IDamageable damageable))
                {
                    damageables.Add(damageable);
                    hasDamage = true;
                }
            }

            return hasDamage;
        }
    }
}