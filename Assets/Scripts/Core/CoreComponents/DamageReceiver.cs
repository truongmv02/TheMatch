using Game.Combat.Damage;
using Game.ModifierSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damagePariticles;

        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();

        private Stats stats;
        private ParticleManager particleManager;

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }


        public void Damage(DamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            SoundManager.Instance.PlaySound(SoundManager.Instance.hurt);
            if (data.Amount <= 0) return;
            stats.Health.Decrease(data.Amount);
            particleManager.StartParticles(damagePariticles);
        }
    }
}