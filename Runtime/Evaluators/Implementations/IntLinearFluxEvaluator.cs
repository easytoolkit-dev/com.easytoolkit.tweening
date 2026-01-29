using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public class IntLinearFluxEvaluator : FluxEvaluatorBase<int, LinearFluxProfile>
    {
        public override int GetRelativeValue(int value, int relative)
        {
            return value + relative;
        }

        public override float GetDistance()
        {
            return (Context.EndValue - Context.StartValue).Abs();
        }

        public override int Process(float normalizedTime)
        {
            return Mathf.RoundToInt(Mathf.Lerp(Context.StartValue, Context.EndValue, normalizedTime));
        }
    }
}
