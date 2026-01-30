using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using EasyToolkit.Fluxion.Eases;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    /// <summary>
    /// Extension methods for EaseBuilder that provide specific easing function implementations.
    /// </summary>
    public static class EaseBuilderExtensions
    {
        /// <summary>
        /// Applies linear easing (constant speed, no acceleration).
        /// </summary>
        public static TFlow Linear<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => t);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in sine easing (starts slow, gradually accelerates).
        /// </summary>
        public static TFlow InSine<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => 1f - Mathf.Cos((t * Mathf.PI) / 2f));
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out sine easing (starts fast, gradually decelerates).
        /// </summary>
        public static TFlow OutSine<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => Mathf.Sin((t * Mathf.PI) / 2f));
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out sine easing (slow at start and end, fast in middle).
        /// </summary>
        public static TFlow InOutSine<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in quad easing (quadratic, starts slow).
        /// </summary>
        public static TFlow InQuad<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InExponential(2f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out quad easing (quadratic, ends slow).
        /// </summary>
        public static TFlow OutQuad<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.OutExponential(2f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out quad easing (quadratic, slow at start and end).
        /// </summary>
        public static TFlow InOutQuad<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InOutExponential(2f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in cubic easing (cubic, starts slow).
        /// </summary>
        public static TFlow InCubic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InExponential(3f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out cubic easing (cubic, ends slow).
        /// </summary>
        public static TFlow OutCubic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.OutExponential(3f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out cubic easing (cubic, slow at start and end).
        /// </summary>
        public static TFlow InOutCubic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InOutExponential(3f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in quart easing (quartic, starts slow).
        /// </summary>
        public static TFlow InQuart<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InExponential(4f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out quart easing (quartic, ends slow).
        /// </summary>
        public static TFlow OutQuart<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.OutExponential(4f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out quart easing (quartic, slow at start and end).
        /// </summary>
        public static TFlow InOutQuart<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InOutExponential(4f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in quint easing (quintic, starts slow).
        /// </summary>
        public static TFlow InQuint<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InExponential(5f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out quint easing (quintic, ends slow).
        /// </summary>
        public static TFlow OutQuint<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.OutExponential(5f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out quint easing (quintic, slow at start and end).
        /// </summary>
        public static TFlow InOutQuint<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InOutExponential(5f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in exponential easing with specified power.
        /// </summary>
        /// <param name="builder">The ease builder.</param>
        /// <param name="power">The power exponent for the curve.</param>
        public static TFlow InExponential<TFlow>(this EaseBuilder<TFlow> builder, float power) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InExponential(power);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out exponential easing with specified power.
        /// </summary>
        /// <param name="builder">The ease builder.</param>
        /// <param name="power">The power exponent for the curve.</param>
        public static TFlow OutExponential<TFlow>(this EaseBuilder<TFlow> builder, float power) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.OutExponential(power);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out exponential easing with specified power.
        /// </summary>
        /// <param name="builder">The ease builder.</param>
        /// <param name="power">The power exponent for the curve.</param>
        public static TFlow InOutExponential<TFlow>(this EaseBuilder<TFlow> builder, float power) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.InOutExponential(power);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in back easing (pulls back before accelerating forward).
        /// </summary>
        public static TFlow InBack<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            const float c1 = 1.70158f;
            builder.Flow.Ease = EaseFactory.Generic(t => c1 * t * t * t - c1 * t * t);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out back easing (overshoots target and springs back).
        /// </summary>
        public static TFlow OutBack<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            const float c1 = 1.70158f;
            builder.Flow.Ease = EaseFactory.Generic(t =>
            {
                float t1 = t - 1f;
                return 1f + c1 * t1 * t1 * t1 + c1 * t1 * t1;
            });
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out back easing (overshoot at both start and end).
        /// </summary>
        public static TFlow InOutBack<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            const float c1 = 1.70158f * 1.525f;
            builder.Flow.Ease = EaseFactory.Generic(t => t < 0.5f
                ? (Mathf.Pow(2f * t, 2f) * ((c1 + 1f) * 2f * t - c1)) / 2f
                : (Mathf.Pow(2f * t - 2f, 2f) * ((c1 + 1f) * (t * 2f - 2f) + c1) + 2f) / 2f);
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in elastic easing (spring oscillation at start).
        /// </summary>
        public static TFlow InElastic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t =>
            {
                if (t == 0f || t.IsApproximatelyOf(1f)) return t;
                const float c = (2f * Mathf.PI) / 0.3f;
                return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c);
            });
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out elastic easing (spring oscillation at end).
        /// </summary>
        public static TFlow OutElastic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t =>
            {
                if (t == 0f || t.IsApproximatelyOf(1f)) return t;
                const float c = (2f * Mathf.PI) / 0.3f;
                return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c) + 1f;
            });
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out elastic easing (spring oscillation at both start and end).
        /// </summary>
        public static TFlow InOutElastic<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t =>
            {
                if (t == 0f || t.IsApproximatelyOf(1f)) return t;
                const float c = (2f * Mathf.PI) / 0.45f;
                return t < 0.5f
                    ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c)) / 2f
                    : (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c)) / 2f + 1f;
            });
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in bounce easing (bounces like a ball at start).
        /// </summary>
        public static TFlow InBounce<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => 1f - BounceEaseOut(1f - t));
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-out bounce easing (bounces like a ball at end).
        /// </summary>
        public static TFlow OutBounce<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => BounceEaseOut(t));
            return builder.Flow;
        }

        /// <summary>
        /// Applies ease-in-out bounce easing (bounces at both start and end).
        /// </summary>
        public static TFlow InOutBounce<TFlow>(this EaseBuilder<TFlow> builder) where TFlow : class, IFlow
        {
            builder.Flow.Ease = EaseFactory.Generic(t => t < 0.5f
                ? (1f - BounceEaseOut(1f - 2f * t)) / 2f
                : (1f + BounceEaseOut(2f * t - 1f)) / 2f);
            return builder.Flow;
        }

        private static float BounceEaseOut(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1f / d1)
                return n1 * t * t;
            else if (t < 2f / d1)
            {
                t -= 1.5f / d1;
                return n1 * t * t + 0.75f;
            }
            else if (t < 2.5f / d1)
            {
                t -= 2.25f / d1;
                return n1 * t * t + 0.9375f;
            }
            else
            {
                t -= 2.625f / d1;
                return n1 * t * t + 0.984375f;
            }
        }
    }
}
