using System.Collections.Generic;
using UnityEngine;
namespace Game.ModifierSystem
{
    public class Modifiers<TModifierType, TValueType> where TModifierType : Modifier<TValueType>
    {
        private readonly List<TModifierType> modifierList = new List<TModifierType>();

        public TValueType ApplyAllModifiers(TValueType initialValue)
        {
            var modifierValue = initialValue;

            foreach (var modifier in modifierList)
            {
                modifierValue = modifier.ModifyValue(modifierValue);
            }

            return modifierValue;

        }

        public void AddModifier(TModifierType modifier) => modifierList.Add(modifier);
        public void RemoveModifier(TModifierType modifier) => modifierList.Remove(modifier);
    }
}