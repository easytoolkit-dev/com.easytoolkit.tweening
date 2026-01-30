using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class InOutExponentialFlowEase : IFlowEase
    {
        private readonly float _power;

        public InOutExponentialFlowEase(float pow)
        {
            _power = pow;
        }

        float IFlowEase.EaseTime(float time)
        {
            return time < 0.5f
                ? Mathf.Pow(2f * time, _power) / 2f
                : 1f - Mathf.Pow(2f - 2f * time, _power) / 2f;
        }
    }
}
