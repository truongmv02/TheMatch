using System.Collections;
using UnityEngine;
namespace Game.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        private Stats stats;
        private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);

        private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
        private ParticleManager particleManager;


        public void Die()
        {
            foreach (var item in deathParticles)
            {
                ParticleManager.StartParticles(item);
            }
            StaticEvent.CallPlayerDieEvent(core.Root.GetComponent<Player>());
            core.transform.parent.gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}