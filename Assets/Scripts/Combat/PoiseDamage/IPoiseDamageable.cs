using UnityEngine;

namespace Game.Combat.PoiseDamage
{
    public interface IPoiseDamageable
    {
        void DamagePoise(PoiseDamageData data);
    }
}