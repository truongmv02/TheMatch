using UnityEngine;

namespace Game.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitbox>
    {
        [field: SerializeField] public LayerMask DetectedableLayers { get; private set; }


        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}