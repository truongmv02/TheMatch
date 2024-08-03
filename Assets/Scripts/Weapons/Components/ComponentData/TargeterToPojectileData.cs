using UnityEngine;
namespace Game.Weapons.Components
{
    public class TargeterToPojectileData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(TargeterToProjectile);
        }
    }
}