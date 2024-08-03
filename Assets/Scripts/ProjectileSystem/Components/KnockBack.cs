using Game.Combat.KnockBack;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ProjectileSystem.Components
{
    public class KnockBack : ProjectileComponent
    {
        public UnityEvent OnKnockBack;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private HitBox hitBox;
        private int direction;
        private float strength;
        private Vector2 angle;
        protected override void Init()
        {
            base.Init();
            if (projectile.owner.name == "Player1")
            {
                LayerMask = LayerMask.GetMask("Player2Damageable");
            }
            else
            {
                LayerMask = LayerMask.GetMask("Player1Damageable");
            }
        }

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active) return;

            direction = (int)Mathf.Sign(transform.right.x);

            foreach (var hit in hits)
            {
                if (!LayerMaskUtility.IsLayerInMask(hit, LayerMask))
                {
                    continue;
                }

                if (!hit.collider.transform.gameObject.TryGetComponent(out IKnockBackable knockBackable))
                {
                    continue;
                }
                knockBackable.KnockBack(new KnockBackData(angle, strength, direction, projectile.gameObject));

                OnKnockBack?.Invoke();

                return;
            }
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not KnockBackDataPackage knockBackData) return;

            strength = knockBackData.Strength;
            angle = knockBackData.Angle;
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