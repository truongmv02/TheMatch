using Game.ProjectileSystem;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class DrawToProjectile : WeaponComponent
    {
        private Draw draw;
        private ProjectileSpawner projectileSpawner;

        private readonly DrawModifierDataPackage drawModifierDataPackage = new DrawModifierDataPackage();

        private void HandleEvaluateCurve(float value)
        {
            drawModifierDataPackage.DrawPercentage = value;
        }

        private void HandleSpawnProjectile(Projectile projectile)
        {
            projectile.SendDataPackage(drawModifierDataPackage);
        }

        protected override void HandlerEnter()
        {
            base.HandlerEnter();

            drawModifierDataPackage.DrawPercentage = 0;
        }

        protected override void Start()
        {
            base.Start();
            draw = GetComponent<Draw>();
            projectileSpawner = GetComponent<ProjectileSpawner>();
            draw.OnEvaluateCurve += HandleEvaluateCurve;
            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            draw.OnEvaluateCurve -= HandleEvaluateCurve;
            projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
        }
    }
}