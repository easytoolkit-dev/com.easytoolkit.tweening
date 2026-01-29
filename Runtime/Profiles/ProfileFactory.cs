using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public static class ProfileFactory
    {
        public static LinearFluxProfile Linear()
        {
            return new LinearFluxProfile();
        }

        public static BezierFluxProfile Bezier()
        {
            return new BezierFluxProfile();
        }
    }
}
