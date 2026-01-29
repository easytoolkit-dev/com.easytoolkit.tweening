using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyToolkit.Fluxion.Core
{
    internal class RunningFluxList
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

            // Iterate backwards or copy list to safely modify? Original code used iteration on list directly which might be unsafe if modification happens,
            // but the original code was: "foreach (var flux in _runningFluxes)".
            // And removals happen in a separate loop or via PendingKill.
            // Let's stick to the original logic structure but using interfaces.

            // Note: If modifications to _runningFluxes happen during Update() (e.g. from callbacks), we might need a copy.
            // But let's keep it close to original for now unless we see issues.
            // A safer approach for iteration with potential modification is usually a backwards loop or a copy.
            // The original code used a simple foreach. Let's assume Add/Remove are not called during this specific loop phase (or handled elsewhere).

            // Use a copy for iteration to safely handle additions/removals during update (defensive coding)
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

                flux.Update();
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
