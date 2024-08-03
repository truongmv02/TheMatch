using UnityEngine;

namespace Game.CoreSystem
{
    public class CoreComp<T> where T : CoreComponent
    {
        private Core core;
        private T comp;
        public T Comp => comp ? comp : core.GetCoreComponent<T>(ref comp);

        public CoreComp(Core core)
        {
            if (core == null)
            {
                Debug.LogWarning("Core is null for component " + typeof(T));
            }
            this.core = core;
        }
    }
}