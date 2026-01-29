using System;

namespace EasyToolkit.Fluxion
{
    public class FluxCallback : FluxBase
    {
        public override float? Duration => null;

        internal Action Callback { get; set; }

        public FluxCallback AddCallback(Action callback)
        {
            Callback += callback;
            return this;
        }

        protected override void OnStart()
        {
            Callback?.Invoke();
        }

        protected override void OnPlaying(float time)
        {
            Complete();
        }
    }
}
