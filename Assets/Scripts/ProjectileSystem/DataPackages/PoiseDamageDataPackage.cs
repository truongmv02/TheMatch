using System;
using UnityEngine;
namespace Game.ProjectileSystem.DataPackages
{
    [Serializable]
    public class PoiseDamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}