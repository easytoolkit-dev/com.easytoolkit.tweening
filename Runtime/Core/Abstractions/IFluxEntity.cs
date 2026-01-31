namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Internal interface for Flux entities, defining methods required for the Flux engine's operation.
    /// </summary>
    internal interface IFluxEntity : IFlux
    {
        /// <summary>
        /// Gets or sets the execution context for this Flux entity.
        /// </summary>
        IFluxContext Context { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this Flux is pending to be killed.
        /// </summary>
        new bool IsPendingKill { get; set; }

        /// <summary>
        /// Gets or sets the sequence that owns this Flux.
        /// </summary>
        new IFlux OwnerSequence { get; set; }

        /// <summary>
        /// Resets the Flux to its initial state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Starts the Flux.
        /// </summary>
        void Start();

        /// <summary>
        /// Updates the Flux state. Called every frame by the engine.
        /// </summary>
        void Update(float deltaTime);

        /// <summary>
        /// Kills the Flux immediately.
        /// </summary>
        void HandleKill();
    }
}
