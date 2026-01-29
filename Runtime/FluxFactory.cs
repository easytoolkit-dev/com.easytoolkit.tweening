using System;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion
{
    public static class FluxFactory
    {
        public static IFlow<TValue> To<TValue>(
            FluxValueGetter<TValue> valueGetter,
            FluxValueSetter<TValue> valueSetter,
            TValue endValue,
            float duration)
        {
            var flow = new Core.Implementations.Flow<TValue>();
            flow.Apply(valueGetter, valueSetter, endValue);
            flow.SetDuration(duration);

            // Explicitly attach to engine
            FluxEngine.Instance.Attach(flow);

            return flow;
        }

        public static IFluxSequence Sequence()
        {
            var sequence = new Core.Implementations.FluxSequence();
            FluxEngine.Instance.Attach(sequence);
            return sequence;
        }

        public static IFluxCallback Callback(Action callback)
        {
            var flux = new Core.Implementations.FluxCallback();
            flux.Callback += callback;
            FluxEngine.Instance.Attach(flux);
            return flux;
        }

        /// <summary>
        /// 创建一个指定持续时间的间隔
        /// </summary>
        /// <param name="duration">间隔时间（秒）</param>
        /// <returns>间隔Flux对象</returns>
        public static IFluxInterval Interval(float duration)
        {
            IFluxInterval interval = new Core.Implementations.FluxInterval();
            interval.Duration = duration;
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
