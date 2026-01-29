namespace EasyToolkit.Fluxion
{
    public class FluxEvaluatorContext
    {
        public IFluxProfile Profile { get; set; }
        public object StartValue { get; set; }
        public object EndValue { get; set; }
    }

    public class FluxEvaluatorContext<TValue, TProfile> : FluxEvaluatorContext
        where TProfile : IFluxProfile
    {
        public new TProfile Profile { get; set; }
        public new TValue StartValue { get; set; }
        public new TValue EndValue { get; set; }
    }
}
