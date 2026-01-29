using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowCoreExtensions
    {
        public static IFlow FlowLocalMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localPosition, pos => target.localPosition = pos, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowLocalMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.x,
                    x => target.localPosition = target.localPosition.WithX(x),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowLocalMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.y,
                    y => target.localPosition = target.localPosition.WithY(y),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowLocalMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.z,
                    z => target.localPosition = target.localPosition.WithY(z),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.position, pos => target.position = pos, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.x,
                    x => target.position = target.position.WithX(x),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.y,
                    y => target.position = target.position.WithY(y),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.z,
                    z => target.position = target.position.WithZ(z),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowScale(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localScale, scale => target.localScale = scale, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowScale(this Transform target, float to, float duration)
        {
            return target.FlowScale(Vector3.one * to, duration);
        }

        public static IFlow FlowSpritesAnim(this SpriteRenderer target, Sprite[] sprites,
            float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .WithUnityObject(target);
        }
    }
}
