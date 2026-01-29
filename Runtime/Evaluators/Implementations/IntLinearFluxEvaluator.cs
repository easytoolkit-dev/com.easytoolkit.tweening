using System;
using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
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
