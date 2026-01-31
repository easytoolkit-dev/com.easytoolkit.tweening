using System;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Core.Tests
{
    /// <summary>
    /// Test runner for manually driving Flux update loops.
    /// </summary>
    internal class FluxTestRunner
    {
        /// <summary>
        /// Updates the Flux engine, processing all registered Flux instances.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last update (default: 0.016s, ~60fps).</param>
        /// <remarks>
        /// This method initializes and updates the mock Flux engine. You MUST call this method
        /// before using <see cref="UpdateFlux"/>, <see cref="RunToCompletion"/>, or <see cref="RunForDuration"/>
        /// to ensure the Flux instances are properly registered and initialized in the engine.
        /// </remarks>
        public void UpdateEngine(float deltaTime = 0.016f)
        {
            MockFluxEngine.Instance.Update(deltaTime);
        }

        /// <summary>
        /// Manually updates a Flux, simulating the passage of time.
        /// </summary>
        /// <param name="flux">The Flux to update.</param>
        /// <param name="deltaTime">The time elapsed since the last update.</param>
        /// <remarks>
        /// You MUST call <see cref="UpdateEngine"/> at least once before using this method
        /// to ensure the Flux instance is properly registered in the engine.
        /// </remarks>
        public void UpdateFlux(IFlux flux, float deltaTime)
        {
            if (flux == null)
            {
                throw new ArgumentNullException(nameof(flux));
            }

            if (deltaTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time cannot be negative.");
            }

            ((IFluxEntity)flux).Update(deltaTime);
        }

        /// <summary>
        /// Runs a Flux until completion or timeout.
        /// </summary>
        /// <param name="flux">The Flux to run.</param>
        /// <param name="timeStep">The time step for each update (default: 0.016s, ~60fps).</param>
        /// <param name="maxTime">The maximum time to run before timeout (default: 10s).</param>
        /// <remarks>
        /// You MUST call <see cref="UpdateEngine"/> at least once before using this method
        /// to ensure the Flux instance is properly registered in the engine.
        /// </remarks>
        public void RunToCompletion(IFlux flux, float timeStep = 0.016f, float maxTime = 10f)
        {
            if (flux == null)
            {
                throw new ArgumentNullException(nameof(flux));
            }

            if (timeStep <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(timeStep), "Time step must be positive.");
            }

            if (maxTime <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(maxTime), "Max time must be positive.");
            }

            var elapsed = 0f;
            while (flux.CurrentState != FluxState.Completed &&
                   flux.CurrentState != FluxState.Killed &&
                   !flux.IsPendingKill &&
                   elapsed < maxTime)
            {
                UpdateFlux(flux, timeStep);
                elapsed += timeStep;
            }

            if (elapsed >= maxTime &&
                flux.CurrentState != FluxState.Completed &&
                flux.CurrentState != FluxState.Killed)
            {
                throw new TimeoutException($"Flux did not complete within {maxTime} seconds.");
            }
        }

        /// <summary>
        /// Runs a Flux for a specified duration.
        /// </summary>
        /// <param name="flux">The Flux to run.</param>
        /// <param name="duration">The total duration to run.</param>
        /// <param name="timeStep">The time step for each update (default: 0.016s, ~60fps).</param>
        /// <remarks>
        /// You MUST call <see cref="UpdateEngine"/> at least once before using this method
        /// to ensure the Flux instance is properly registered in the engine.
        /// </remarks>
        public void RunForDuration(IFlux flux, float duration, float timeStep = 0.016f)
        {
            if (flux == null)
            {
                throw new ArgumentNullException(nameof(flux));
            }

            if (duration < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration cannot be negative.");
            }

            if (timeStep <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(timeStep), "Time step must be positive.");
            }

            var elapsed = 0f;
            while (elapsed < duration &&
                   flux.CurrentState != FluxState.Completed &&
                   flux.CurrentState != FluxState.Killed &&
                   !flux.IsPendingKill)
            {
                var step = Math.Min(timeStep, duration - elapsed);
                UpdateFlux(flux, step);
                elapsed += step;
            }
        }
    }
}
