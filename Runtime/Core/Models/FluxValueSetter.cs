namespace EasyToolkit.Fluxion.Core
{
    public delegate void FluxValueSetter(object val);

    public delegate void FluxValueSetter<in T>(T val);
}
