namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Manages the lifecycle of Flux entities, including attaching and detaching from the execution engine.
    /// </summary>
    public interface IFluxLifecycleManager
    {
        /// <summary>
        /// Attaches a Flux to the execution engine for updates.
        /// </summary>
        /// <param name="flux">The Flux to attach.</param>
        void Attach(IFlux flux);

        /// <summary>
        /// Detaches a Flux from the execution engine, stopping updates.
        /// </summary>
        /// <param name="flux">The Flux to detach.</param>
        void Detach(IFlux flux);
    }
}
