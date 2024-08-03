using System;
using UnityEngine;
namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackParry : AttackData
    {
        [field: SerializeField] public DirectionalInfomation[] ParryDirectionalInfomation { get; private set; }

        [field: SerializeField] public PhaseTime ParryWindowStart { get; private set; }
        [field: SerializeField] public PhaseTime ParryWindowEnd { get; private set; }
        [field: SerializeField] public GameObject Particles { get; private set; }
        [field: SerializeField] public Vector2 ParticlesOffset { get; private set; }

        public bool IsBlocked(float angle, out DirectionalInfomation directionalInfomation)
        {

            directionalInfomation = null;

            foreach (var dir in ParryDirectionalInfomation)
            {
                var blocked = dir.IsAngleBetween(angle);
                if (!blocked) continue;
                directionalInfomation = dir;
                return true;
            }

            return false;
        }
    }
}