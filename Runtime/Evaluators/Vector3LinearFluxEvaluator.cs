using EasyToolkit.Core;
using System;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class Vector3LinearFluxEvaluator : AbstractFluxEvaluator<Vector3, LinearFluxProfile>
    {
        protected override Vector3 GetRelativeValue(Vector3 value, Vector3 relative)
        {
            return value + relative;
        }

        private float _distance;
        private Vector3 _direction;
        protected override void OnInit()
        {
            _distance = Vector3.Distance(Context.StartValue, Context.EndValue);
            _direction = (Context.EndValue - Context.StartValue).normalized;
        }

        protected override float GetDistance()
        {
            return _distance;
        }

        protected override Vector3 OnProcess(float normalizedTime)
        {
            var curDist = Mathf.Lerp(0, _distance, normalizedTime);
            return Context.StartValue + curDist * _direction;
        }
    }
}
