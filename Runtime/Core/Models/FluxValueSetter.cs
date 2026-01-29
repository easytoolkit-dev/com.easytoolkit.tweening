namespace EasyToolkit.Fluxion
{
    public delegate void FluxValueSetter(object val);

    public delegate void FluxValueSetter<in T>(T val);
}
