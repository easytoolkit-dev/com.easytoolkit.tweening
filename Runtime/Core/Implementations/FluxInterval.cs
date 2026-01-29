using System;

namespace EasyToolkit.Fluxion.Core.Implementations
{
    internal class FluxInterval : FluxBase, IFluxInterval
    {
        private float _duration;

        public override float? Duration => _duration;

        protected override void OnPlaying(float time)
        {
        }

        float IFluxInterval.Duration
        {
            get => _duration;
            set => _duration = value;
        }
    }
}
