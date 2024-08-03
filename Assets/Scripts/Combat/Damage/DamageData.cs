using UnityEngine;

namespace Game.Combat.Damage
{
    public class DamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public DamageData(float amount, GameObject source)
        {
            this.Amount = amount;
            this.Source = source;
        }

        public void SetAmount(float amount)
        {
            this.Amount = amount;
        }

    }
}