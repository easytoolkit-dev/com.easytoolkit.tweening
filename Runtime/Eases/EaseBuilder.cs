using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Eases
{
    /// <summary>
    /// A builder struct for chaining ease configuration to a Flow.
    /// Supports both IFlow and IFlow&lt;TValue&gt; through generic constraints.
    /// </summary>
    /// <typeparam name="TFlow">The type of flow (IFlow or IFlow&lt;TValue&gt;).</typeparam>
    public readonly struct EaseBuilder<TFlow> where TFlow : class, IFlow
    {
        /// <summary>
        /// Gets the underlying Flow instance.
        /// </summary>
        internal readonly TFlow Flow;

        /// <summary>
        /// Initializes a new instance of the EaseBuilder struct.
        /// </summary>
        /// <param name="flow">The Flow to configure easing for.</param>
        public EaseBuilder(TFlow flow)
        {
            Flow = flow;
        }
    }
}
