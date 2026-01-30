using System;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FluxExtensions
    {
        public static T WithId<T>(this T flux, string id) where T : IFlux
        {
            FluxEngine.Instance.UnregisterFluxById(flux.Id);
            flux.Id = id;
            FluxEngine.Instance.RegisterFluxById(id, flux);

            return flux;
        }

        public static T OnPlay<T>(this T flux, Action callback) where T : IFlux
        {
            flux.Played += _ => callback();
            return flux;
        }

        public static T OnPause<T>(this T flux, Action callback) where T : IFlux
        {
            flux.Paused += _ => callback();
            return flux;
        }

        public static T OnComplete<T>(this T flux, Action callback) where T : IFlux
        {
            flux.Completed += _ => callback();
            return flux;
        }


        public static T OnKill<T>(this T flux, Action callback) where T : IFlux
        {
            flux.Killed += _ => callback();
            return flux;
        }

        public static T WithDelay<T>(this T flux, float delay) where T : IFlux
        {
            flux.Delay = delay;
            return flux;
        }

        public static T WithLoopCount<T>(this T flux, int loopCount) where T : IFlux
        {
            flux.LoopCount = loopCount;
            return flux;
        }

        public static T WithInfiniteLoop<T>(this T flux, bool infiniteLoop = true) where T : IFlux
        {
            flux.InfiniteLoop = infiniteLoop;
            return flux;
        }
    }
}
