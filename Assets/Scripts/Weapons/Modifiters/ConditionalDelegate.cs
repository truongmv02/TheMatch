using Game.Weapons.Components;
using UnityEngine;

namespace Game.Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInfomation directionalInfomation);
}