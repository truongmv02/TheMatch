using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackPoiseDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
        
    }
}