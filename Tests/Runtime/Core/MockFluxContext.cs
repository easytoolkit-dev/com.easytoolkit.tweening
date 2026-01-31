using System.Collections.Generic;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Tests
{
    /// <summary>
    /// Mock implementation of <see cref="IFluxContext"/> for testing purposes.
    /// </summary>
    internal class MockFluxContext : IFluxContext
    {
        /// <summary>
        /// Gets the mock time provider.
        /// </summary>
        public ITimeProvider Time { get; } = new MockTimeProvider();

        /// <summary>
        /// Gets the mock lifecycle manager.
        /// </summary>
        public IFluxLifecycleManager Lifecycle { get; } = new MockLifecycleManager();

        /// <summary>
        /// Gets the mock registry.
        /// </summary>
        public IFluxRegistry Registry { get; } = new MockRegistry();
    }

    /// <summary>
    /// Mock implementation of <see cref="ITimeProvider"/> for testing purposes.
    /// </summary>
    internal class MockTimeProvider : ITimeProvider
    {
        /// <summary>
        /// Gets or sets the delta time value to return.
        /// </summary>
        public float DeltaTime { get; set; } = 0.016f;

        /// <summary>
        /// Gets or sets the unscaled delta time value to return.
        /// </summary>
        public float UnscaledDeltaTime { get; set; } = 0.016f;
    }

    /// <summary>
    /// Mock implementation of <see cref="IFluxLifecycleManager"/> for testing purposes.
    /// </summary>
    internal class MockLifecycleManager : IFluxLifecycleManager
    {
        /// <summary>
        /// Gets the list of attached Fluxes for verification.
        /// </summary>
        public List<IFlux> AttachedFluxes { get; } = new();

        /// <summary>
        /// Gets the list of detached Fluxes for verification.
        /// </summary>
        public List<IFlux> DetachedFluxes { get; } = new();

        /// <summary>
        /// Attaches a Flux to the mock lifecycle manager.
        /// </summary>
        /// <param name="flux">The Flux to attach.</param>
        public void Attach(IFlux flux)
        {
            AttachedFluxes.Add(flux);
        }

        /// <summary>
        /// Detaches a Flux from the mock lifecycle manager.
        /// </summary>
        /// <param name="flux">The Flux to detach.</param>
        public void Detach(IFlux flux)
        {
            DetachedFluxes.Add(flux);
        }
    }

    /// <summary>
    /// Mock implementation of <see cref="IFluxRegistry"/> for testing purposes.
    /// </summary>
    internal class MockRegistry : IFluxRegistry
    {
        /// <summary>
        /// Gets the dictionary of registered Fluxes for verification.
        /// </summary>
        public Dictionary<string, IFlux> Fluxes { get; } = new();

        /// <summary>
        /// Registers a Flux with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the Flux.</param>
        /// <param name="flux">The Flux to register.</param>
        public void RegisterFlux(string id, IFlux flux)
        {
            Fluxes[id] = flux;
        }

        /// <summary>
        /// Unregisters a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to unregister.</param>
        public void UnregisterFlux(string id)
        {
            Fluxes.Remove(id);
        }

        /// <summary>
        /// Gets a Flux by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Flux to retrieve.</param>
        /// <returns>The Flux with the specified identifier, or null if not found.</returns>
        public IFlux GetFluxById(string id)
        {
            return Fluxes.GetValueOrDefault(id);
        }
    }
}
