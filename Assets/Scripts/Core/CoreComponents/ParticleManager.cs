using UnityEngine;
namespace Game.CoreSystem
{
    public class ParticleManager : CoreComponent
    {
        private Transform particleContainer;
        private Movement movement;

        protected override void Awake()
        {
            base.Awake();

            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }

        void Start()
        {
            movement = core.GetCoreComponent<Movement>();
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
        {
            return Instantiate(particlePrefab, position, rotation, particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab)
        {
            return Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }

        public GameObject StartWithRandomRotation(GameObject particlePrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlePrefab, transform.position, randomRotation);
        }

        public GameObject StartWithRandomRotation(GameObject particlePrefab, Vector2 offset)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlePrefab, FindRelativePoint(offset), randomRotation);
        }


        public GameObject StartParticleRelative(GameObject paritclePrefab, Vector2 offset, Quaternion rotation)
        {
            var pos = FindRelativePoint(offset);
            return StartParticles(paritclePrefab, pos, rotation);
        }

        public Vector2 FindRelativePoint(Vector2 offset) => movement.FindRelativePoint(offset);
    }
}