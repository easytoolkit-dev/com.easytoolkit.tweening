using System;
using EasyToolkit.Core;
using EasyToolkit.Core.Diagnostics;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public delegate object FluxGetter();
    public delegate void FluxSetter(object val);

    public delegate T FluxGetter<T>();
    public delegate void FluxSetter<T>(T val);

    public enum LoopType
    {
        /// <summary>
        /// 每次循环都从起点重新开始（从A到B，再从A到B，...）
        /// </summary>
        Restart,
        /// <summary>
        /// 每次循环都会反转方向（A到B，再B到A，再A到B...）
        /// </summary>
        Yoyo
    }

    public class Flow : AbstractFlux
    {
        private object _startValue;
        private object _endValue;
        private Type _valueType;
        private FluxGetter _getter;
        private FluxSetter _setter;

        private float _duration;
        private bool _hasUnityObject;

        private UnityEngine.Object _unityObject;
        internal UnityEngine.Object UnityObject
        {
            get => _unityObject;
            set
            {
                if (ReferenceEquals(value, null))
                {
                    _hasUnityObject = false;
                    _unityObject = null;
                }
                else
                {
                    _hasUnityObject = true;
                    _unityObject = value;
                }
            }
        }
        protected internal LoopType LoopType { get; set; }
        protected internal IFlowEase Ease { get; set; }

        protected internal bool IsSpeedBased { get; set; }
        protected internal bool IsRelative { get; set; }

        private IFluxProfile _profile;
        private IFluxEvaluator _processor;
        private float? _actualDuration;

        protected override float? ActualDuration
        {
            get
            {
                if (_actualDuration.HasValue)
                {
                    return _actualDuration.Value;
                }

                if (IsSpeedBased)
                {
                    if (CurrentState == FluxState.Idle)
                        return null;
                    _actualDuration = _processor.GetDistance() / _duration;
                }
                else
                {
                    _actualDuration = _duration;
                }

                return _actualDuration;
            }
        }

        internal void SetDuration(float duration)
        {
            _duration = duration;
            _actualDuration = null;
        }

        protected override void OnReset()
        {
            _startValue = null;
            _endValue = null;
            _valueType = null;
            _getter = null;
            _setter = null;
            _duration = 0f;
            _unityObject = null;
            _hasUnityObject = false;
            IsRelative = false;
        }

        internal void SetProfileWithUpdateProcessor(IFluxProfile profile)
        {
            if (profile == _profile)
                return;

            if (_profile == null || profile.GetType() != _profile.GetType())
            {
                _processor = FluxEvaluatorFactory.Instance.GetFluxEvaluator(_valueType, profile.GetType());
            }
            _profile = profile;
        }

        internal void Apply(Type valueType, FluxGetter getter, FluxSetter setter, object endValue)
        {
            _valueType = valueType;
            _getter = getter;
            _setter = setter;
            _endValue = endValue;
        }

        protected override void OnStart()
        {
            if (_hasUnityObject && _unityObject == null)
            {
                PendingKillSelf = true;
                return;
            }

            if (Ease == null)
            {
                Ease = Fluxion.Ease.Linear();
            }

            if (_profile == null)
            {
                SetProfileWithUpdateProcessor(Effect.Linear());
            }

            if (IsInLoop)
            {
                switch (LoopType)
                {
                    case LoopType.Restart:
                        break;
                    case LoopType.Yoyo:
                        (_startValue, _endValue) = (_endValue, _startValue);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                _startValue = _getter();
                if (IsRelative)
                {
                    _endValue = _processor.GetRelativeValue(_startValue, _endValue);
                }
            }

            _processor.Context.Effect = _profile;
            _processor.Context.StartValue = _startValue;
            _processor.Context.EndValue = _endValue;
            _processor.Initialize();
        }

        protected override void OnPlaying(float time)
        {
            if (_hasUnityObject && _unityObject == null)
            {
                PendingKillSelf = true;
                return;
            }

            var duration = ActualDuration;
            Assert.IsTrue(duration.HasValue);

            var t = MathUtility.Remap(time, 0f, duration.Value, 0f, 1f);
            var easedT = Ease.EaseTime(t);
            var curValue = _processor.Process(easedT);
            _setter(curValue);
        }
    }
}
