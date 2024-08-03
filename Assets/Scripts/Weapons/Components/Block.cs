using System;
using Game.CoreSystem;
using Game.Weapons.Modifiers;
using UnityEngine;
namespace Game.Weapons.Components
{
    public class Block : WeaponComponent<BlockData, AttackBlock>
    {
        public event Action<GameObject> OnBlock;

        private DamageReceiver damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiver poiseDamageReceiver;

        private DamageModifiter damageModifiter;
        private KnockBackModifier KnockBackModifier;
        private PoiseDamageModifier poiseDamageModifier;
        private CoreSystem.Movement movement;
        private ParticleManager particleManager;

        private bool isBlockWindowActive;
        private bool shouldUpdate;
        private float nextWindowTriggerTime;

        private void StartBlockWindown()
        {
            isBlockWindowActive = true;
            shouldUpdate = false;
            damageModifiter.OnModified += HandleModified;

            damageReceiver.Modifiers.AddModifier(damageModifiter);
            knockBackReceiver.Modifiers.AddModifier(KnockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
        }

        private void StopBlockWindown()
        {
            isBlockWindowActive = false;
            shouldUpdate = false;
            damageModifiter.OnModified -= HandleModified;
            damageReceiver.Modifiers.RemoveModifier(damageModifiter);
            knockBackReceiver.Modifiers.RemoveModifier(KnockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
        }
        private bool IsAttackBlocked(Transform source, out DirectionalInfomation blockDirectionInfomation)
        {
            var angleOfAttacker = AngleUtilities.AngleFormFacingDirection(Core.Root.transform, source, movement.FacingDirection);
            return currentAttackData.IsBlocked(angleOfAttacker, out blockDirectionInfomation);
        }

        private void HandleAttackPhase(AttackPhases phase)
        {
            shouldUpdate = isBlockWindowActive ? currentAttackData.BlockWindowEnd.TryGetTriggerTime(phase, out nextWindowTriggerTime)
                          : currentAttackData.BlockWindowStart.TryGetTriggerTime(phase, out nextWindowTriggerTime);

        }

        private void HandleModified(GameObject source)
        {
            particleManager.StartWithRandomRotation(currentAttackData.Particles, currentAttackData.ParticlesOffset);
            OnBlock?.Invoke(source);
        }

        protected override void Start()
        {
            base.Start();
            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiver>();
            particleManager = Core.GetCoreComponent<ParticleManager>();

            damageModifiter = new DamageModifiter(IsAttackBlocked);
            KnockBackModifier = new KnockBackModifier(IsAttackBlocked);
            poiseDamageModifier = new PoiseDamageModifier(IsAttackBlocked);

            AnimationEventHandler.OnEnterAttackPhase += HandleAttackPhase;
        }

        private void Update()
        {
            if (!shouldUpdate || !IsPastTriggerTime())
            {
                return;
            }

            if (isBlockWindowActive)
            {
                StopBlockWindown();
            }
            else
            {
                StartBlockWindown();
            }
        }

        private bool IsPastTriggerTime()
        {
            return Time.time >= nextWindowTriggerTime;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            AnimationEventHandler.OnEnterAttackPhase -= HandleAttackPhase;
        }
    }


}