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
