using System;
using Game.ProjectileSystem;
using Game.Weapons.Components;
using UnityEngine;
namespace Game.Weapons
{
    [Serializable]
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        public float AngleVariation;
        public int ChargeAmount;
        private Vector2 currentDirection;



        public override void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnPosition,
            int facingDirection, ObjectPools objectPools, Action<Projectile> OnSpawnProjectile, Transform owner)
        {
            if (ChargeAmount <= 0) return;

            if (ChargeAmount == 1)
            {
                currentDirection = projectileSpawnInfo.Direction;
            }
            else
            {
                var initialRotationQuaternion = Quaternion.Euler(0f, 0f, -((ChargeAmount - 1f) / 2f * AngleVariation));
                currentDirection = initialRotationQuaternion * projectileSpawnInfo.Direction;
            }

            var rotationQuaternion = Quaternion.Euler(0f, 0f, AngleVariation);

            for (int i = 0; i < ChargeAmount; i++)
            {
                SpawnProjectile(projectileSpawnInfo, currentDirection, spawnPosition,
                facingDirection, objectPools, OnSpawnProjectile, owner);
                currentDirection = rotationQuaternion * currentDirection;
            }
        }

    }
}