using System;
using EasyToolkit.Core.Textual;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public static class FluxFactory
    {
        public static Flow To(Type valueType, FluxValueGetter valueGetter, FluxValueSetter valueSetter, object endValue,
            float duration)
        {
            var flow = new Flow();
            flow.Apply(valueType, valueGetter, valueSetter, endValue);
            flow.SetDuration(duration);

            // Explicitly attach to engine
            FluxEngine.Instance.Attach(flow);

            return flow;
        }

        public static Flow To<T>(FluxValueGetter<T> valueGetter, FluxValueSetter<T> valueSetter, T endValue, float duration)
        {
            return To(typeof(T), () => valueGetter(), val => valueSetter((T)val), endValue, duration);
        }

        public static FluxSequence Sequence()
        {
            var sequence = new FluxSequence();
            FluxEngine.Instance.Attach(sequence);
            return sequence;
        }

        public static FluxCallback Callback(Action callback)
        {
            var flux = new FluxCallback().AddCallback(callback);
            FluxEngine.Instance.Attach(flux);
            return flux;
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
            FluxEngine.Instance.Attach(interval);
            return interval;
        }

        /// <summary>
        /// 通过ID获取Flux实例
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到则返回对应的Flux实例，否则返回null</returns>
        public static IFlux GetById(string id)
        {
            return FluxEngine.Instance.GetFluxById(id);
        }

        internal static void RegisterFluxById(string id, IFluxEntity flux)
        {
            FluxEngine.Instance.RegisterFluxById(id, flux);
        }

        internal static void UnregisterFluxById(string id)
        {
            FluxEngine.Instance.UnregisterFluxById(id);
        }
    }
}
