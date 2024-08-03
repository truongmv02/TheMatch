using Game.Combat.PoiseDamage;
using Game.CoreSystem;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ProjectileSystem.Components
{
    public class PoiseDamage : ProjectileComponent
    {
        public UnityEvent OnPoiseDamage;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private float amount;
        private HitBox hitBox;

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active) return;

            foreach (var hit in hits)
            {
                if (!LayerMaskUtility.IsLayerInMask(hit, LayerMask)) continue;
                if (!hit.collider.transform.gameObject.TryGetComponent(out IPoiseDamageable poiseDamageable))
                    continue;

                poiseDamageable.DamagePoise(new PoiseDamageData(amount, projectile.gameObject));
                OnPoiseDamage?.Invoke();

                return;
            }

        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not PoiseDamageDataPackage poiseDamageData)
            {
                return;
            }
            amount = poiseDamageData.Amount;
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<HitBox>();
            hitBox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

    }
}