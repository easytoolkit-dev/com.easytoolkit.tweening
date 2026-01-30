using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Profiles
{
    /// <summary>
    /// A builder struct for chaining profile configuration to a Flow.
    /// Supports both IFlow and IFlow&lt;TValue&gt; through generic constraints.
    /// </summary>
    /// <typeparam name="TFlow">The type of flow (IFlow or IFlow&lt;TValue&gt;).</typeparam>
    public readonly struct ProfileBuilder<TFlow> where TFlow : class, IFlow
    {
        /// <summary>
        /// Gets the underlying Flow instance.
        /// </summary>
        public readonly TFlow Flow;

        /// <summary>
        /// Initializes a new instance of the ProfileBuilder struct.
        /// </summary>
        /// <param name="flow">The Flow to configure profile for.</param>
        public ProfileBuilder(TFlow flow)
        {
            Flow = flow;
        }
    }
}
