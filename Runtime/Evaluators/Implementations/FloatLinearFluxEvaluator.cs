using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public class FloatLinearFluxEvaluator : FluxEvaluatorBase<float, LinearFluxProfile>
    {
        public override float GetRelativeValue(float value, float relative)
        {
            return value + relative;
        }

        public override float GetDistance()
        {
            return (Context.EndValue - Context.StartValue).Abs();
        }

        public override float Process(float normalizedTime)
        {
            return Mathf.Lerp(Context.StartValue, Context.EndValue, normalizedTime);
        }
    }
}
