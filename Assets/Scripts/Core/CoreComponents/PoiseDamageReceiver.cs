using Game.Combat.PoiseDamage;
using Game.ModifierSystem;
using UnityEngine;

namespace Game.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {

        private Stats stats;

        public Modifiers<Modifier<PoiseDamageData>, PoiseDamageData> Modifiers { get; } = new();

        protected override void Awake()
        {
            base.Awake();
            stats = core.GetCoreComponent<Stats>();
        }
        public void DamagePoise(PoiseDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            stats.Poise.Decrease(data.Amount);
        }
    }
}