using UnityEngine;
namespace Game.CoreSystem
{
    public class Movement : CoreComponent
    {
        private Vector2 workspace;

        public bool CanSetVelocity { get; set; }
        public Vector2 CurrentVelocity { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public int FacingDirection { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            FacingDirection = 1;
            CanSetVelocity = true;
            Rb = GetComponentInParent<Rigidbody2D>();
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = Rb.velocity;
        }



        #region Set methods

        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * direction * velocity, angle.y * velocity);
            SetFinalVelocity();
        }
        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        private void SetFinalVelocity()
        {
            if (!CanSetVelocity) return;
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        #endregion


        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            Rb.transform.Rotate(0f, 180f, 0f);
        }

        public Vector2 FindRelativePoint(Vector2 offset)
        {
            offset.x *= FacingDirection;
            return transform.position + (Vector3)offset;
        }

    }
}