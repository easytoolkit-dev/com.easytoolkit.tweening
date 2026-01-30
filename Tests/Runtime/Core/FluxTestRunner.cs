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
        /// Manually updates a Flux, simulating the passage of time.
        /// </summary>
        /// <param name="flux">The Flux to update.</param>
        /// <param name="deltaTime">The time elapsed since the last update.</param>
        public void UpdateFlux(IFluxEntity flux, float deltaTime)
        {
            if (flux == null)
            {
                throw new ArgumentNullException(nameof(flux));
            }

            if (deltaTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time cannot be negative.");
            }

            flux.Update(deltaTime);
        }

        /// <summary>
        /// Runs a Flux until completion or timeout.
        /// </summary>
        /// <param name="flux">The Flux to run.</param>
        /// <param name="timeStep">The time step for each update (default: 0.016s, ~60fps).</param>
        /// <param name="maxTime">The maximum time to run before timeout (default: 10s).</param>
        public void RunToCompletion(IFluxEntity flux, float timeStep = 0.016f, float maxTime = 10f)
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
        public void RunForDuration(IFluxEntity flux, float duration, float timeStep = 0.016f)
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
