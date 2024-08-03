using System;
using Game.ProjectileSystem;
using Game.Weapons.Components;
using UnityEngine;
namespace Game.Weapons
{
    public class ProjectileSpawnerStrategy : IProjectileSpawnerStrategy
    {
        private Vector2 spawnPos;
        private Vector2 spawnDir;

        private Projectile currentProjectile;
        public virtual void ExecuteSpawnStrategy(
            ProjectileSpawnInfo projectileSpawnInfo,
            Vector3 spawnPosition,
            int facingDirection,
            ObjectPools objectPools,
            Action<Projectile> OnSpawnProjectile, Transform owner)
        {
            SpawnProjectile(projectileSpawnInfo, projectileSpawnInfo.Direction, spawnPosition, facingDirection, objectPools, OnSpawnProjectile, owner);
        }

        protected virtual void SpawnProjectile(
            ProjectileSpawnInfo projectileSpawnInfo,
            Vector2 spawnDirection,
            Vector3 spawnPosition,
            int facingDirection,
            ObjectPools objectPools,
            Action<Projectile> OnSpawnProjectile, Transform owner = null
        )
        {
            SetSpawnPosition(spawnPosition, projectileSpawnInfo.Offset, facingDirection);
            SetSpawnDirection(spawnDirection, facingDirection);
            GetProjectileAndSetPostionAndRotation(objectPools, projectileSpawnInfo.projectilePrefab);
            currentProjectile.owner = owner;
            InitializeProjectile(projectileSpawnInfo, OnSpawnProjectile);

        }

        protected virtual void GetProjectileAndSetPostionAndRotation(ObjectPools objectPools, Projectile prefab)
        {
            currentProjectile = objectPools.GetObject(prefab);
            currentProjectile.transform.position = spawnPos;
            var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
            currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        protected virtual void InitializeProjectile(ProjectileSpawnInfo projectileSpawnInfo, Action<Projectile> OnSpawnProjectile)
        {
            currentProjectile.Reset();
            currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.PoiseDamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.SpriteDataPackage);
            OnSpawnProjectile?.Invoke(currentProjectile);
            currentProjectile.Init();
        }

        protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
        {
            spawnDir.Set(direction.x * facingDirection, direction.y);
        }

        protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
        {
            spawnPos = referencePosition;
            spawnPos.Set(spawnPos.x + offset.x * facingDirection, spawnPos.y + offset.y);
        }
    }
}