using UnityEngine;

namespace Game.Weapons.Components
{
    public class DamageOnBlockData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(DamageOnBlock);
        }
    }
}