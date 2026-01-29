using EasyToolkit.Core;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using UnityEngine;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowCoreExtensions
    {
        public static IFlow FluxLocalMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localPosition, pos => target.localPosition = pos, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxLocalMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.x,
                    x => target.localPosition = target.localPosition.WithX(x),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxLocalMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.y,
                    y => target.localPosition = target.localPosition.WithY(y),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxLocalMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.localPosition.z,
                    z => target.localPosition = target.localPosition.WithY(z),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxMove(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.position, pos => target.position = pos, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxMoveX(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.x,
                    x => target.position = target.position.WithX(x),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxMoveY(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.y,
                    y => target.position = target.position.WithY(y),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxMoveZ(this Transform target, float to, float duration)
        {
            return FluxFactory.To(() => target.position.z,
                    z => target.position = target.position.WithZ(z),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxScale(this Transform target, Vector3 to, float duration)
        {
            return FluxFactory.To(() => target.localScale, scale => target.localScale = scale, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FluxScale(this Transform target, float to, float duration)
        {
            return target.FluxScale(Vector3.one * to, duration);
        }

        public static IFlow FluxSpritesAnim(this SpriteRenderer target, Sprite[] sprites,
            float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .WithUnityObject(target);
        }
    }
}
