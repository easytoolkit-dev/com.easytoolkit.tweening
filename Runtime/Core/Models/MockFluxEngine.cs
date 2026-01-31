using System;
using System.Collections.Generic;
using EasyToolkit.Core.Collections;
using EasyToolkit.Core.Patterns;

namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Mock implementation of <see cref="IFluxContext"/> for testing purposes.
    /// </summary>
    public class MockFluxEngine : Singleton<MockFluxEngine>, IFluxContext, IFluxLifecycleManager, IFluxRegistry
    {
        private readonly FluxCollection _runningFluxes = new FluxCollection();
        private readonly Dictionary<string, IFlux> _fluxesById = new Dictionary<string, IFlux>();

        private MockFluxEngine()
        {
        }

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
            UnregisterFlux(flux.Id);
        }

        /// <summary>
        /// Registers a Flux with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the Flux.</param>
        /// <param name="flux">The Flux to register.</param>
        public void RegisterFlux(string id, IFlux flux)
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
        public void UnregisterFlux(string id)
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
        public IFlux GetFluxById(string id)
        {
            if (id.IsNullOrEmpty())
                return null;

            return _fluxesById.GetValueOrDefault(id);
        }

        public void Update(float deltaTime = 0.016f)
        {
            _runningFluxes.Update(deltaTime);
        }
    }
}
