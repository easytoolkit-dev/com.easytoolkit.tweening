using System;

namespace EasyToolkit.Fluxion
{
    /// <summary>
    /// Interface for the public API of a Flux object.
    /// Represents a unit of work that can be played, paused, and completed over time.
    /// </summary>
    public interface IFlux
    {
        /// <summary>
        /// Gets or sets the unique identifier for this Flux.
        /// Setting this ID registers the Flux with the engine, allowing it to be retrieved later.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the delay in seconds before the Flux starts playing after being started.
        /// </summary>
        float Delay { get; set; }

        /// <summary>
        /// Gets or sets the number of times the Flux should loop.
        /// </summary>
        int LoopCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Flux should loop infinitely.
        /// </summary>
        bool InfiniteLoop { get; set; }

        /// <summary>
        /// Gets the current state of the Flux.
        /// </summary>
        FluxState CurrentState { get; }

        /// <summary>
        /// Gets a value indicating whether this Flux is pending to be killed.
        /// </summary>
        bool IsPendingKill { get; }

        /// <summary>
        /// Gets the duration of the Flux, or null if it cannot be determined.
        /// </summary>
        /// <returns>The duration in seconds, or null.</returns>
        float? Duration { get; }

        /// <summary>
        /// Gets the sequence that owns this Flux.
        /// </summary>
        IFlux OwnerSequence { get; }

        void Kill();

        /// <summary>
        /// Adds a callback to be invoked when the Flux starts playing.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        void AddPlayCallback(Action<IFlux> callback);

        /// <summary>
        /// Adds a callback to be invoked when the Flux is paused.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        void AddPauseCallback(Action<IFlux> callback);

        /// <summary>
        /// Adds a callback to be invoked when the Flux completes.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        void AddCompleteCallback(Action<IFlux> callback);

        /// <summary>
        /// Adds a callback to be invoked when the Flux is killed.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        void AddKillCallback(Action<IFlux> callback);
    }
}
