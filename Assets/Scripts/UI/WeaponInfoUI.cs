using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WeaponInfoUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image weaponIcon;

        [SerializeField] TMP_Text weaponName;
        [SerializeField] TMP_Text weaponDescription;

        private WeaponDataSO weaponData;

        public void PopulateUI(WeaponDataSO data)
        {
            if (data == null) return;

            weaponData = data;

            weaponIcon.sprite = weaponData.Icon;
            weaponName.SetText(weaponData.Name);
            weaponDescription.SetText(weaponData.Description);
        }

    }
}