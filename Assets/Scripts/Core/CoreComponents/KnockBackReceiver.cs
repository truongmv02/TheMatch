using Game.Combat.KnockBack;
using Game.ModifierSystem;
using UnityEngine;
namespace Game.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        public Modifiers<Modifier<KnockBackData>, KnockBackData> Modifiers { get; } = new();
        [SerializeField] private float maxKnockBackTime = 0.2f;
        private bool isKnockBackActive;
        private float knockBackStartTime;

        private CoreComp<Movement> movement;
        private CoreComp<CollisionSenses> collisionSenses;

        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
            collisionSenses = new CoreComp<CollisionSenses>(core);
        }

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }


        public void KnockBack(KnockBackData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            movement.Comp?.SetVelocity(data.Strength, data.Angle, data.Direction);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }

        private void CheckKnockBack()
        {
            if (isKnockBackActive &&
                (movement.Comp.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.Grounded) ||
                Time.time >= maxKnockBackTime + knockBackStartTime)
            {
                isKnockBackActive = false;
                movement.Comp.CanSetVelocity = true;
            }
        }

    }
}