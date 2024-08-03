using Game.Combat.KnockBack;
using Game.ModifierSystem;
using UnityEngine;

namespace Game.Weapons.Modifiers
{
    public class KnockBackModifier : Modifier<KnockBackData>
    {
        private readonly ConditionalDelegate isBlocked;

        public KnockBackModifier(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }
        public override KnockBackData ModifyValue(KnockBackData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInfomation))
            {
                value.Strength *= (1 - blockDirectionInfomation.KnockBackAbsorption);
            }

            return value;
        }
    }
}