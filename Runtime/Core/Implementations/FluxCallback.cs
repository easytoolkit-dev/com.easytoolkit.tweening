using System;

namespace EasyToolkit.Fluxion.Core.Implementations
{
    internal class FluxCallback : FluxBase, IFluxCallback
    {
        public override float? Duration => null;

        public event Action Callback;

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
