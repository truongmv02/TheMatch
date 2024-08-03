using System;
using Game.ProjectileSystem;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Weapons
{
    public interface IProjectileSpawnerStrategy
    {
        void ExecuteSpawnStrategy(
            ProjectileSpawnInfo projectileSpawnInfo,
            Vector3 spawnPosition,
            int facingDirection,
            ObjectPools objectPools,
            Action<Projectile> OnSpawnProjectile, Transform owner = null
        );
    }
}