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
            float duration,
            IFluxContext context = null)
        {
            var flow = new Core.Implementations.Flow<TValue>();
            flow.Apply(valueGetter, valueSetter, endValue);
            flow.SetDuration(duration);

            flow.Context = context ?? FluxEngine.Instance;
            flow.Context.Lifecycle.Attach(flow);

            return flow;
        }

        public static IFluxSequence Sequence(IFluxContext context = null)
        {
            var sequence = new Core.Implementations.FluxSequence();
            sequence.Context = context ?? FluxEngine.Instance;
            sequence.Context.Lifecycle.Attach(sequence);
            return sequence;
        }

        public static IFluxCallback Callback(Action callback, IFluxContext context = null)
        {
            var flux = new Core.Implementations.FluxCallback();
            flux.Callback += callback;
            flux.Context = context ?? FluxEngine.Instance;
            flux.Context.Lifecycle.Attach(flux);
            return flux;
        }

        /// <summary>
        /// 创建一个指定持续时间的间隔
        /// </summary>
        /// <param name="duration">间隔时间（秒）</param>
        /// <returns>间隔Flux对象</returns>
        public static IFluxInterval Interval(float duration, IFluxContext context = null)
        {
            IFluxInterval interval = new Core.Implementations.FluxInterval();
            interval.Duration = duration;
            interval.Context = context ?? FluxEngine.Instance;
            interval.Context.Lifecycle.Attach(interval);
            return interval;
        }

        /// <summary>
        /// 通过ID获取Flux实例
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到则返回对应的Flux实例，否则返回null</returns>
        public static IFlux GetFluxById(string id, IFluxContext context = null)
        {
            return (context ?? FluxEngine.Instance).Registry.GetFluxById(id);
        }
    }
}
