using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    internal class RunningFluxList
    {
        private readonly List<AbstractFlux> _pendingKillFluxes = new List<AbstractFlux>();
        private readonly List<AbstractFlux> _runningFluxes = new List<AbstractFlux>();

        public bool IsAllKilled()
        {
            return _runningFluxes.All(flux => flux.CurrentState == FluxState.Killed);
        }

        public void Add(AbstractFlux flux)
        {
            _runningFluxes.Add(flux);
        }

        public void Remove(AbstractFlux flux)
        {
            _runningFluxes.Remove(flux);
        }

        public void Update()
        {
            if (_runningFluxes.Count == 0)
                return;

            foreach (var flux in _runningFluxes)
            {
                if (flux.PendingKillSelf)
                {
                    _pendingKillFluxes.Add(flux);
                    continue;
                }

                if (flux.CurrentState == FluxState.Idle)
                {
                    try
                    {
                        flux.Start();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Failed to start flux '{flux}': {e.Message}");
                        flux.PendingKillSelf = true;
                        _pendingKillFluxes.Add(flux);
                        continue;
                    }
                }

                flux.Update();
            }

            foreach (var flux in _pendingKillFluxes)
            {
                flux.Kill();
                _runningFluxes.Remove(flux);
            }

            _pendingKillFluxes.Clear();
        }

        public void Kill()
        {
            foreach (var flux in _runningFluxes)
            {
                if (flux.CurrentState != FluxState.Killed)
                {
                    flux.Kill();
                }
            }
        }
    }
}
