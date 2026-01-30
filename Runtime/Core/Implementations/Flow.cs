using System;
using EasyToolkit.Core.Diagnostics;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Eases;
using EasyToolkit.Fluxion.Evaluators;
using EasyToolkit.Fluxion.Profiles;

namespace EasyToolkit.Fluxion.Core.Implementations
{
    internal class Flow<TValue> : FluxBase, IFlowEntity<TValue>
    {
        private TValue _startValue;
        private TValue _endValue;
        private FluxValueGetter<TValue> _valueGetter;
        private FluxValueSetter<TValue> _valueSetter;

        private float _duration;

        private UnityEngine.Object _unityObject;
        public Type ValueType => typeof(TValue);

        public UnityEngine.Object UnityObject
        {
            get => _unityObject;
            set => _unityObject = value;
        }

        public LoopType LoopType { get; set; }
        public IFlowEase Ease { get; set; }

        EaseBuilder<IFlow> IFlow.WithEase => new EaseBuilder<IFlow>(this);
        public EaseBuilder<IFlow<TValue>> WithEase => new EaseBuilder<IFlow<TValue>>(this);

        public bool IsSpeedBased { get; set; }
        public bool IsRelative { get; set; }

        private IFluxProfile _profile;
        private IFluxEvaluator<TValue> _evaluator;
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
                    _actualDuration = _evaluator.GetDistance() / _duration;
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
            _startValue = default;
            _endValue = default;
            _valueGetter = null;
            _valueSetter = null;
            _duration = 0f;
            _unityObject = null;
            IsRelative = false;
        }

        public void SetProfile(IFluxProfile profile)
        {
            if (profile == _profile)
                return;

            if (_profile == null || profile.GetType() != _profile.GetType())
            {
                _evaluator =
                    (IFluxEvaluator<TValue>)FluxEvaluatorFactory.Instance.GetFluxEvaluator(ValueType,
                        profile.GetType());
            }

            _profile = profile;
        }

        internal void Apply(FluxValueGetter<TValue> valueGetter, FluxValueSetter<TValue> valueSetter, TValue endValue)
        {
            _valueGetter = valueGetter;
            _valueSetter = valueSetter;
            _endValue = endValue;
        }

        protected override void OnStart()
        {
            if (_unityObject == null)
            {
                IsPendingKill = true;
                return;
            }

            if (Ease == null)
            {
                Ease = EaseFactory.Generic(t => t);
            }

            if (_profile == null)
            {
                SetProfile(new LinearFluxProfile());
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
                    _endValue = _evaluator.GetRelativeValue(_startValue, _endValue);
                }
            }

            _evaluator.Context.Profile = _profile;
            _evaluator.Context.StartValue = _startValue;
            _evaluator.Context.EndValue = _endValue;
            _evaluator.Initialize();
        }

        protected override void OnPlaying(float time)
        {
            if (_unityObject == null)
            {
                IsPendingKill = true;
                return;
            }

            Assert.IsTrue(Duration.HasValue);

            var t = MathUtility.Remap(time, 0f, Duration.Value, 0f, 1f);
            var easedT = Ease.EaseTime(t);
            var curValue = _evaluator.Process(easedT);
            _valueSetter(curValue);
        }
    }
}
