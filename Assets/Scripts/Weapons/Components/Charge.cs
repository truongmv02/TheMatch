using Game.CoreSystem;
using Unity.VisualScripting;
using UnityEngine;
namespace Game.Weapons.Components
{
    public class Charge : WeaponComponent<ChargeData, AttackCharge>
    {
        private int currentCharge;
        public int CurrentCharge => currentCharge;
        private TimeNotifier timeNotifier;
        private ParticleManager particleManager;

        protected override void Awake()
        {
            base.Awake();

            timeNotifier = new TimeNotifier();
            timeNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();
            particleManager = Core.GetCoreComponent<ParticleManager>();
        }
        private void Update()
        {
            timeNotifier.Tick();
        }

        public int TakeFinalChargeReading()
        {
            timeNotifier.Disable();
            return currentCharge;
        }

        protected override void HandlerEnter()
        {
            base.HandlerEnter();

            currentCharge = currentAttackData.InitialChargeAmount;
            timeNotifier.Init(currentAttackData.ChargeTime, true);
        }

        private void HandleNotify()
        {
            currentCharge++;

            if (currentCharge >= currentAttackData.NumberOfCharges)
            {
                currentCharge = currentAttackData.NumberOfCharges;
                timeNotifier.Disable();
                particleManager.StartParticleRelative(currentAttackData.FullyChargeIndicatorParticlePrefab, currentAttackData.ParticleOffset, Quaternion.identity);

            }
            else
            {
                particleManager.StartParticleRelative(currentAttackData.ChargeIncreaseIndicatorParticlePrefab, currentAttackData.ParticleOffset, Quaternion.identity);
            }
        }

        protected override void HandleExit()
        {
            base.HandleExit();

            timeNotifier.Disable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            timeNotifier.OnNotify -= HandleNotify;
        }
    }
}