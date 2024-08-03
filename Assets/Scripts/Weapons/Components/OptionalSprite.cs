using UnityEngine;

namespace Game.Weapons.Components
{
    public class OptionalSprite : WeaponComponent<OptionalSpriteData, AttackOptionalSprite>
    {
        private SpriteRenderer spriteRenderer;

        private void HandleSetOptionalSpriteActive(bool value)
        {
            spriteRenderer.enabled = value;
        }

        protected override void HandlerEnter()
        {
            base.HandlerEnter();
            if (!currentAttackData.UseOptionalSprite) return;
            spriteRenderer.sprite = currentAttackData.Sprite;
        }

        protected override void Awake()
        {
            base.Awake();

            spriteRenderer = GetComponentInChildren<OptionalSpriteMarket>().SpriteRenderer;
            spriteRenderer.enabled = false;
        }

        protected override void Start()
        {
            base.Start();
            AnimationEventHandler.OnSetOptionalSpriteActive += HandleSetOptionalSpriteActive;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            AnimationEventHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
        }
    }
}