using System;
using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.ProjectileSystem
{
    public class Projectile : MonoBehaviour
    {
        public event Action OnInit;
        public event Action OnReset;
        public event Action<ProjectileDataPackage> OnReveiceDataPackage;
        public Rigidbody2D Rigidbody2D { get; private set; }

        public Transform owner;


        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Reset()
        {
            OnReset?.Invoke();
        }

        public void Init()
        {
            OnInit?.Invoke();
        }

        public void SendDataPackage(ProjectileDataPackage dataPackage)
        {
            OnReveiceDataPackage?.Invoke(dataPackage);
        }
    }
}