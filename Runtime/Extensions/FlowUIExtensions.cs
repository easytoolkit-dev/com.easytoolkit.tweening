using EasyToolkit.Core.Mathematics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyToolkit.Fluxion
{
    public static class FlowUIExtensions
    {
        public static Flow DoAnchorPos(this RectTransform target, Vector2 to, float duration)
        {
            return FluxFactroy.To(() => target.anchoredPosition, pos => target.anchoredPosition = pos,
                    to, duration)
                .SetUnityObject(target);
        }

        public static Flow DoAnchorPosX(this RectTransform target, float to, float duration)
        {
            return FluxFactroy.To(() => target.anchoredPosition.x,
                    x => target.anchoredPosition = target.anchoredPosition.WithX(x),
                    to, duration)
                .SetUnityObject(target);
        }

        public static Flow DoAnchorPosY(this RectTransform target, float to, float duration)
        {
            return FluxFactroy.To(() => target.anchoredPosition.y,
                    y => target.anchoredPosition = target.anchoredPosition.WithY(y),
                    to, duration)
                .SetUnityObject(target);
        }

        public static Flow DoFade(this CanvasGroup target, float to, float duration)
        {
            return FluxFactroy.To(() => target.alpha, val => target.alpha = val, to, duration)
                .SetUnityObject(target);
        }

        public static Flow DoMaxVisibleCharacters(this TextMeshProUGUI target, int to, float duration)
        {
            return FluxFactroy.To(() => target.maxVisibleCharacters, val => target.maxVisibleCharacters = val, to, duration)
                .SetUnityObject(target);
        }

        public static Flow DoSpritesAnim(this Image target, Sprite[] sprites, float duration)
        {
            return FluxUtility.PlaySpritesAnim(sprite => target.sprite = sprite, sprites, duration)
                .SetUnityObject(target);
        }
    }
}
