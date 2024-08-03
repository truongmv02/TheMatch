using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackDraw : AttackData
    {
        [field: SerializeField] public AnimationCurve DrawCurve { get; private set; }
        [field: SerializeField] public float DrawTime { get; private set; }
    }
}