using System;

namespace EasyToolkit.Fluxion
{
    public static class FluxCallbackExtensions
    {
        public static FluxCallback AddCallback(this FluxCallback tweenCallback, Action callback)
        {
            tweenCallback.Callback += callback;
            return tweenCallback;
        }

        public static FluxCallback RemoveCallback(this FluxCallback tweenCallback, Action callback)
        {
            tweenCallback.Callback -= callback;
            return tweenCallback;
        }
    }
}
