using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackSound : AttackData
    {
        [field: SerializeField] public PhaseSounds[] AttackSounds { get; private set; }
    }
    [Serializable]
    public struct PhaseSounds
    {
        [field: SerializeField] public AttackPhases Phase { get; private set; }
        [field: SerializeField] public AudioClip Clip { private set; get; }
    }
}