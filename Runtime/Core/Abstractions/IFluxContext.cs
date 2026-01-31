namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Provides the execution context for Flux entities, including time, lifecycle management, and registry services.
    /// </summary>
    public interface IFluxContext
    {
        /// <summary>
        /// Gets the lifecycle manager for attaching and detaching Flux entities.
        /// </summary>
        IFluxLifecycleManager Lifecycle { get; }

        /// <summary>
        /// Gets the registry for managing Flux entities by identifier.
        /// </summary>
        IFluxRegistry Registry { get; }
    }
}
