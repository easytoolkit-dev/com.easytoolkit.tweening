using System;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    internal static class FluxUtility
    {
        public static Flow PlaySpritesAnim(Action<Sprite> spriteSetter, Sprite[] sprites, float duration)
        {
            int index = 0;
            return FluxFactory.To(() => index, x =>
            {
                index = x;
                spriteSetter(sprites[index]);
            }, sprites.Length - 1, duration);
        }
    }
}
