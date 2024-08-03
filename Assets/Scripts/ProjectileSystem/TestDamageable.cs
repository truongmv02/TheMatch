using Game.Combat.Damage;
using UnityEngine;

namespace Game.ProjectileSystem
{
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(DamageData data)
        {
            print(gameObject.name + " Damaged: " + data.Amount);
        }
    }
}