using System;
using System.Collections.Generic;
using Game.CoreSystem;
using Game.Weapons.Modifiers;
using UnityEngine;
using static Game.Combat.Parry.CombatParryUtilities;

namespace Game.Weapons.Components
{
    public class Parry : WeaponComponent<ParryData, AttackParry>
    {
        public event Action<GameObject> OnParry;

        private DamageReceiver damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiver poiseDamageReceiver;

        private DamageModifiter damageModifiter;
        private KnockBackModifier knockBackModifier;
        private PoiseDamageModifier poiseDamageModifier;

        private CoreSystem.Movement movement;
        private ParticleManager particleManager;

        private bool isBlockWindowActive;
        private bool shouldUpdate;

        private float nextWindowTrigger;

        private void StartParryWindow()
        {
            isBlockWindowActive = true;
            shouldUpdate = false;
            damageModifiter.OnModified += HandleParry;

            damageReceiver.Modifiers.AddModifier(damageModifiter);
            knockBackReceiver.Modifiers.AddModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
        }


        private void StopParryWindow()
        {
            isBlockWindowActive = false;
            shouldUpdate = false;
            damageModifiter.OnModified += HandleParry;

            damageReceiver.Modifiers.RemoveModifier(damageModifiter);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
        }

        private void HandleParry(GameObject parriedGameObject)
        {
            if (!TryParry(parriedGameObject, new Combat.Parry.ParryData(Core.Root), out _, out _))
            {
                return;
            }

            weapon.Anim.SetTrigger("Parry");
            OnParry?.Invoke(parriedGameObject);

            particleManager.StartWithRandomRotation(currentAttackData.Particles, currentAttackData.ParticlesOffset);
        }

        private void HandleAttackPhase(AttackPhases phase)
        {
            shouldUpdate = isBlockWindowActive ? currentAttackData.ParryWindowEnd.TryGetTriggerTime(phase, out nextWindowTrigger)
                            : currentAttackData.ParryWindowStart.TryGetTriggerTime(phase, out nextWindowTrigger);
        }
        private bool IsAttackParried(Transform source, out DirectionalInfomation directionalInfomation)
        {
            var angleOfAttacker = AngleUtilities.AngleFormFacingDirection(Core.Root.transform, source, movement.FacingDirection);

            return currentAttackData.IsBlocked(angleOfAttacker, out directionalInfomation);
        }
        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiver>();

            particleManager = Core.GetCoreComponent<ParticleManager>();

            damageModifiter = new DamageModifiter(IsAttackParried);
            knockBackModifier = new KnockBackModifier(IsAttackParried);
            poiseDamageModifier = new PoiseDamageModifier(IsAttackParried);
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
                StopParryWindow();
            }
            else
            {
                StartParryWindow();
            }


        }

        private bool IsPastTriggerTime()
        {
            return Time.time >= nextWindowTrigger;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnEnterAttackPhase -= HandleAttackPhase;
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            damageReceiver.Modifiers.RemoveModifier(damageModifiter);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
        }


    }
}