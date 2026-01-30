using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class InExponentialFlowEase : IFlowEase
    {
        private readonly float _power;

        public InExponentialFlowEase(float pow)
        {
            _power = pow;
        }

        float IFlowEase.EaseTime(float time)
        {
            return Mathf.Pow(time, _power);
        }
    }
}
