using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Game.Weapons.Components;
using System;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
    [field: SerializeField] public int NumberOfAttack { get; private set; }

    [field: SerializeReference] public List<ComponentData> ComponentDatas { get; private set; }

    public void AddData(ComponentData data)
    {
        if (ComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;
        ComponentDatas.Add(data);
    }

    public List<Type> GetAllDependencies()
    {
        return ComponentDatas.Select(comp => comp.ComponentDependency).ToList();
    }

    public T GetData<T>()
    {
        return ComponentDatas.OfType<T>().FirstOrDefault();
    }

}