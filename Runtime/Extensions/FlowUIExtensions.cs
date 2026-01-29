using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FlowUIExtensions
    {
        public static IFlow DoAnchorPos(this RectTransform target, Vector2 to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition, pos => target.anchoredPosition = pos,
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoAnchorPosX(this RectTransform target, float to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition.x,
                    x => target.anchoredPosition = target.anchoredPosition.WithX(x),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoAnchorPosY(this RectTransform target, float to, float duration)
        {
            return FluxFactory.To(() => target.anchoredPosition.y,
                    y => target.anchoredPosition = target.anchoredPosition.WithY(y),
                    to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoFade(this CanvasGroup target, float to, float duration)
        {
            return FluxFactory.To(() => target.alpha, val => target.alpha = val, to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoMaxVisibleCharacters(this TextMeshProUGUI target, int to, float duration)
        {
            return FluxFactory.To(() => target.maxVisibleCharacters, val => target.maxVisibleCharacters = val, to, duration)
                .SetUnityObject(target);
        }

        public static IFlow DoSpritesAnim(this Image target, Sprite[] sprites, float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .SetUnityObject(target);
        }
    }
}
