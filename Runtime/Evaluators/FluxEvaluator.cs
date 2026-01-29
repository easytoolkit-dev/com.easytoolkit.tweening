using System;

namespace EasyToolkit.Fluxion
{
    internal class FluxEvaluatorContext
    {
        public IFluxProfile Effect { get; set; }
        public object StartValue { get; set; }
        public object EndValue { get; set; }
    }

    public class FluxEvaluatorContext<TValue, TEffect>
    {
        public TEffect Effect { get; set; }
        public TValue StartValue { get; set; }
        public TValue EndValue { get; set; }
    }

    internal interface IFluxEvaluator
    {
        bool CanProcess(Type valueType);

        Type ValueType { get; }
        Type EffectType { get; }
        FluxEvaluatorContext Context { get; }

        object GetRelativeValue(object value, object relative);
        float GetDistance();
        void Initialize();
        object Process(float normalizedTime);
    }

    public abstract class AbstractFluxEvaluator<TValue, TEffect> : IFluxEvaluator
        where TEffect : IFluxProfile
    {
        private readonly FluxEvaluatorContext _context = new FluxEvaluatorContext();

        bool IFluxEvaluator.CanProcess(Type valueType)
        {
            return CanProcess(valueType);
        }

        Type IFluxEvaluator.ValueType => typeof(TValue);
        Type IFluxEvaluator.EffectType => typeof(TEffect);

        FluxEvaluatorContext IFluxEvaluator.Context => _context;

        object IFluxEvaluator.GetRelativeValue(object value, object relative)
        {
            return GetRelativeValue((TValue)value, (TValue)relative);
        }

        float IFluxEvaluator.GetDistance()
        {
            return GetDistance();
        }

        void IFluxEvaluator.Initialize()
        {
            Context.Effect = (TEffect)_context.Effect;
            Context.StartValue = (TValue)_context.StartValue;
            Context.EndValue = (TValue)_context.EndValue;
            OnInit();
        }

        object IFluxEvaluator.Process(float normalizedTime)
        {
            return OnProcess(normalizedTime);
        }

        protected virtual bool CanProcess(Type valueType) => true;
        protected abstract TValue GetRelativeValue(TValue value, TValue relative);

        protected FluxEvaluatorContext<TValue, TEffect> Context { get; } = new FluxEvaluatorContext<TValue, TEffect>();
        protected abstract float GetDistance();
        protected virtual void OnInit() { }
        protected abstract TValue OnProcess(float normalizedTime);
    }
}
