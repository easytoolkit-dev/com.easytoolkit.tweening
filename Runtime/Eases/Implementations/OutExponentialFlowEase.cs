using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class OutExponentialFlowEase : IFlowEase
    {
        private float _power = 2;

        public OutExponentialFlowEase SetPow(float pow)
        {
            _power = pow;
            return this;
        }

        float IFlowEase.EaseTime(float time)
        {
            return 1f - Mathf.Pow(1f - time, _power);
        }
    }
}
