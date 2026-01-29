using System;
using EasyToolkit.Fluxion.Profiles;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public abstract class FluxEvaluatorBase<TValue, TProfile> : IFluxEvaluator<TValue, TProfile>
        where TProfile : IFluxProfile
    {
        public FluxEvaluatorContext<TValue, TProfile> Context { get; } = new();

        public virtual void Initialize()
        {
        }

        public virtual bool CanProcess(Type valueType) => true;

        public abstract float GetDistance();

        public abstract TValue GetRelativeValue(TValue value, TValue relative);
        public abstract TValue Process(float normalizedTime);

        Type IFluxEvaluator.ValueType => typeof(TValue);
        Type IFluxEvaluator.ProfileType => typeof(TProfile);
        FluxEvaluatorContext IFluxEvaluator.Context => Context;

        object IFluxEvaluator.GetRelativeValueUntyped(object value, object relative)
        {
            return GetRelativeValue((TValue)value, (TValue)relative);
        }

        object IFluxEvaluator.ProcessUntyped(float normalizedTime)
        {
            return Process(normalizedTime);
        }
    }
}
