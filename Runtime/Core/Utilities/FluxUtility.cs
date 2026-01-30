using System;
using UnityEngine;

namespace EasyToolkit.Fluxion.Core
{
    public static class FluxUtility
    {
        public static IFlow<int> PlaySpritesAnim(Action<Sprite> spriteSetter, Sprite[] sprites, float duration)
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
