using System.Collections.Generic;
using UnityEngine;
namespace Game.Combat.Parry
{
    public static class CombatParryUtilities
    {
        public static bool TryParry(GameObject gameObject, ParryData data, out IParryable parryable, out GameObject parriedObject)
        {
            parriedObject = null;
            if (gameObject.TryGetComponentInChildren(out parryable))
            {
                parryable.Parry(data);
                parriedObject = gameObject;
                return true;
            }
            return false;
        }

        public static bool TryParry(Component component, ParryData data, out IParryable parryable, out GameObject parriedObject)
        {
            return TryParry(component.gameObject, data, out parryable, out parriedObject);
        }
        public static bool TryParry<T>(T[] components, ParryData data, out List<IParryable> parryables,
            out List<GameObject> parriedObjects) where T : Component
        {
            var hasParried = false;

            parryables = new List<IParryable>();
            parriedObjects = new List<GameObject>();

            foreach (var component in components)
            {
                if (TryParry(component, data, out var parryable, out var parriedObject))
                {
                    hasParried = true;
                    parryables.Add(parryable);
                    parriedObjects.Add(parriedObject);
                }
            }
            return hasParried;
        }


    }
}