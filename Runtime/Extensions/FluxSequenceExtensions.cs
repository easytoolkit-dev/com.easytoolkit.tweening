namespace EasyToolkit.Fluxion
{
    public static class FluxSequenceExtensions
    {
        public static FluxSequence Append(this FluxSequence sequence, IFlux tween)
        {
            sequence.AddFluxAsNewClip(tween);
            return sequence;
        }

        public static FluxSequence Join(this FluxSequence sequence, IFlux tween)
        {
            sequence.AddFluxToLastClip(tween);
            return sequence;
        }
    }
}
