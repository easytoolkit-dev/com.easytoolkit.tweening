using System;

namespace EasyToolkit.Fluxion
{
    public class FluxInterval : FluxBase
    {
        private float _duration;
        public override float? Duration => _duration;

        internal void SetDuration(float duration)
        {
            _duration = duration;
        }

        protected override void OnPlaying(float time)
        {
        }
    }
}
