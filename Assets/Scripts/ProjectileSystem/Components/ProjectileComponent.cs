using System.Collections;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.ProjectileSystem.Components
{
    public class ProjectileComponent : MonoBehaviour
    {
        protected Projectile projectile;
        protected Rigidbody2D rb => projectile.Rigidbody2D;

        public bool Active { get; private set; }

        public virtual void SetActive(bool value) => Active = value;

        public virtual void SetActiveNextFrame(bool value)
        {
            StartCoroutine(SetActiveNextFrameCoroutine(value));
        }

        public IEnumerator SetActiveNextFrameCoroutine(bool value)
        {
            yield return null;
            SetActive(value);
        }
        protected virtual void Init()
        {
            SetActive(true);
        }

        protected virtual void ResetProjectile() { }

        protected virtual void Awake()
        {
            projectile = GetComponent<Projectile>();
            projectile.OnInit += Init;
            projectile.OnReset += ResetProjectile;
            projectile.OnReveiceDataPackage += HandleReceiveDataPackage;

        }

        protected virtual void Start() { }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        protected virtual void OnDestroy()
        {
            projectile.OnInit -= Init;
            projectile.OnInit -= ResetProjectile;
            projectile.OnReveiceDataPackage -= HandleReceiveDataPackage;
        }

        protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage) { }
    }
}