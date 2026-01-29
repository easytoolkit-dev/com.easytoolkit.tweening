using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class FloatLinearFluxEvaluator : AbstractFluxEvaluator<float, LinearFluxProfile>
    {
        protected override float GetRelativeValue(float value, float relative)
        {
            return value + relative;
        }

        protected override float GetDistance()
        {
            return (Context.EndValue - Context.StartValue).Abs();
        }

        protected override float OnProcess(float normalizedTime)
        {
            return Mathf.Lerp(Context.StartValue, Context.EndValue, normalizedTime);
        }
    }
}
