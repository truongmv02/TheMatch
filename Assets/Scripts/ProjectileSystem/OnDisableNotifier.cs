using System;
using UnityEngine;

namespace Game.ProjectileSystem
{
    public class OnDisableNotifier : MonoBehaviour
    {
        public event Action OnDisableEvent;

        private void OnDisable() {
            OnDisableEvent?.Invoke();
        }

        [ContextMenu("Test")]
        private void Test(){
            gameObject.SetActive(false);
        }
    }
}