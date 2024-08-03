using UnityEngine;
namespace Game.Weapons.Components
{
    public class InputHold : WeaponComponent
    {
        private Animator anim;
        private bool input;
        private bool minHoldPassed;

        protected override void HandlerEnter()
        {
            base.HandlerEnter();
            minHoldPassed = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            input = newInput;
            SetAnimatorParameter();
        }

        private void HandleMinHoldPassed()
        {
            minHoldPassed = true;
            SetAnimatorParameter();
        }

        private void SetAnimatorParameter()
        {
            if (input)
            {
                anim.SetBool("Hold", input);
                return;
            }

            if (minHoldPassed)
            {
                anim.SetBool("Hold", false);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponentInChildren<Animator>();
            weapon.OnCurrentInputChange += HandleCurrentInputChange;
            AnimationEventHandler.OnMinHoldPassed += HandleMinHoldPassed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
            AnimationEventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
        }

    }
}