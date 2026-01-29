using System;

namespace EasyToolkit.Fluxion
{
    public static class FluxExtensions
    {
        public static T SetId<T>(this T tween, string id) where T : IFlux
        {
            // Update the ID via the Factory to handle registration/unregistration
            if (tween is IFluxEntity entity && entity.Id != id)
            {
                FluxFactory.UnregisterFluxById(entity.Id);
                entity.Id = id;
                FluxFactory.RegisterFluxById(id, entity);
            }
            else
            {
                tween.Id = id;
            }

            return tween;
        }

        public static T OnPlay<T>(this T tween, Action callback) where T : IFlux
        {
            tween.AddPlayCallback(_ => callback());
            return tween;
        }

        public static T OnPause<T>(this T tween, Action callback) where T : IFlux
        {
            tween.AddPauseCallback(_ => callback());
            return tween;
        }

        public static T OnComplete<T>(this T tween, Action callback) where T : IFlux
        {
            tween.AddCompleteCallback(_ => callback());
            return tween;
        }


        public static T OnKill<T>(this T tween, Action callback) where T : IFlux
        {
            tween.AddKillCallback(_ => callback());
            return tween;
        }

        public static T SetDelay<T>(this T tween, float delay) where T : IFlux
        {
            tween.Delay = delay;
            return tween;
        }

        public static T SetLoopCount<T>(this T tween, int loopCount) where T : IFlux
        {
            tween.LoopCount = loopCount;
            return tween;
        }

        public static T SetInfiniteLoop<T>(this T tween, bool infiniteLoop = true) where T : IFlux
        {
            tween.InfiniteLoop = infiniteLoop;
            return tween;
        }
    }
}
