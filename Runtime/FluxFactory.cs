using System;
using EasyToolkit.Fluxion.Core;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public static class FluxFactory
    {
        public static IFlow<TValue> To<TValue>(
            FluxValueGetter<TValue> valueGetter,
            FluxValueSetter<TValue> valueSetter,
            TValue endValue,
            float duration,
            IFluxContext customContext = null)
        {
            var flow = new Core.Implementations.Flow<TValue>();
            flow.Apply(valueGetter, valueSetter, endValue);
            flow.SetDuration(duration);
            if (Application.isPlaying)
            {
                flow.Context = customContext ?? FluxEngine.Instance;
            }
            else
            {
                flow.Context = customContext ?? MockFluxEngine.Instance;
            }
            return flow;
        }

        public static IFluxSequence Sequence(IFluxContext customContext = null)
        {
            var sequence = new Core.Implementations.FluxSequence();
            if (Application.isPlaying)
            {
                sequence.Context = customContext ?? FluxEngine.Instance;
            }
            else
            {
                sequence.Context = customContext ?? MockFluxEngine.Instance;
            }
            return sequence;
        }

        public static IFluxCallback Callback(Action callback, IFluxContext customContext = null)
        {
            var flux = new Core.Implementations.FluxCallback();
            flux.Callback += callback;
            if (Application.isPlaying)
            {
                flux.Context = customContext ?? FluxEngine.Instance;
            }
            else
            {
                flux.Context = customContext ?? MockFluxEngine.Instance;
            }
            return flux;
        }

        /// <summary>
        /// 创建一个指定持续时间的间隔
        /// </summary>
        /// <param name="duration">间隔时间（秒）</param>
        /// <returns>间隔Flux对象</returns>
        public static IFluxInterval Interval(float duration, IFluxContext customContext = null)
        {
            IFluxInterval interval = new Core.Implementations.FluxInterval();
            interval.Duration = duration;
            if (Application.isPlaying)
            {
                interval.Context = customContext ?? FluxEngine.Instance;
            }
            else
            {
                interval.Context = customContext ?? MockFluxEngine.Instance;
            }
            return interval;
        }

        /// <summary>
        /// 通过ID获取Flux实例
        /// </summary>
        /// <param name="id">Flux的ID</param>
        /// <returns>如果找到则返回对应的Flux实例，否则返回null</returns>
        public static IFlux GetFluxById(string id, IFluxContext customContext = null)
        {
            return (customContext ?? FluxEngine.Instance).Registry.GetFluxById(id);
        }
    }
}
