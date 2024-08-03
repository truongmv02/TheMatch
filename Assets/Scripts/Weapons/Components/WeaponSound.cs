using UnityEngine;
using Game.Weapons.Components;
using System.Linq;

namespace Game.Weapons.Components
{
    public class WeaponSound : WeaponComponent<WeaponSoundData, AttackSound>
    {

        private AudioClip clip;

        protected override void Start()
        {
            base.Start();

            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
            AnimationEventHandler.OnPlayMusic += PlaySound;
        }

        public void PlaySound()
        {
            if (clip != null)
                SoundManager.Instance.PlaySound(clip);
        }

        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            clip = currentAttackData.AttackSounds.FirstOrDefault(data => data.Phase == phase).Clip;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            AnimationEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
            AnimationEventHandler.OnPlayMusic -= PlaySound;

        }
    }
}
