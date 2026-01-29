namespace EasyToolkit.Fluxion.Core
{
    public interface IFluxSequence : IFlux
    {
        void AddFluxAsNewClip(IFlux flux);
        void AddFluxToLastClip(IFlux flux);
    }
}
