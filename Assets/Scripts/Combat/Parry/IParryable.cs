using UnityEngine;
namespace Game.Combat.Parry
{
    public interface IParryable
    {
        void Parry(ParryData data);
    }
}