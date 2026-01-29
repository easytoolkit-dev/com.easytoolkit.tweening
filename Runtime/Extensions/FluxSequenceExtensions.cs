namespace EasyToolkit.Fluxion
{
    public static class FluxSequenceExtensions
    {
        public static FluxSequence Append(this FluxSequence sequence, AbstractFlux tween)
        {
            sequence.AddFluxAsNewClip(tween);
            return sequence;
        }

        public static FluxSequence Join(this FluxSequence sequence, AbstractFlux tween)
        {
            sequence.AddFluxToLastClip(tween);
            return sequence;
        }
    }
}
