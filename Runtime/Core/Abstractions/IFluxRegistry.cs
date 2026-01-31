namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Registry for managing Flux entities by their identifiers.
    /// </summary>
    internal interface IFluxRegistry
    {
        /// <summary>
        /// Registers a Flux with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the Flux.</param>
        /// <param name="flux">The Flux to register.</param>
        void Register(string id, IFlux flux);

        /// <summary>
        /// Unregisters a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to unregister.</param>
        void Unregister(string id);

        /// <summary>
        /// Gets a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to retrieve.</param>
        /// <returns>The Flux with the specified identifier, or null if not found.</returns>
        IFlux GetById(string id);
    }
}
