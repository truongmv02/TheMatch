using System;
using UnityEngine;

namespace Game.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        public event Action OnMinHoldPassed;
        public event Action OnUseInput;
        public event Action OnEnableInterrupt;
        public event Action<bool> OnFlipSetActive;
        public event Action<AttackPhases> OnEnterAttackPhase;
        public event Action OnPlayMusic;
        public event Action<bool> OnSetOptionalSpriteActive;
        public event Action<AnimationWindows> OnStartAnimationWindow;
        public event Action<AnimationWindows> OnStopAnimationWindow;

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void MinHoldPassed() => OnMinHoldPassed?.Invoke();
        private void UseInput() => OnUseInput?.Invoke();
        private void EnableInterrupt() => OnEnableInterrupt?.Invoke();
        private void OnAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
        private void PlayMusic() => OnPlayMusic?.Invoke();
        private void SetOptionalSpriteEnable() => OnSetOptionalSpriteActive?.Invoke(true);
        private void SetOptionalSpriteDisable() => OnSetOptionalSpriteActive?.Invoke(false);
        private void SetFlipActive() => OnFlipSetActive?.Invoke(true);
        private void SetFlipInactive() => OnFlipSetActive?.Invoke(false);
        private void StartAnimationWindown(AnimationWindows window) => OnStartAnimationWindow?.Invoke(window);
        private void StopAnimationWindow(AnimationWindows window) => OnStopAnimationWindow?.Invoke(window);
    }
}