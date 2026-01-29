using System;

namespace EasyToolkit.Fluxion.Core
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

        event Action<IFlux> Played;
        event Action<IFlux> Paused;
        event Action<IFlux> Completed;
        event Action<IFlux> Killed;

        void Kill();
    }
}
