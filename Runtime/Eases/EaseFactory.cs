using System;
using UnityEngine;

namespace EasyToolkit.Fluxion.Eases
{
    /// <summary>
    /// Factory for creating atomic easing function instances.
    /// </summary>
    public static class EaseFactory
    {
        /// <summary>
        /// Creates a generic ease from a custom function.
        /// </summary>
        /// <param name="easeFunc">The easing function that takes a normalized time (0-1) and returns an eased time value.</param>
        /// <returns>An ease function based on the provided custom function.</returns>
        public static IFlowEase Generic(Func<float, float> easeFunc)
            => new Implementations.GenericFlowEase(easeFunc);

        /// <summary>
        /// Creates an ease-in exponential curve.
        /// </summary>
        /// <param name="pow">The power exponent for the curve.</param>
        /// <returns>An ease-in exponential function with the specified power.</returns>
        public static IFlowEase InExponential(float pow)
            => new Implementations.InExponentialFlowEase(pow);

        /// <summary>
        /// Creates an ease-out exponential curve.
        /// </summary>
        /// <param name="pow">The power exponent for the curve.</param>
        /// <returns>An ease-out exponential function with the specified power.</returns>
        public static IFlowEase OutExponential(float pow)
            => new Implementations.OutExponentialFlowEase(pow);

        /// <summary>
        /// Creates an ease-in-out exponential curve.
        /// </summary>
        /// <param name="pow">The power exponent for the curve.</param>
        /// <returns>An ease-in-out exponential function with the specified power.</returns>
        public static IFlowEase InOutExponential(float pow)
            => new Implementations.InOutExponentialFlowEase(pow);
    }
}
