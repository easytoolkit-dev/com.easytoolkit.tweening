using System;
using EasyToolkit.Core.Diagnostics;
using EasyToolkit.Core.Mathematics;

namespace EasyToolkit.Fluxion
{
    public class Flow : FluxBase, IFlowEntity
    {
        private object _startValue;
        private object _endValue;
        private Type _valueType;
        private FluxValueGetter _valueGetter;
        private FluxValueSetter _valueSetter;

        private float _duration;
        private bool _hasUnityObject;

        private UnityEngine.Object _unityObject;
        public UnityEngine.Object UnityObject
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
        public LoopType LoopType { get; set; }
        public IFlowEase Ease { get; set; }

        public bool IsSpeedBased { get; set; }
        public bool IsRelative { get; set; }

        private IFluxProfile _profile;
        private IFluxEvaluator _processor;
        private float? _actualDuration;

        public override float? Duration
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

        public void SetDuration(float duration)
        {
            _duration = duration;
            _actualDuration = null;
        }

        protected override void OnReset()
        {
            base.OnReset();
            _startValue = null;
            _endValue = null;
            _valueType = null;
            _valueGetter = null;
            _valueSetter = null;
            _duration = 0f;
            _unityObject = null;
            _hasUnityObject = false;
            IsRelative = false;
        }

        public void SetProfile(IFluxProfile profile)
        {
            if (profile == _profile)
                return;

            if (_profile == null || profile.GetType() != _profile.GetType())
            {
                _processor = FluxEvaluatorFactory.Instance.GetFluxEvaluator(_valueType, profile.GetType());
            }
            _profile = profile;
        }

        internal void Apply(Type valueType, FluxValueGetter valueGetter, FluxValueSetter valueSetter, object endValue)
        {
            _valueType = valueType;
            _valueGetter = valueGetter;
            _valueSetter = valueSetter;
            _endValue = endValue;
        }

        protected override void OnStart()
        {
            if (_hasUnityObject && _unityObject == null)
            {
                IsPendingKill = true;
                return;
            }

            if (Ease == null)
            {
                Ease = EaseFactory.Linear();
            }

            if (_profile == null)
            {
                SetProfile(ProfileFactory.Linear());
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
                _startValue = _valueGetter();
                if (IsRelative)
                {
                    _endValue = _processor.GetRelativeValueUntyped(_startValue, _endValue);
                }
            }

            _processor.Context.Profile = _profile;
            _processor.Context.StartValue = _startValue;
            _processor.Context.EndValue = _endValue;
            _processor.Initialize();
        }

        protected override void OnPlaying(float time)
        {
            if (_hasUnityObject && _unityObject == null)
            {
                IsPendingKill = true;
                return;
            }

            Assert.IsTrue(Duration.HasValue);

            var t = MathUtility.Remap(time, 0f, Duration.Value, 0f, 1f);
            var easedT = Ease.EaseTime(t);
            var curValue = _processor.ProcessUntyped(easedT);
            _valueSetter(curValue);
        }
    }
}
