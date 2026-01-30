using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class OutExponentialFlowEase : IFlowEase
    {
        private readonly float _power;

        public OutExponentialFlowEase(float pow)
        {
            _power = pow;
        }

        float IFlowEase.EaseTime(float time)
        {
            return 1f - Mathf.Pow(1f - time, _power);
        }
    }
}
