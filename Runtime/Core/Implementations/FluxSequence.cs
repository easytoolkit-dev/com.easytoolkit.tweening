using System;
using System.Collections.Generic;
using System.Linq;
using EasyToolkit.Core.Mathematics;

namespace EasyToolkit.Fluxion.Core.Implementations
{
    internal class FluxSequenceClip
    {
        private readonly List<IFlux> _totalFluxes = new List<IFlux>();
        private readonly FluxCollection _runningFluxes = new FluxCollection();
        public FluxSequence Owner { get; }
        public float ElapsedTime { get; private set; }

        public FluxSequenceClip(FluxSequence owner)
        {
            Owner = owner;
            ElapsedTime = 0f;
        }

        public void AddFlux(IFlux flux)
        {
            if (flux.OwnerSequence != null && flux.OwnerSequence != Owner)
            {
                throw new Exception("Only one sequence can be added to a flux");
            }

            // Note: Detach is now handled by the Sequence level using Context

            ((IFluxEntity)flux).OwnerSequence = Owner;

            _runningFluxes.Add((IFluxEntity)flux);
            _totalFluxes.Add(flux);
        }

        public void Kill()
        {
            _runningFluxes.Kill();
        }

        public float? GetDuration()
        {
            return _totalFluxes.Max(flux => flux.Duration ?? 0f);
        }

        public void Update(float deltaTime)
        {
            ElapsedTime += deltaTime;
            _runningFluxes.Update(deltaTime);
        }

        public float GetRemainingDuration()
        {
            var totalDuration = GetDuration() ?? 0f;
            return (totalDuration - ElapsedTime).Clamp(0f, float.MaxValue);
        }

        public bool IsAllKilled()
        {
            return _runningFluxes.IsAllKilled();
        }
    }

    internal class FluxSequence : FluxBase, IFluxSequence
    {
        private readonly List<FluxSequenceClip> _fluxClips = new List<FluxSequenceClip>();
        private int _currentClipIndex;

        private float? _actualDuration;
        public override float? Duration => _actualDuration;

        protected override void OnReset()
        {
            base.OnReset();
            _fluxClips.Clear();
            _currentClipIndex = -1;
        }

        public void AddFluxAsNewClip(IFlux flux)
        {
            // Detach from current context before adding to sequence
            Context?.Lifecycle.Detach(flux);

            var node = new FluxSequenceClip(this);
            node.AddFlux((IFluxEntity)flux);
            _fluxClips.Add(node);
        }

        public void AddFluxToLastClip(IFlux flux)
        {
            // Detach from current context before adding to sequence
            Context?.Lifecycle.Detach(flux);

            var node = _fluxClips.LastOrDefault();
            if (node == null)
            {
                AddFluxAsNewClip(flux);
            }
            else
            {
                node.AddFlux((IFluxEntity)flux);
            }
        }

        protected override void OnStart()
        {
            _currentClipIndex = 0;
        }

        protected override void OnPlaying(float time)
        {
            var remainingDeltaTime = PreviousDeltaTime;

            while (remainingDeltaTime > 0f && _currentClipIndex < _fluxClips.Count)
            {
                var clip = _fluxClips[_currentClipIndex];
                var clipRemainingDuration = clip.GetRemainingDuration();

                clip.Update(remainingDeltaTime);

                if (clip.IsAllKilled())
                {
                    // Calculate the remaining time after this clip completes
                    remainingDeltaTime -= clipRemainingDuration;
                    if (remainingDeltaTime < 0f)
                    {
                        remainingDeltaTime = 0f;
                    }
                    _currentClipIndex++;
                }
                else
                {
                    // Clip is still running, no remaining time to pass
                    break;
                }
            }

            if (_currentClipIndex >= _fluxClips.Count)
            {
                _actualDuration = 0f;
                foreach (var fluxNode in _fluxClips)
                {
                    _actualDuration += fluxNode.GetDuration();
                }

                Complete();
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
