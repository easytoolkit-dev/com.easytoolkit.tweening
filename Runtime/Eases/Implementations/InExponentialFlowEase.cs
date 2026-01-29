using UnityEngine;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class InExponentialFlowEase : IFlowEase
    {
        private float _power = 2;

        public InExponentialFlowEase SetPow(float pow)
        {
            _power = pow;
            return this;
        }

        float IFlowEase.EaseTime(float time)
        {
            return Mathf.Pow(time, _power);
        }
    }
}
