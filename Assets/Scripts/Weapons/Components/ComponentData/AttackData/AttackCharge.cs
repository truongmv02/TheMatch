using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackCharge : AttackData
    {
        [field: SerializeField] public float ChargeTime { get; private set; }
        [field: SerializeField, Range(0, 1)] public int InitialChargeAmount { get; private set; }
        [field: SerializeField] public int NumberOfCharges { get; private set; }
        [field: SerializeField] public Vector2 ParticleOffset { get; private set; }
        [field: SerializeField] public GameObject ChargeIncreaseIndicatorParticlePrefab { get; private set; }
        [field: SerializeField] public GameObject FullyChargeIndicatorParticlePrefab { get; private set; }
    }
}