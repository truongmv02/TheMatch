using Game.Combat.PoiseDamage;
using Game.ModifierSystem;
using UnityEngine;

namespace Game.Weapons.Modifiers
{
    public class PoiseDamageModifier : Modifier<PoiseDamageData>
    {
        private readonly ConditionalDelegate isBlocked;
        public PoiseDamageModifier(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override PoiseDamageData ModifyValue(PoiseDamageData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInfomation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInfomation.PoiseDamageAbsorption));
            }
            return value;
        }
    }
}