using System;
using System.Collections.Generic;
using System.Linq;
using Game.Weapons.Components;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
namespace Game.Weapons
{

    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        private static List<Type> dataCompTypes = new List<Type>();
        private WeaponDataSO dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set Number of Attacks"))
            {
                foreach (var item in dataSO.ComponentDatas)
                {
                    item.InitializeAttackData(dataSO.NumberOfAttack);
                }
            }

            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");
            if (showAddComponentButtons)
            {
                foreach (var item in dataCompTypes)
                {
                    if (GUILayout.Button(item.Name))
                    {
                        var comp = Activator.CreateInstance(item) as ComponentData;
                        if (comp == null)
                        {
                            return;
                        }
                        comp.InitializeAttackData(dataSO.NumberOfAttack);
                        dataSO.AddData(comp);

                        EditorUtility.SetDirty(dataSO);
                    }
                }

            }

            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");

            if (showForceUpdateButtons)
            {
                if (GUILayout.Button("Force Update Component Name"))
                {
                    foreach (var item in dataSO.ComponentDatas)
                    {
                        item.SetComponentName();
                    }
                }

                if (GUILayout.Button("Force Update Attack Name"))
                {
                    foreach (var item in dataSO.ComponentDatas)
                    {
                        item.SetAttackDataNames();
                    }
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assemblies => assemblies.GetTypes());
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );

            dataCompTypes = filteredTypes.ToList();
        }
    }
}