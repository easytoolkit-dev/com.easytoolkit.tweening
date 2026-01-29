using EasyToolkit.Fluxion.Core;
using EasyToolkit.Fluxion.Eases;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowExtensions
    {
        public static bool IsPlaying(this IFlux flux)
        {
            return flux.CurrentState == FluxState.Playing || flux.CurrentState == FluxState.DelayAfterPlay;
        }

        public static bool IsActive(this IFlux flux)
        {
            return flux.IsPlaying() || flux.CurrentState == FluxState.Idle;
        }

        /// <summary>
        /// 设置所属的unity对象，当对象销毁时也会停止该flow
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="unityObject"></param>
        /// <returns></returns>
        public static T SetUnityObject<T>(this T flow, UnityEngine.Object unityObject) where T : IFlow
        {
            flow.UnityObject = unityObject;
            return flow;
        }

        public static T SetEase<T>(this T flow, IFlowEase ease) where T : IFlow
        {
            flow.Ease = ease;
            return flow;
        }

        public static T SetRelative<T>(this T flow, bool isRelative = true) where T : IFlow
        {
            flow.IsRelative = isRelative;
            return flow;
        }

        /// <summary>
        /// <para>速度模式，将“持续时间”的值变成“速度”，从“起始值”开始每秒增加“速度”直到“结束值”。</para>
        /// <para>注意：使用此模式后，FluxFactory.Duration()将返回null，除非该flow的第一帧被调用了。</para>
        /// </summary>
        public static T SetSpeedBased<T>(this T flow, bool isSpeedBased = true) where T : IFlow
        {
            flow.IsSpeedBased = isSpeedBased;
            return flow;
        }

        public static T SetLoopType<T>(this T flow, LoopType loopType) where T : IFlow
        {
            flow.LoopType = loopType;
            return flow;
        }
    }
}
