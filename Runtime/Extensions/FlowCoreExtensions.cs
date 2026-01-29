using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowCoreExtensions
    {
        public static IFlow DoLocalMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localPosition, pos => target.localPosition = pos, to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoLocalMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.x,
                    x => target.localPosition = target.localPosition.WithX(x),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoLocalMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.y,
                    y => target.localPosition = target.localPosition.WithY(y),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoLocalMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.z,
                    z => target.localPosition = target.localPosition.WithY(z),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.position, pos => target.position = pos, to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.x,
                    x => target.position = target.position.WithX(x),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.y,
                    y => target.position = target.position.WithY(y),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.z,
                    z => target.position = target.position.WithZ(z),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoScale(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localScale, scale => target.localScale = scale, to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoScale(this Transform target, float to, float duration)
        {
            return target.DoScale(Vector3.one * to, duration);
        }

        public static IFlow DoSpritesAnim(this SpriteRenderer target, Sprite[] sprites,
            float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .SetUnityObject(target);
        }
    }
}
