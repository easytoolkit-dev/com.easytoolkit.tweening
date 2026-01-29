using System;
using EasyToolkit.Core.Diagnostics;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Textual;
using UnityEngine;

namespace EasyToolkit.Fluxion.Core.Implementations
{
    internal abstract class FluxBase : IFluxEntity
    {
        private string _id;
        private bool _pendingKillSelf;
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// 获取实际的持续时间，返回null代表无法判断具体持续时间。
        /// </summary>
        /// <returns></returns>
        public virtual float? Duration => null;

        internal float? LastPlayTime { get; private set; }

        public float Delay { get; set; }

        public bool InfiniteLoop { get; set; }
        public int LoopCount { get; set; }

        public FluxState CurrentState => _state.CurrentStateKey;

        public IFlux OwnerSequence { get; set; }
        public event Action<IFlux> Played;
        public event Action<IFlux> Paused;
        public event Action<IFlux> Completed;
        public event Action<IFlux> Killed;

        public bool IsPendingKill
        {
            get
            {
                if (_pendingKillSelf)
                {
                    return true;
                }

                if (OwnerSequence != null)
                {
                    return OwnerSequence.IsPendingKill;
                }
                return false;
            }
            set => _pendingKillSelf = value;
        }

        protected internal bool IsInLoop { get; set; }

        private readonly StateMachine<FluxState> _state = new StateMachine<FluxState>();
        private bool _pause;
        private float _playElapsedTime;

        public void Kill()
        {
            _pendingKillSelf = true;
        }


        protected FluxBase()
        {
            _state.StateChanged += OnStateChanged;
            Reset();
        }

        public void Reset()
        {
            Id = string.Empty;
            Delay = 0f;
            InfiniteLoop = false;
            LoopCount = 1;
            OwnerSequence = null;
            LastPlayTime = null;

            Played = null;
            Paused = null;
            Completed = null;
            Killed = null;

            _pause = false;
            _playElapsedTime = 0f;
            _state.StartState(FluxState.Idle);

            OnReset();
        }

        public void Start()
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
                    Played?.Invoke(this);
                    break;
                case FluxState.Paused:
                    Paused?.Invoke(this);
                    break;
                case FluxState.Completed:
                    Completed?.Invoke(this);
                    break;
                case FluxState.Killed:
                    Killed?.Invoke(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        public void Update()
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
                    if (Duration.HasValue && time >= Duration.Value)
                    {
                        // 减小运动误差
                        if (!time.IsApproximatelyOf(Duration.Value))
                        {
                            OnPlaying(Duration.Value);
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

        public void HandleKill()
        {
            OnKill();
            // 注意：FluxEngine 的注销逻辑已经移出，不再在这里调用
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
                IsPendingKill = true;
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
