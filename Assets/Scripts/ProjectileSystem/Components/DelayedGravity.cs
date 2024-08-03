using UnityEngine;

namespace Game.ProjectileSystem.Components
{
    public class DelayedGravity : ProjectileComponent
    {
        [field: SerializeField] public float Distance { private set; get; } = 10f;
        private float gravity;
        [HideInInspector]
        public float distanceMultiplier = 1f;

        private DistanceNotifier distanceNotifier = new DistanceNotifier();

        private void HandleNotify()
        {
            rb.gravityScale = gravity;
        }

        protected override void Init()
        {
            base.Init();
            rb.gravityScale = 0f;
            distanceNotifier.Init(transform.position, Distance * distanceMultiplier);
            distanceMultiplier = 1f;
        }
        protected override void Awake()
        {
            base.Awake();
            gravity = rb.gravityScale;
            distanceNotifier.OnNotify += HandleNotify;
        }

        protected override void Update()
        {
            base.Update();
            distanceNotifier?.Tick(transform.position);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            distanceNotifier.OnNotify -= HandleNotify;
        }

    }
}