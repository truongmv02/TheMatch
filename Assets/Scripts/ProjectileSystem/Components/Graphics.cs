using Game.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Game.ProjectileSystem.Components
{
    public class Graphics : ProjectileComponent
    {
        private Sprite sprite;

        private SpriteRenderer spriteRenderer;

        protected override void Init()
        {
            base.Init();
            spriteRenderer.sprite = sprite;
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);
            if (dataPackage is not SpriteDataPackage spriteDataPackage)
                return;

            sprite = spriteDataPackage.Sprite;
        }

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}