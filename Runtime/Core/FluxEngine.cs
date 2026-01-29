using System;
using System.Collections.Generic;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Textual;

namespace EasyToolkit.Fluxion
{
    [MonoSingletonConfiguration(MonoSingletonFlags.DontDestroyOnLoad)]
    public class FluxEngine : MonoSingleton<FluxEngine>
    {
        private readonly RunningFluxList _runningFluxes = new RunningFluxList();
        private readonly Dictionary<string, AbstractFlux> _fluxesById = new Dictionary<string, AbstractFlux>();

        public void Attach(AbstractFlux flux)
        {
            _runningFluxes.Add(flux);
        }

        public void Detach(AbstractFlux flux)
        {
            _runningFluxes.Remove(flux);
        }

        internal void RegisterFluxById(string id, AbstractFlux flux)
        {
            if (id.IsNullOrEmpty())
                return;

            if (_fluxesById.TryGetValue(id, out var existingFlux))
            {
                if (existingFlux != flux)
                {
                    if (!existingFlux.IsPendingKill())
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

        internal void UnregisterFluxById(string id)
        {
            if (id.IsNullOrEmpty())
                return;

            _fluxesById.Remove(id);
        }

        internal AbstractFlux GetFluxById(string id)
        {
            if (id.IsNullOrEmpty())
                return null;

            return _fluxesById.GetValueOrDefault(id);
        }

        void Update()
        {
            _runningFluxes.Update();
        }
    }
}
