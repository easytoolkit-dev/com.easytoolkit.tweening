namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Registry for managing Flux entities by their identifiers.
    /// </summary>
    public interface IFluxRegistry
    {
        /// <summary>
        /// Registers a Flux with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the Flux.</param>
        /// <param name="flux">The Flux to register.</param>
        void RegisterFlux(string id, IFlux flux);

        /// <summary>
        /// Unregisters a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to unregister.</param>
        void UnregisterFlux(string id);

        /// <summary>
        /// Gets a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to retrieve.</param>
        /// <returns>The Flux with the specified identifier, or null if not found.</returns>
        IFlux GetFluxById(string id);
    }
}
