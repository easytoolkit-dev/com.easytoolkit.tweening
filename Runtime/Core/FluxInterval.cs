using System;

namespace EasyToolkit.Fluxion
{
    public class FluxInterval : AbstractFlux
    {
        private float _duration;
        protected override float? ActualDuration => _duration;

        internal void SetDuration(float duration)
        {
            _duration = duration;
        }

        protected override void OnPlaying(float time)
        {
        }
    }
}
