using EasyToolkit.Fluxion.Core;
using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    /// <summary>
    /// Extension methods for ProfileBuilder that provide specific profile implementations.
    /// </summary>
    public static class ProfileBuilderExtensions
    {
        /// <summary>
        /// Applies linear profile (constant rate of change).
        /// </summary>
        public static TFlow Linear<TFlow>(this ProfileBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.SetProfile(new LinearFluxProfile());
            return builder.Flow;
        }

        /// <summary>
        /// Applies quadratic Bezier curve profile with specified control point.
        /// </summary>
        /// <param name="builder">The profile builder.</param>
        /// <param name="controlPoint">The control point that defines the curve shape.</param>
        /// <param name="controlPointRelativeTo">Specifies how the control point is interpreted (relative to start, end, or absolute).</param>
        public static TFlow Bezier<TFlow>(this ProfileBuilder<TFlow> builder,
            Vector3 controlPoint,
            BezierControlPointRelativeTo controlPointRelativeTo = BezierControlPointRelativeTo.None)
            where TFlow : class, IFlow
        {
            builder.Flow.SetProfile(new BezierFluxProfile(controlPoint, controlPointRelativeTo));
            return builder.Flow;
        }
    }
}
