using System;
using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class Vector2LinearFluxEvaluator : AbstractFluxEvaluator<Vector2, LinearFluxProfile>
    {
        protected override Vector2 GetRelativeValue(Vector2 value, Vector2 relative)
        {
            return value + relative;
        }

        private float _distance;
        private Vector2 _direction;
        protected override void OnInit()
        {
            _distance = Vector2.Distance(Context.StartValue, Context.EndValue);
            _direction = (Context.EndValue - Context.StartValue).normalized;
        }

        protected override float GetDistance()
        {
            return _distance;
        }

        protected override Vector2 OnProcess(float normalizedTime)
        {
            var curDist = Mathf.Lerp(0, _distance, normalizedTime);
            return Context.StartValue + curDist * _direction;
        }
    }
}
