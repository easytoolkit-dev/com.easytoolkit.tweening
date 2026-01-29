using System;
using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class Vector2LinearFluxEvaluator : FluxEvaluatorBase<Vector2, LinearFluxProfile>
    {
        public override Vector2 GetRelativeValue(Vector2 value, Vector2 relative)
        {
            return value + relative;
        }

        private float _distance;
        private Vector2 _direction;
        public override void Initialize()
        {
            _distance = Vector2.Distance(Context.StartValue, Context.EndValue);
            _direction = (Context.EndValue - Context.StartValue).normalized;
        }

        public override float GetDistance()
        {
            return _distance;
        }

        public override Vector2 Process(float normalizedTime)
        {
            var curDist = Mathf.Lerp(0, _distance, normalizedTime);
            return Context.StartValue + curDist * _direction;
        }
    }
}
