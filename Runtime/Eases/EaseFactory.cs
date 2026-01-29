using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion.Eases
{
    public static class EaseFactory
    {
        /// <summary>
        /// 线性匀速过渡，无加速度。
        /// </summary>
        public static IFlowEase Linear()
        {
            return new Implementations.GenericFlowEase(time => time);
        }

        /// <summary>
        /// 使用正弦函数起始缓慢，逐渐加速。
        /// </summary>
        public static IFlowEase InSine()
        {
            return new Implementations.GenericFlowEase(time => 1f - Mathf.Cos((time * Mathf.PI) / 2f));
        }

        /// <summary>
        /// 使用正弦函数起始快速，逐渐减速。
        /// </summary>
        public static IFlowEase OutSine()
        {
            return new Implementations.GenericFlowEase(time => Mathf.Sin((time * Mathf.PI) / 2f));
        }

        /// <summary>
        /// 使用正弦函数在开始和结束时都较缓慢，中间加速。
        /// </summary>
        public static IFlowEase InOutSine()
        {
            return new Implementations.GenericFlowEase(time => -(Mathf.Cos(Mathf.PI * time) - 1f) / 2f);
        }

        /// <summary>
        /// 二次函数缓动，起始缓慢。
        /// </summary>
        public static IFlowEase InQuad()
        {
            return new Implementations.InExponentialFlowEase().SetPow(2f);
        }

        /// <summary>
        /// 二次函数缓动，结束缓慢。
        /// </summary>
        public static IFlowEase OutQuad()
        {
            return new Implementations.OutExponentialFlowEase().SetPow(2f);
        }

        /// <summary>
        /// 二次函数缓动，开始和结束都缓慢，中间加速。
        /// </summary>
        public static IFlowEase InOutQuad()
        {
            return new Implementations.InOutExponentialFlowEase().SetPow(2f);
        }

        /// <summary>
        /// 三次函数缓动，起始缓慢。
        /// </summary>
        public static IFlowEase InCubic()
        {
            return new Implementations.InExponentialFlowEase().SetPow(3f);
        }

        /// <summary>
        /// 三次函数缓动，结束缓慢。
        /// </summary>
        public static IFlowEase OutCubic()
        {
            return new Implementations.OutExponentialFlowEase().SetPow(3f);
        }

        /// <summary>
        /// 三次函数缓动，开始和结束都缓慢，中间加速。
        /// </summary>
        public static IFlowEase InOutCubic()
        {
            return new Implementations.InOutExponentialFlowEase().SetPow(3f);
        }

        /// <summary>
        /// 四次函数缓动，起始缓慢。
        /// </summary>
        public static IFlowEase InQuart()
        {
            return new Implementations.InExponentialFlowEase().SetPow(4f);
        }

        /// <summary>
        /// 四次函数缓动，结束缓慢。
        /// </summary>
        public static IFlowEase OutQuart()
        {
            return new Implementations.OutExponentialFlowEase().SetPow(4f);
        }

        /// <summary>
        /// 四次函数缓动，开始和结束都缓慢，中间加速。
        /// </summary>
        public static IFlowEase InOutQuart()
        {
            return new Implementations.InOutExponentialFlowEase().SetPow(4f);
        }

        /// <summary>
        /// 五次函数缓动，起始缓慢。
        /// </summary>
        public static IFlowEase InQuint()
        {
            return new Implementations.InExponentialFlowEase().SetPow(5f);
        }

        /// <summary>
        /// 五次函数缓动，结束缓慢。
        /// </summary>
        public static IFlowEase OutQuint()
        {
            return new Implementations.OutExponentialFlowEase().SetPow(5f);
        }

        /// <summary>
        /// 五次函数缓动，开始和结束都缓慢，中间加速。
        /// </summary>
        public static IFlowEase InOutQuint()
        {
            return new Implementations.InOutExponentialFlowEase().SetPow(5f);
        }

        /// <summary>
        /// 指数函数缓动，起始缓慢。
        /// </summary>
        /// <param name="power">指数次方</param>
        public static IFlowEase InExponential(float power)
        {
            return new Implementations.InExponentialFlowEase().SetPow(power);
        }

        /// <summary>
        /// 指数函数缓动，结束缓慢。
        /// </summary>
        /// <param name="power">指数次方</param>
        public static IFlowEase OutExponential(float power)
        {
            return new Implementations.OutExponentialFlowEase().SetPow(power);
        }

        /// <summary>
        /// 指数函数缓动，开始和结束都缓慢，中间加速。
        /// </summary>
        /// <param name="power">指数次方</param>
        public static IFlowEase InOutExponential(float power)
        {
            return new Implementations.InOutExponentialFlowEase().SetPow(power);
        }

        /// <summary>
        /// 向后拉动再向前加速的动画。
        /// </summary>
        public static IFlowEase InBack()
        {
            const float c1 = 1.70158f;
            return new Implementations.GenericFlowEase(time => c1 * time * time * time - c1 * time * time);
        }

        /// <summary>
        /// 向前滑动并超出目标后回弹的动画。
        /// </summary>
        public static IFlowEase OutBack()
        {
            const float c1 = 1.70158f;
            return new Implementations.GenericFlowEase(time =>
            {
                float t1 = time - 1f;
                return 1f + c1 * t1 * t1 * t1 + c1 * t1 * t1;
            });
        }

        /// <summary>
        /// 起始和终点都有回弹的动画。
        /// </summary>
        public static IFlowEase InOutBack()
        {
            const float c1 = 1.70158f * 1.525f;
            return new Implementations.GenericFlowEase(time => time < 0.5f
                ? (Mathf.Pow(2f * time, 2f) * ((c1 + 1f) * 2f * time - c1)) / 2f
                : (Mathf.Pow(2f * time - 2f, 2f) * ((c1 + 1f) * (time * 2f - 2f) + c1) + 2f) / 2f);
        }

        /// <summary>
        /// 起始具有弹簧震荡效果，逐渐进入动画。
        /// </summary>
        public static IFlowEase InElastic()
        {
            return new Implementations.GenericFlowEase(time =>
            {
                if (time == 0f || time.IsApproximatelyOf(1f)) return time;
                const float c = (2f * Mathf.PI) / 0.3f;
                return -Mathf.Pow(2f, 10f * time - 10f) * Mathf.Sin((time * 10f - 10.75f) * c);
            });
        }

        /// <summary>
        /// 动画结束时产生弹簧震荡回弹效果。
        /// </summary>
        public static IFlowEase OutElastic()
        {
            return new Implementations.GenericFlowEase(time =>
            {
                if (time == 0f || time.IsApproximatelyOf(1f)) return time;
                const float c = (2f * Mathf.PI) / 0.3f;
                return Mathf.Pow(2f, -10f * time) * Mathf.Sin((time * 10f - 0.75f) * c) + 1f;
            });
        }

        /// <summary>
        /// 动画开始和结束都具有弹簧震荡效果。
        /// </summary>
        public static IFlowEase InOutElastic()
        {
            return new Implementations.GenericFlowEase(time =>
            {
                if (time == 0f || time.IsApproximatelyOf(1f)) return time;
                const float c = (2f * Mathf.PI) / 0.45f;
                return time < 0.5f
                    ? -(Mathf.Pow(2f, 20f * time - 10f) * Mathf.Sin((20f * time - 11.125f) * c)) / 2f
                    : (Mathf.Pow(2f, -20f * time + 10f) * Mathf.Sin((20f * time - 11.125f) * c)) / 2f + 1f;
            });
        }

        /// <summary>
        /// 动画开始像球落地一样反弹，逐渐收敛。
        /// </summary>
        public static IFlowEase InBounce()
        {
            return new Implementations.GenericFlowEase(time => 1f - BounceEaseOut(1f - time));
        }

        /// <summary>
        /// 动画结束像球落地一样反弹，逐渐停止。
        /// </summary>
        public static IFlowEase OutBounce()
        {
            return new Implementations.GenericFlowEase(time => BounceEaseOut(time));
        }

        /// <summary>
        /// 动画开始和结束都带有弹跳效果。
        /// </summary>
        public static IFlowEase InOutBounce()
        {
            return new Implementations.GenericFlowEase(time => time < 0.5f
                ? (1f - BounceEaseOut(1f - 2f * time)) / 2f
                : (1f + BounceEaseOut(2f * time - 1f)) / 2f);
        }

        // 弹跳效果的计算方法
        private static float BounceEaseOut(float time)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (time < 1f / d1)
                return n1 * time * time;
            else if (time < 2f / d1)
            {
                time -= 1.5f / d1;
                return n1 * time * time + 0.75f;
            }
            else if (time < 2.5f / d1)
            {
                time -= 2.25f / d1;
                return n1 * time * time + 0.9375f;
            }
            else
            {
                time -= 2.625f / d1;
                return n1 * time * time + 0.984375f;
            }
        }
    }
}
