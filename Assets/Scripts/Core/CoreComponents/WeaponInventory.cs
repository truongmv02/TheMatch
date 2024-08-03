/*using System;
using Game.Weapons;
using UnityEngine;

namespace Game.CoreSystem
{
    public class WeaponInventory : CoreComponent
    {
        public event Action<int, WeaponDataSO> OnWeaponDataChanged;
        [field: SerializeField] public WeaponDataSO[] weaponDatas { get; private set; }

        public bool TrySetWeapon(WeaponDataSO newData, int index, out WeaponDataSO oldData)
        {
            if (index >= weaponDatas.Length)
            {
                oldData = null;
                return false;
            }

            oldData = weaponDatas[index];
            weaponDatas[index] = newData;

            OnWeaponDataChanged?.Invoke(index, newData);
            return true;
        }

        public bool TryGetWeapon(int index, out WeaponDataSO data)
        {
            if (index >= weaponDatas.Length)
            {
                data = null;
                return false;
            }

            data = weaponDatas[index];
            return true;
        }

        public bool TryGetEmptyIndex(out int index)
        {
            for (int i = 0; i < weaponDatas.Length; i++)
            {
                if (weaponDatas[i] != null) continue;
                index = i;
                return true;
            }

            index = -1;
            return false;
        }

        public WeaponSwapChoice[] GetWeaponSwapChoices()
        {
            var choice = new WeaponSwapChoice[weaponDatas.Length];

            for (int i = 0; i < weaponDatas.Length; i++)
            {
                var data = weaponDatas[i];
                choice[i] = new WeaponSwapChoice(data, i);

            }

            return choice;
        }
    }
}*/