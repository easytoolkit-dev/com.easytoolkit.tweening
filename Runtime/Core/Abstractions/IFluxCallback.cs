using System;

namespace EasyToolkit.Fluxion.Core
{
    public interface IFluxCallback : IFlux
    {
        event Action Callback;
    }
}
