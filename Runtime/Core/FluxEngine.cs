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
        private readonly Dictionary<string, IFlux> _fluxesById = new Dictionary<string, IFlux>();

        public void Attach(IFlux flux)
        {
            _runningFluxes.Add((IFluxEntity)flux);
        }

        public void Detach(IFlux flux)
        {
            _runningFluxes.Remove((IFluxEntity)flux);
        }

        public void RegisterFluxById(string id, IFlux flux)
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

        public void UnregisterFluxById(string id)
        {
            if (id.IsNullOrEmpty())
                return;

            _fluxesById.Remove(id);
        }

        public IFlux GetFluxById(string id)
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
