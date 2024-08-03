using System;
using UnityEngine;

namespace Game.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;
        public event Action<float> OnValueChange;
        [field: SerializeField] public float MaxValue { get; private set; }
        public float CurrentValue
        {
            get => currentValue; private set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);
                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }

        private float currentValue;

        public void Init() => currentValue = MaxValue;
        public void Increase(float amount)
        {
            CurrentValue += amount;
            OnValueChange?.Invoke(amount);
        }
        public void Decrease(float amount)
        {
            CurrentValue -= amount;
            OnValueChange?.Invoke(amount);
        }

    }
}