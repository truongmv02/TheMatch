using UnityEngine;

namespace Game.Weapons.Components
{
    public class ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
    {
        private ProjectileSpawner projectileSpawner;
        private ChargeProjectileSpawnerStrategy chargeProjectileSpawnerStrategy = new ChargeProjectileSpawnerStrategy();
        private Charge charge;

        private bool hasReadCharge;

        protected override void HandlerEnter()
        {
            base.HandlerEnter();
            hasReadCharge = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasReadCharge) return;

            chargeProjectileSpawnerStrategy.AngleVariation = currentAttackData.AngleVariation;
            chargeProjectileSpawnerStrategy.ChargeAmount = charge.TakeFinalChargeReading();
            projectileSpawner.SetProjectileSpawnerStrategy(chargeProjectileSpawnerStrategy);
            hasReadCharge = true;
        }

        protected override void Start()
        {
            base.Start();
            projectileSpawner = GetComponent<ProjectileSpawner>();
            charge = GetComponent<Charge>();
            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        }


    }
}