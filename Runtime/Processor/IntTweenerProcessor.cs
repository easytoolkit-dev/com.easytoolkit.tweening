using System;
using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Tweening
{
    public class IntLinearTweenerProcessor : AbstractTweenerProcessor<int, LinearTweenerEffect>
    {
        protected override int GetRelativeValue(int value, int relative)
        {
            return value + relative;
        }

        protected override float GetDistance()
        {
            return (Context.EndValue - Context.StartValue).Abs();
        }

        protected override int OnProcess(float normalizedTime)
        {
            return Mathf.RoundToInt(Mathf.Lerp(Context.StartValue, Context.EndValue, normalizedTime));
        }
    }
}
