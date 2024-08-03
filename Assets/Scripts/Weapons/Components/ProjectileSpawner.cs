using System;
using Game.ProjectileSystem;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {

        public event Action<Projectile> OnSpawnProjectile;
        private CoreSystem.Movement movement;

        private IProjectileSpawnerStrategy projectileSpawnerStrategy;

        private readonly ObjectPools objectPools = new ObjectPools();

        public void SetProjectileSpawnerStrategy(IProjectileSpawnerStrategy newStrategy)
        {
            this.projectileSpawnerStrategy = newStrategy;
        }
        private void HandleAttackAction()
        {
            foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
            {
                projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, transform.position,
                     movement.FacingDirection, objectPools, OnSpawnProjectile, weapon.Core.Root.transform);
            }
        }

        private void SetDefaultProjectileSpawnStartegy()
        {
            projectileSpawnerStrategy = new ProjectileSpawnerStrategy();
        }

        protected override void Awake()
        {
            base.Awake();
            SetDefaultProjectileSpawnStartegy();
        }

        protected override void Start()
        {
            base.Start();
            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            AnimationEventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            SetDefaultProjectileSpawnStartegy();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnAttackAction -= HandleAttackAction;
        }



        private void OnDrawGizmosSelected()
        {
            if (data == null)
            {
                return;
            }

            foreach (var item in data.GetAllAttackData())
            {
                foreach (var point in item.SpawnInfos)
                {
                    var pos = transform.position + (Vector3)point.Offset;
                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(pos, pos + (Vector3)point.Direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }
    }
}