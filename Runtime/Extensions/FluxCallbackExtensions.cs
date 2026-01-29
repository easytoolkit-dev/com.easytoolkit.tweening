using System;

namespace EasyToolkit.Fluxion
{
    public static class FluxCallbackExtensions
    {
        public static FluxCallback AddCallback(this FluxCallback tweenCallback, Action callback)
        {
            tweenCallback.AddCallback(callback);
            return tweenCallback;
        }

        public static FluxCallback RemoveCallback(this FluxCallback tweenCallback, Action callback)
        {
            // FluxCallback property Callback has get/set.
            // We can just manipulate the delegate.
            tweenCallback.Callback -= callback;
            return tweenCallback;
        }
    }
}
