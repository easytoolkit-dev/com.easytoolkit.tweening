using System;
using EasyToolkit.Core.Diagnostics;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Textual;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public enum FluxState
    {
        Idle,
        DelayAfterPlay,
        Playing,
        Paused,
        Completed,
        Killed,
    }

    public abstract class AbstractFlux
    {
        private string _id;
        internal string Id
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                if (!string.IsNullOrEmpty(_id))
                {
                    FluxEngine.Instance.UnregisterFluxById(_id);
                }

                _id = value;

                if (!string.IsNullOrEmpty(_id))
                {
                    FluxEngine.Instance.RegisterFluxById(_id, this);
                }
            }
        }

        internal float? GetActualDuration() => ActualDuration;

        /// <summary>
        /// 获取实际的持续时间，返回null代表无法判断具体持续时间。
        /// </summary>
        /// <returns></returns>
        protected virtual float? ActualDuration => null;

        internal float? LastPlayTime { get; private set; }

        internal float Delay { get; set; }

        internal bool InfiniteLoop { get; set; }
        internal int LoopCount { get; set; }

        internal FluxState CurrentState => _state.CurrentStateKey;


        internal FluxSequence OwnerSequence { get; set; }

        internal bool PendingKillSelf { get; set; }
        protected internal bool IsInLoop { get; set; }

        internal bool IsPendingKill()
        {
            if (PendingKillSelf)
            {
                return true;
            }

            if (OwnerSequence != null)
            {
                return OwnerSequence.IsPendingKill();
            }
            return false;
        }

        private readonly StateMachine<FluxState> _state = new StateMachine<FluxState>();
        private bool _pause;
        private float _playElapsedTime;
        private Action<AbstractFlux> _onPlay;
        private Action<AbstractFlux> _onPause;
        private Action<AbstractFlux> _onComplete;
        private Action<AbstractFlux> _onKill;

        public void AddPlayCallback(Action<AbstractFlux> callback) => _onPlay += callback;
        public void AddPauseCallback(Action<AbstractFlux> callback) => _onPause += callback;
        public void AddCompleteCallback(Action<AbstractFlux> callback) => _onComplete += callback;
        public void AddKillCallback(Action<AbstractFlux> callback) => _onKill += callback;


        protected AbstractFlux()
        {
            _state.StateChanged += OnStateChanged;
            Reset();
            FluxEngine.Instance.Attach(this);
        }

        internal void Reset()
        {
            Id = string.Empty;
            Delay = 0f;
            InfiniteLoop = false;
            LoopCount = 1;
            OwnerSequence = null;
            LastPlayTime = null;

            _onPlay = null;
            _onPause = null;
            _onComplete = null;
            _onKill = null;

            _pause = false;
            _playElapsedTime = 0f;
            _state.StartState(FluxState.Idle);

            OnReset();
        }

        internal void Start()
        {
            _playElapsedTime = 0f;
            OnStart();

            if (Delay > 0)
            {
                _state.ChangeState(FluxState.DelayAfterPlay);
            }
            else
            {
                _state.ChangeState(FluxState.Playing);
            }

            if (IsInLoop)
            {
                OnLoop();
            }
        }

        protected virtual void OnLoop() { }

        protected virtual void OnStateChanged(FluxState previousState, FluxState newState)
        {
            switch (newState)
            {
                case FluxState.Idle:
                    break;
                case FluxState.DelayAfterPlay:
                    break;
                case FluxState.Playing:
                    _onPlay?.Invoke(this);
                    break;
                case FluxState.Paused:
                    _onPause?.Invoke(this);
                    break;
                case FluxState.Completed:
                    _onComplete?.Invoke(this);
                    break;
                case FluxState.Killed:
                    _onKill?.Invoke(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        internal void Update()
        {
            var stateKey = _state.CurrentStateKey;
            Assert.IsTrue(stateKey != FluxState.Idle);

            if (stateKey == FluxState.Completed)
            {
                return;
            }

            if (stateKey != FluxState.Paused)
            {
                _playElapsedTime += Time.deltaTime;
            }

            if (stateKey == FluxState.Paused)
            {
                if (!_pause)
                {
                    if (_playElapsedTime < Delay)
                    {
                        _state.ChangeState(FluxState.DelayAfterPlay);
                    }
                    else
                    {
                        _state.ChangeState(FluxState.Playing);
                    }
                }
            }

            if (stateKey == FluxState.DelayAfterPlay || stateKey == FluxState.Playing)
            {
                if (_pause)
                {
                    _state.ChangeState(FluxState.Paused);
                }

                if (stateKey == FluxState.DelayAfterPlay)
                {
                    if (_playElapsedTime > Delay)
                    {
                        _state.ChangeState(FluxState.Playing);
                    }
                }
                else
                {
                    var time = _playElapsedTime - Delay;
                    if (ActualDuration.HasValue && time >= ActualDuration.Value)
                    {
                        // 减小运动误差
                        if (!time.IsApproximatelyOf(ActualDuration.Value))
                        {
                            OnPlaying(ActualDuration.Value);
                        }

                        Complete();
                    }
                    else
                    {
                        OnPlaying(time);
                    }
                }
            }
        }

        internal void Kill()
        {
            OnKill();

            if (Id.IsNotNullOrEmpty())
            {
                FluxEngine.Instance.UnregisterFluxById(Id);
            }
            _state.ChangeState(FluxState.Killed);
        }

        internal void Complete()
        {
            LastPlayTime = Mathf.Max(_playElapsedTime - Delay, 0f);
            _state.ChangeState(FluxState.Completed);

            if (InfiniteLoop || LoopCount >= 2)
            {
                if (!InfiniteLoop)
                    LoopCount--;

                IsInLoop = true;
                _state.ChangeState(FluxState.Idle);
            }
            else
            {
                PendingKillSelf = true;
            }
        }

        internal void Pause()
        {
            _pause = true;
        }

        internal void Resume()
        {
            _pause = false;
        }

        protected virtual void OnReset()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnKill()
        {
        }

        protected abstract void OnPlaying(float time);

        public override string ToString()
        {
            if (Id.IsNotNullOrEmpty())
            {
                return Id;
            }

            return "<No ID>";
        }
    }
}
