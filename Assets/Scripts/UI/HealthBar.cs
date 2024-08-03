using Game.CoreSystem;
using Game.CoreSystem.StatsSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {

        public Stats stats;
        public Image progress;
        private Stat health;

        void Start()
        {
            health = stats.Health;
            health.OnValueChange += OnHealthChange;
            OnHealthChange(0);
        }

        void Update()
        {

        }
        private void OnHealthChange(float value)
        {
            var fillAmount = health.CurrentValue / health.MaxValue;
            progress.fillAmount = fillAmount;
        }

    }
}