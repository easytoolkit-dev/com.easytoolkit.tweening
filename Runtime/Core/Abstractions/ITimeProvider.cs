namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Provides time-related values for Flux updates.
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the time in seconds it took to complete the last frame.
        /// </summary>
        float DeltaTime { get; }

        /// <summary>
        /// Gets the unscaled time in seconds it took to complete the last frame.
        /// </summary>
        float UnscaledDeltaTime { get; }
    }
}
