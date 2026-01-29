using System;
using System.Collections.Generic;
using System.Linq;
using EasyToolkit.Core.Mathematics;

namespace EasyToolkit.Fluxion
{
    class FluxSequenceClip
    {
        private readonly List<AbstractFlux> _totalFluxes = new List<AbstractFlux>();
        private readonly RunningFluxList _runningFluxes = new RunningFluxList();
        public FluxSequence Owner { get; }

        public FluxSequenceClip(FluxSequence owner)
        {
            Owner = owner;
        }

        public void AddFlux(AbstractFlux flux)
        {
            if (flux.OwnerSequence != null && flux.OwnerSequence != Owner)
            {
                throw new Exception("Only one sequence can be added to a flux");
            }

            FluxEngine.Instance.Detach(flux);
            flux.OwnerSequence = Owner;

            _runningFluxes.Add(flux);
            _totalFluxes.Add(flux);
        }

        public void Kill()
        {
            _runningFluxes.Kill();
        }

        public float? GetDuration()
        {
            return _totalFluxes.Max(flux => flux.GetActualDuration() ?? 0f);
        }

        public void Update()
        {
            _runningFluxes.Update();
        }

        public bool IsAllKilled()
        {
            return _runningFluxes.IsAllKilled();
        }
    }

    public class FluxSequence : AbstractFlux
    {
        private readonly List<FluxSequenceClip> _fluxClips = new List<FluxSequenceClip>();
        private int _currentClipIndex;

        private float? _actualDuration;
        protected override float? ActualDuration => _actualDuration;

        protected override void OnReset()
        {
            _fluxClips.Clear();
            _currentClipIndex = -1;
        }

        internal void AddFluxAsNewClip(AbstractFlux flux)
        {
            var node = new FluxSequenceClip(this);
            node.AddFlux(flux);
            _fluxClips.Add(node);
        }

        internal void AddFluxToLastClip(AbstractFlux flux)
        {
            var node = _fluxClips.LastOrDefault();
            if (node == null)
            {
                AddFluxAsNewClip(flux);
            }
            else
            {
                node.AddFlux(flux);
            }
        }

        protected override void OnStart()
        {
            _currentClipIndex = 0;
        }

        protected override void OnPlaying(float time)
        {
            if (_currentClipIndex >= _fluxClips.Count)
            {
                _actualDuration = 0f;
                foreach (var fluxNode in _fluxClips)
                {
                    _actualDuration += fluxNode.GetDuration();
                }

                Complete();
                return;
            }

            var node = _fluxClips[_currentClipIndex];
            node.Update();

            if (node.IsAllKilled())
            {
                _currentClipIndex++;
            }
        }

        protected override void OnKill()
        {
            var i = _currentClipIndex.Clamp(0, _currentClipIndex);
            for (; i < _fluxClips.Count; i++)
            {
                _fluxClips[i].Kill();
            }
        }
    }
}
