using System;

namespace EasyToolkit.Fluxion.Eases.Implementations
{
    internal class GenericFlowEase : IFlowEase
    {
        private readonly Func<float, float> _easeTime;

        internal GenericFlowEase(Func<float, float> easeTime)
        {
            _easeTime = easeTime;
        }

        float IFlowEase.EaseTime(float time)
        {
            return _easeTime(time);
        }
    }
}
