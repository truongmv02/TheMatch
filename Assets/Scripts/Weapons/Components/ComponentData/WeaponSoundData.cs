using UnityEngine;
namespace Game.Weapons.Components
{
    public class WeaponSoundData : ComponentData<AttackSound>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponSound);
        }
    }
}