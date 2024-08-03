using UnityEngine;

namespace Game.ProjectileSystem.Components
{
    public class RotateTowardsVelocity : ProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var velocity = rb.velocity;

            if (velocity.Equals(Vector3.zero))
            {
                return;
            }

            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}