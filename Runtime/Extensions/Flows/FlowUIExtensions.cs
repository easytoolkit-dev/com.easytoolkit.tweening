using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowUIExtensions
    {
        public static IFlow FlowAnchorPos(this RectTransform target, Vector2 to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition, pos => target.anchoredPosition = pos,
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowAnchorPosX(this RectTransform target, float to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition.x,
                    x => target.anchoredPosition = target.anchoredPosition.WithX(x),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowAnchorPosY(this RectTransform target, float to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition.y,
                    y => target.anchoredPosition = target.anchoredPosition.WithY(y),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowFade(this CanvasGroup target, float to, float duration)
        {
            return FluxFactory.To(() => target.alpha, val => target.alpha = val, to, duration)
                .WithUnityObject(target);
        }

        /// <summary>
        /// Creates a flow that animates the alpha (transparency) of a Graphic component.
        /// This works with Image, Text, and other UI components that inherit from Graphic.
        /// </summary>
        /// <param name="target">The Graphic component to animate</param>
        /// <param name="to">The target alpha value (0-1)</param>
        /// <param name="duration">The duration of the animation in seconds</param>
        /// <returns>A flow that animates the Graphic's alpha</returns>
        public static IFlow FlowFade(this Graphic target, float to, float duration)
        {
            return FluxFactory.To(() => target.color.a,
                    a => target.color = target.color.WithA(a),
                    to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowMaxVisibleCharacters(this TextMeshProUGUI target, int to, float duration)
        {
            return FluxFactory.To(() => target.maxVisibleCharacters, val => target.maxVisibleCharacters = val, to, duration)
                .WithUnityObject(target);
        }

        public static IFlow FlowSpritesAnim(this Image target, Sprite[] sprites, float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .WithUnityObject(target);
        }
    }
}
