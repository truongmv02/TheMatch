using UnityEngine;

namespace Game.Weapons.Components
{
    public class OptionalSpriteData : ComponentData<AttackOptionalSprite>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(OptionalSprite);
        }
    }
}