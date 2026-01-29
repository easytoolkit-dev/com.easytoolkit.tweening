using System;
using EasyToolkit.Fluxion.Profiles;

namespace EasyToolkit.Fluxion.Evaluators
{
    public interface IFluxEvaluator
    {
        bool CanProcess(Type valueType);

        Type ValueType { get; }
        Type ProfileType { get; }
        FluxEvaluatorContext Context { get; }

        void Initialize();
        float GetDistance();
        object GetRelativeValueUntyped(object value, object relative);
        object ProcessUntyped(float normalizedTime);
    }

    public interface IFluxEvaluator<TValue> : IFluxEvaluator
    {
        new FluxEvaluatorContext<TValue> Context { get; }

        TValue GetRelativeValue(TValue value, TValue relative);
        TValue Process(float normalizedTime);
    }

    public interface IFluxEvaluator<TValue, TProfile> : IFluxEvaluator<TValue>
        where TProfile : IFluxProfile
    {
        new FluxEvaluatorContext<TValue, TProfile> Context { get; }
    }
}
