using System;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public static class FluxFactroy
    {
        public static Flow To(Type valueType, FluxGetter getter, FluxSetter setter, object endValue,
            float duration)
        {
            var flow = new Flow();
            flow.Apply(valueType, getter, setter, endValue);
            flow.SetDuration(duration);
            return flow;
        }

        public static Flow To<T>(FluxGetter<T> getter, FluxSetter<T> setter, T endValue, float duration)
        {
            return To(typeof(T), () => getter(), val => setter((T)val), endValue, duration);
        }

        public static FluxSequence Sequence()
        {
            return new FluxSequence();
        }

        public static FluxCallback Callback(Action callback)
        {
            return new FluxCallback().AddCallback(callback);
        }

        /// <summary>
        /// 创建一个指定持续时间的间隔
        /// </summary>
        /// <param name="duration">间隔时间（秒）</param>
        /// <returns>间隔Flux对象</returns>
        public static FluxInterval Interval(float duration)
        {
            var interval = new FluxInterval();
            interval.SetDuration(duration);
            return interval;
        }

        /// <summary>
        /// <para>获取持续时间，返回null代表此时无法判断具体持续时间。</para>
        /// <para>如果是FluxSequence，只有运行完成了一次，才能确定那一次运行持续的时间，否则将始终返回null。</para>
        /// </summary>
        /// <param name="flux"></param>
        /// <returns></returns>
        public static float? Duration(AbstractFlux flux)
        {
            return flux.GetActualDuration();
        }

        /// <summary>
        /// <para>通过ID获取Flux的持续时间，返回null代表此时无法判断具体持续时间。</para>
        /// <para>如果是FluxSequence，只有运行完成了一次，才能确定那一次运行持续的时间，否则将始终返回null。</para>
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到对应的Flux则返回其持续时间，否则返回null</returns>
        public static float? Duration(string id)
        {
            var flux = GetById(id);
            return flux?.GetActualDuration();
        }

        public static bool IsPlaying(AbstractFlux flux)
        {
            return flux.CurrentState == FluxState.Playing || flux.CurrentState == FluxState.DelayAfterPlay;
        }

        /// <summary>
        /// 通过ID检查Flux是否正在播放
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到对应的Flux且正在播放则返回true，否则返回false</returns>
        public static bool IsPlaying(string id)
        {
            var flux = GetById(id);
            return flux != null && IsPlaying(flux);
        }

        public static bool IsActive(AbstractFlux flux)
        {
            return IsPlaying(flux) || flux.CurrentState == FluxState.Idle;
        }

        /// <summary>
        /// 通过ID检查Flux是否处于活动状态
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到对应的Flux且处于活动状态则返回true，否则返回false</returns>
        public static bool IsActive(string id)
        {
            var flux = GetById(id);
            return flux != null && IsActive(flux);
        }

        public static void Kill(AbstractFlux flux)
        {
            flux.PendingKillSelf = true;
        }

        /// <summary>
        /// 通过ID终止Flux
        /// </summary>
        /// <param name="id">Flux的ID</param>
        public static void Kill(string id)
        {
            var flux = GetById(id);
            if (flux != null)
            {
                Kill(flux);
            }
        }

        /// <summary>
        /// 通过ID获取Flux实例
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到则返回对应的Flux实例，否则返回null</returns>
        public static AbstractFlux GetById(string id)
        {
            return FluxEngine.Instance.GetFluxById(id);
        }
    }
}
