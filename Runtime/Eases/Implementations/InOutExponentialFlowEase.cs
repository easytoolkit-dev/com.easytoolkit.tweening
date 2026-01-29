using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class InOutExponentialFlowEase : IFlowEase
    {
        private float _power = 2;

        public InOutExponentialFlowEase SetPow(float pow)
        {
            _power = pow;
            return this;
        }

        float IFlowEase.EaseTime(float time)
        {
            return time < 0.5f
                ? Mathf.Pow(2f * time, _power) / 2f
                : 1f - Mathf.Pow(2f - 2f * time, _power) / 2f;
        }
    }
}
