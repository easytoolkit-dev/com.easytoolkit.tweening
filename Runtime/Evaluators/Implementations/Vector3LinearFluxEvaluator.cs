using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public class Vector3LinearFluxEvaluator : FluxEvaluatorBase<Vector3, LinearFluxProfile>
    {
        public override Vector3 GetRelativeValue(Vector3 value, Vector3 relative)
        {
            return value + relative;
        }

        private float _distance;
        private Vector3 _direction;

        public override void Initialize()
        {
            _distance = Vector3.Distance(Context.StartValue, Context.EndValue);
            _direction = (Context.EndValue - Context.StartValue).normalized;
        }

        public override float GetDistance()
        {
            return _distance;
        }

        public override Vector3 Process(float normalizedTime)
        {
            var curDist = Mathf.Lerp(0, _distance, normalizedTime);
            return Context.StartValue + curDist * _direction;
        }
    }
}
