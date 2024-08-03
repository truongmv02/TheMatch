using UnityEngine;
using System;
using Game.ModifierSystem;
using Game.Weapons.Components;
using Movement = Game.CoreSystem.Movement;
using DamageData = Game.Combat.Damage.DamageData;

namespace Game.Weapons.Modifiers
{
    public class DamageModifiter : Modifier<DamageData>
    {
        public event Action<GameObject> OnModified;
        private readonly ConditionalDelegate isBlocked;

        public DamageModifiter(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }


        public override DamageData ModifyValue(DamageData value)
        {

            if (isBlocked(value.Source.transform, out var blockDirectionInfomation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInfomation.DamageAbsorption));
                OnModified?.Invoke(value.Source);
            }

            return value;
        }
    }
}