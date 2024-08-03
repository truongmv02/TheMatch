using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ProjectileSystem.Components
{
    public class HitBox : ProjectileComponent
    {
        public UnityEvent<RaycastHit2D[]> OnRaycastHit2D;
        [field: SerializeField] public Rect HitBoxRect { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private RaycastHit2D[] hits;
        private float checkdistance;
        private Transform _transform;

        protected override void Init()
        {
            base.Init();
            if (projectile.owner.name == "Player1")
            {
                LayerMask = LayerMask.GetMask(new[] { "Player2Damageable", "Ground" });
            }
            else
            {
                LayerMask = LayerMask.GetMask(new[] { "Player1Damageable", "Ground" });
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            checkdistance = rb.velocity.magnitude * Time.deltaTime;
            CheckHitBox();
        }

        private void CheckHitBox()
        {
            hits = Physics2D.BoxCastAll(transform.TransformPoint(HitBoxRect.center), HitBoxRect.size,
                    _transform.rotation.eulerAngles.z, _transform.right, checkdistance, LayerMask);

            if (hits.Length <= 0) return;
            OnRaycastHit2D?.Invoke(hits);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Matrix4x4 rotateMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Vector3.one);
            Gizmos.matrix = rotateMatrix;
            Gizmos.DrawWireCube(HitBoxRect.center, HitBoxRect.size);
        }
    }
}