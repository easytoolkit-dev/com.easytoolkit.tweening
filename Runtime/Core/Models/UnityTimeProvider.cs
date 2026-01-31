using UnityEngine;

namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Default implementation of <see cref="ITimeProvider"/> using Unity's time system.
    /// </summary>
    internal class UnityTimeProvider : ITimeProvider
    {
        /// <summary>
        /// Gets the time in seconds it took to complete the last frame (Read Only).
        /// </summary>
        public float DeltaTime => Time.deltaTime;

        /// <summary>
        /// Gets the unscaled time in seconds it took to complete the last frame (Read Only).
        /// </summary>
        public float UnscaledDeltaTime => Time.unscaledDeltaTime;
    }
}
