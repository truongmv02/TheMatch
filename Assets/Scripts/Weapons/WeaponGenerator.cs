using System;
using System.Collections.Generic;
using System.Linq;
using Game.CoreSystem;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        public event Action OnWeaponGenerating;
        [SerializeField] private Weapon weapon;
        [SerializeField] private CombatInput combatInput;
        private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDependencies = new List<Type>();


        private Animator anim;
        private void Start()
        {
            anim = GetComponentInChildren<Animator>();

            GenerateWeapon(weapon.Data);

        }


        public void GenerateWeapon(WeaponDataSO data)
        {
            OnWeaponGenerating?.Invoke();
            weapon.SetData(data);

            if (data == null)
            {
                weapon.CanEnterAttack = false;
                return;
            }

            componentAlreadyOnWeapon.Clear();
            componentAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                if (componentAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                {
                    continue;
                }

                var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);
                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }
                weaponComponent.Init();
                componentAddedToWeapon.Add(weaponComponent);
            }

            var componentsToRemove = componentAlreadyOnWeapon.Except(componentAddedToWeapon);

            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
            anim.runtimeAnimatorController = data.AnimatorController;

            weapon.CanEnterAttack = true;
        }

        private void HandleWeaponDataChanged(int input, WeaponDataSO data)
        {
            if (input != (int)combatInput)
            {
                return;
            }

            GenerateWeapon(data);
        }

        private void OnDestroy()
        {
            //  weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
        }


    }
}