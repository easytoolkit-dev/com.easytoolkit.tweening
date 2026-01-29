using System;

namespace EasyToolkit.Fluxion
{
    public interface IFluxEvaluator
    {
        bool CanProcess(Type valueType);

        Type ValueType { get; }
        Type ProfileType { get; }
        FluxEvaluatorContext Context { get; }

        object GetRelativeValueUntyped(object value, object relative);
        float GetDistance();
        void Initialize();
        object ProcessUntyped(float normalizedTime);
    }

    public interface IFluxEvaluator<TValue, TProfile> : IFluxEvaluator
        where TProfile : IFluxProfile
    {
        new FluxEvaluatorContext<TValue, TProfile> Context { get; }
        TValue GetRelativeValue(TValue value, TValue relative);
        TValue Process(float normalizedTime);
    }
}
