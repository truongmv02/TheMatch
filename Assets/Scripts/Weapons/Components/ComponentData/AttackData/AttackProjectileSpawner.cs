using System;
using Game.ProjectileSystem;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo
    {
        [field: SerializeField] public Vector2 Offset { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public Projectile projectilePrefab { get; private set; }
        [field: SerializeField] public DamageDataPackage DamageData { get; private set; }
        [field: SerializeField] public KnockBackDataPackage KnockBackData { get; private set; }
        [field: SerializeField] public PoiseDamageDataPackage PoiseDamageData { get; private set; }
        [field: SerializeField] public SpriteDataPackage SpriteDataPackage { get;  private set; }
    }
}