using System.Collections.Generic;
using Game.Combat.Damage;
using UnityEngine;
namespace Game.Combat.KnockBack
{
    public static class CombatKnockBackUtilities
    {
        public static bool TryKnockBack(GameObject gameObject, KnockBackData knockBackData, out IKnockBackable knockBackable)
        {
            if (gameObject.TryGetComponentInChildren(out knockBackable))
            {
                knockBackable.KnockBack(knockBackData);
                return true;
            }
            return false;
        }

        public static bool TryKnockBack(IEnumerable<GameObject> gameObjects, KnockBackData data, out List<IKnockBackable> knockBackables)
        {
            bool hasKnockBack = false;
            knockBackables = new List<IKnockBackable>();
            foreach (var gameObject in gameObjects)
            {
                if (TryKnockBack(gameObject, data, out var knockBackable))
                {
                    hasKnockBack = true;
                    knockBackables.Add(knockBackable);
                }
            }

            return hasKnockBack;
        }

        public static bool TryKnockBack(Component component, KnockBackData knockBackData, out IKnockBackable knockBackable)
        {
            return TryKnockBack(component.gameObject, knockBackData, out knockBackable);
        }



        public static bool TryKnockBack(IEnumerable<Component> components, KnockBackData data, out List<IKnockBackable> knockBackables)
        {
            bool hasKnockBack = false;
            knockBackables = new List<IKnockBackable>();
            foreach (var comp in components)
            {
                if (TryKnockBack(comp, data, out var knockBackable))
                {
                    hasKnockBack = true;
                    knockBackables.Add(knockBackable);
                }
            }

            return hasKnockBack;
        }
    }
}