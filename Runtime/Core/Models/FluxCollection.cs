using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyToolkit.Fluxion.Core
{
    internal class FluxCollection
    {
        private readonly List<IFluxEntity> _pendingKillFluxes = new List<IFluxEntity>();
        private readonly List<IFluxEntity> _runningFluxes = new List<IFluxEntity>();

        public bool IsAllKilled()
        {
            return _runningFluxes.All(flux => flux.CurrentState == FluxState.Killed);
        }

        public void Add(IFluxEntity flux)
        {
            _runningFluxes.Add(flux);
        }

        public void Remove(IFluxEntity flux)
        {
            _runningFluxes.Remove(flux);
        }

        public void Update()
        {
            if (_runningFluxes.Count == 0)
                return;

            var count = _runningFluxes.Count;
            for (int i = 0; i < count; i++)
            {
                if (i >= _runningFluxes.Count) break; // Safety check

                var flux = _runningFluxes[i];

                if (flux.IsPendingKill)
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
                        flux.IsPendingKill = true;
                        _pendingKillFluxes.Add(flux);
                        continue;
                    }
                }

                flux.Update(Time.deltaTime);
            }

            foreach (var flux in _pendingKillFluxes)
            {
                flux.HandleKill();
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
                    flux.HandleKill();
                }
            }
        }
    }
}
