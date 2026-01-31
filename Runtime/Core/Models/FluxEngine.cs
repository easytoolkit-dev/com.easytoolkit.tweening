using System;
using System.Collections.Generic;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Textual;

namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// MonoSingleton that manages the execution context for all Flux entities.
    /// Implements <see cref="IFluxContext"/> to provide time, lifecycle, and registry services.
    /// </summary>
    [MonoSingletonConfiguration(MonoSingletonFlags.DontDestroyOnLoad)]
    public class FluxEngine : MonoSingleton<FluxEngine>, IFluxContext, IFluxLifecycleManager, IFluxRegistry
    {
        private readonly FluxCollection _runningFluxes = new FluxCollection();
        private readonly Dictionary<string, IFlux> _fluxesById = new Dictionary<string, IFlux>();
        private readonly UnityTimeProvider _timeProvider = new();

        /// <summary>
        /// Gets the time provider for accessing time-related values.
        /// </summary>
        ITimeProvider IFluxContext.Time => _timeProvider;

        /// <summary>
        /// Gets the lifecycle manager for attaching and detaching Flux entities.
        /// </summary>
        IFluxLifecycleManager IFluxContext.Lifecycle => this;

        /// <summary>
        /// Gets the registry for managing Flux entities by identifier.
        /// </summary>
        IFluxRegistry IFluxContext.Registry => this;

        /// <summary>
        /// Attaches a Flux to the execution engine for updates.
        /// </summary>
        /// <param name="flux">The Flux to attach.</param>
        public void Attach(IFlux flux)
        {
            _runningFluxes.Add((IFluxEntity)flux);
        }

        /// <summary>
        /// Detaches a Flux from the execution engine, stopping updates.
        /// </summary>
        /// <param name="flux">The Flux to detach.</param>
        public void Detach(IFlux flux)
        {
            _runningFluxes.Remove((IFluxEntity)flux);
        }

        /// <summary>
        /// Registers a Flux with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the Flux.</param>
        /// <param name="flux">The Flux to register.</param>
        public void Register(string id, IFlux flux)
        {
            if (id.IsNullOrEmpty())
                return;

            if (_fluxesById.TryGetValue(id, out var existingFlux))
            {
                if (existingFlux != flux)
                {
                    if (!existingFlux.IsPendingKill)
                    {
                        throw new ArgumentException($"The id '{id}' has been occupied.");
                    }
                }
                else
                {
                    return;
                }
            }
            _fluxesById[id] = flux;
        }

        /// <summary>
        /// Unregisters a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to unregister.</param>
        public void Unregister(string id)
        {
            if (id.IsNullOrEmpty())
                return;

            _fluxesById.Remove(id);
        }

        /// <summary>
        /// Gets a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to retrieve.</param>
        /// <returns>The Flux with the specified identifier, or null if not found.</returns>
        public IFlux GetById(string id)
        {
            if (id.IsNullOrEmpty())
                return null;

            return _fluxesById.GetValueOrDefault(id);
        }

        private void Update()
        {
            _runningFluxes.Update(_timeProvider.DeltaTime);
        }
    }
}
