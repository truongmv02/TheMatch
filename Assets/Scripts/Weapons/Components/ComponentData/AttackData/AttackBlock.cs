using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackBlock : AttackData
    {
        [field: SerializeField] public DirectionalInfomation[] BlockDirectionInfomation { get; private set; }

        [field : SerializeField] public PhaseTime BlockWindowStart{ get; private set; }
        [field : SerializeField] public PhaseTime BlockWindowEnd{ get; private set; }
        [field: SerializeField] public GameObject Particles { get; private set; }
        [field: SerializeField] public Vector2 ParticlesOffset { get; private set; }

        public bool IsBlocked(float angle, out DirectionalInfomation directionInfomation)
        {
            directionInfomation = null;
            foreach (var dir in BlockDirectionInfomation)
            {
                var blocked = dir.IsAngleBetween(angle);
                if (!blocked) break;

                directionInfomation = dir;
                return true;
            }

            return false;
        }
    }

    [Serializable]
    public class DirectionalInfomation
    {
        [Range(-180f, 180f)] public float MinAngle;
        [Range(-180f, 180f)] public float MaxAngle;
        [Range(0f, 1f)] public float DamageAbsorption;
        [Range(0f, 1f)] public float KnockBackAbsorption;
        [Range(0f, 1f)] public float PoiseDamageAbsorption;

        public bool IsAngleBetween(float angle)
        {
            if (MaxAngle > MinAngle)
            {
                return angle >= MinAngle && angle <= MaxAngle;
            }

            return (angle >= MinAngle && angle <= 180f) || (angle <= MaxAngle && angle >= -180f);
        }
    }
}