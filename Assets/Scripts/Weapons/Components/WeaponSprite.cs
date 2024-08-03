using UnityEngine;
using Game.Weapons.Components;
using System.Linq;

namespace Game.Weapons.Components
{
    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        private int currentWeaponSpriteIndex;
        private Sprite[] currentPhaseSprites;

        protected override void Start()
        {
            base.Start();

            baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
            baseSpriteRenderer.RegisterSpriteChangeCallback(HandlerBaseSpriteChange);
            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
            // data = weapon.Data.GetData<WeaponSpriteData>();

        }

        protected override void HandlerEnter()
        {
            base.HandlerEnter();
            currentWeaponSpriteIndex = 0;
        }

        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            currentWeaponSpriteIndex = 0;

            currentPhaseSprites = currentAttackData.PhaseSprites.FirstOrDefault(data => data.Phase == phase).Sprites;
        }

        private void HandlerBaseSpriteChange(SpriteRenderer sr)
        {
            if (!isAttackActive)
            {
                weaponSpriteRenderer.sprite = null;
                return;
            }
            if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
            {
                Debug.Log($"{weapon.name} weapon sprites lenght mismatch");
                return;
            }
            weaponSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
            currentWeaponSpriteIndex++;
        }



        protected override void OnDestroy()
        {
            base.OnDestroy();
            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandlerBaseSpriteChange);
            AnimationEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
        }
    }
}
