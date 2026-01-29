using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FluxSequenceExtensions
    {
        public static IFluxSequence Append(this IFluxSequence sequence, IFlux tween)
        {
            sequence.AddFluxAsNewClip(tween);
            return sequence;
        }

        public static IFluxSequence Join(this IFluxSequence sequence, IFlux tween)
        {
            sequence.AddFluxToLastClip(tween);
            return sequence;
        }
    }
}
