using System;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FluxCallbackExtensions
    {
        public static IFluxCallback AddCallback(this IFluxCallback fluxCallback, Action callback)
        {
            fluxCallback.Callback += callback;
            return fluxCallback;
        }

        public static IFluxCallback RemoveCallback(this IFluxCallback fluxCallback, Action callback)
        {
            fluxCallback.Callback -= callback;
            return fluxCallback;
        }
    }
}
