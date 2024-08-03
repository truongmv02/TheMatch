using UnityEngine;

namespace Game.Combat.Parry
{
    public class ParryData
    {
        public GameObject Source { get; private set; }

        public ParryData(GameObject source)
        {
            this.Source = source;
        }
    }
}