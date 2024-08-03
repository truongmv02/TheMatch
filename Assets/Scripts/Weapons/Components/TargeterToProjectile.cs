using Game.ProjectileSystem;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;
namespace Game.Weapons.Components
{
    public class TargeterToProjectile : WeaponComponent
    {
        private ProjectileSpawner projectileSpawner;

        private Targeter targeter;

        private readonly TargetsDataPackage targetsDataPackage = new TargetsDataPackage();

        private void HandleSpawnProjectile(Projectile projectile)
        {
            targetsDataPackage.targets = targeter.GetTargets();

            projectile.SendDataPackage(targetsDataPackage);
        }

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<ProjectileSpawner>();
            targeter = GetComponent<Targeter>();
            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;

        }

    }
}