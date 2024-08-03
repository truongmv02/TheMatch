using Game.Combat.Damage;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ProjectileSystem.Components
{
    public class Damage : ProjectileComponent
    {
        public UnityEvent<IDamageable> OnDamage;
        public UnityEvent<RaycastHit2D> OnRaycastHit;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public bool SetInactiveAfterDamage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }


        private HitBox hitBox;
        private float amount;
        private float lastDamageTime;

        protected override void Init()
        {
            base.Init();
            lastDamageTime = Mathf.NegativeInfinity;
            if (projectile.owner.name == "Player1")
            {
                LayerMask = LayerMask.GetMask("Player2Damageable");
            }
            else
            {
                LayerMask = LayerMask.GetMask("Player1Damageable");
            }
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
        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active) return;

            if (Time.time < lastDamageTime + Cooldown) return;

            foreach (var hit in hits)
            {
                if (!LayerMaskUtility.IsLayerInMask(hit, LayerMask))
                {
                    continue;
                }
                if (!hit.collider.transform.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    continue;
                }
                damageable.Damage(new DamageData(amount, projectile.gameObject));
                OnDamage?.Invoke(damageable);
                OnRaycastHit?.Invoke(hit);
                lastDamageTime = Time.time;

                if (SetInactiveAfterDamage)
                {
                    SetActive(false);
                }

                return;
            }
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DamageDataPackage package) return;

            amount = package.Amount;
        }


    }
}