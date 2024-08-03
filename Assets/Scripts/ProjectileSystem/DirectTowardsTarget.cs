using System.Collections.Generic;
using System.Linq;
using Game.ProjectileSystem.Components;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.ProjectileSystem
{
    public class DirectTowardsTarget : ProjectileComponent
    {
        [SerializeField] private float minStep;
        [SerializeField] private float maxStep;
        [SerializeField] private float timeToMaxStep;

        private List<Transform> targets;

        private Transform currentTarget;

        private float step;
        private float startTime;

        private Vector2 direction;

        protected override void Init()
        {
            base.Init();

            currentTarget = null;
            startTime = Time.time;
            step = minStep;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!HasTarget()) return;

            step = Mathf.Lerp(minStep, maxStep, (Time.time - startTime) / timeToMaxStep);
            direction = (currentTarget.position - transform.position).normalized;

            Rotate(direction);
        }

        private bool HasTarget()
        {
            if (currentTarget) return true;

            targets.RemoveAll(item => item == null);

            if (targets.Count <= 0)
            {
                return false;
            }

            targets = targets.OrderBy(target => (target.position - transform.position).sqrMagnitude).ToList();
            currentTarget = targets[0];
            return true;
        }

        private void Rotate(Vector2 dir)
        {
            if (dir.Equals(Vector2.zero)) return;
            var toRotation = QuaternionExtensions.Vector2ToRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, step * Time.deltaTime);
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not TargetsDataPackage targetsDataPackage)
            {
                return;
            }
            targets = targetsDataPackage.targets;
        }

        private void OnDrawGizmos()
        {
            if (currentTarget)
            {
                Gizmos.DrawLine(transform.position, currentTarget.position);
            }
        }
    }
}