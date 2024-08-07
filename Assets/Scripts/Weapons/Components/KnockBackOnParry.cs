using System.Collections.Generic;
using UnityEngine;
using static Game.Combat.KnockBack.CombatKnockBackUtilities;

namespace Game.Weapons.Components
{
    public class KnockBackOnParry : WeaponComponent<KnockBackOnParryData, AttackKnockBack>
    {
        private Parry parry;
        private CoreSystem.Movement movement;

        private void HandleParry(GameObject parriedObject)
        {
            TryKnockBack(parriedObject, new Combat.KnockBack.KnockBackData(
                currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection, Core.Root), out _);
        }

        protected override void Start()
        {
            base.Start();
            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            parry = GetComponent<Parry>();
            parry.OnParry += HandleParry;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            parry.OnParry -= HandleParry;
        }
    }
}