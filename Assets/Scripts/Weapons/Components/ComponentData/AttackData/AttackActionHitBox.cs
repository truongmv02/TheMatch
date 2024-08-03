using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    [Serializable]
    public class AttackActionHitbox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}