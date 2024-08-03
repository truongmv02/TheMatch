using System;
using UnityEngine;
namespace Game.Weapons.Components
{
    public class Draw : WeaponComponent<DrawData, AttackDraw>
    {
        public event Action<float> OnEvaluateCurve;
        private bool hasEvaluateDraw;
        private float drawPercentage;

        protected override void HandlerEnter()
        {
            base.HandlerEnter();

            hasEvaluateDraw = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasEvaluateDraw) return;

            EvaluateDrawPercentage();
        }

        private void EvaluateDrawPercentage()
        {
            hasEvaluateDraw = true;
            drawPercentage = currentAttackData.DrawCurve.Evaluate(
                Mathf.Clamp((Time.time - attackStartTime) / currentAttackData.DrawTime, 0f, 1f)
            );
            OnEvaluateCurve?.Invoke(drawPercentage);

        }

        protected override void Awake()
        {
            base.Awake();
            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        }
    }
}