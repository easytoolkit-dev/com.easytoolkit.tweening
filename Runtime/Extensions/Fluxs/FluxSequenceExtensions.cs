using System;
using EasyToolkit.Fluxion.Core;

namespace EasyToolkit.Fluxion.Extensions
{
    public static class FluxSequenceExtensions
    {
        public static IFluxSequence Append(this IFluxSequence sequence, IFlux flux)
        {
            sequence.AddFluxAsNewClip(flux);
            return sequence;
        }

        public static IFluxSequence Join(this IFluxSequence sequence, IFlux flux)
        {
            sequence.AddFluxToLastClip(flux);
            return sequence;
        }

        /// <summary>
        /// Appends a callback action to the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to append the callback to.</param>
        /// <param name="callback">The action to execute when the callback is triggered.</param>
        /// <returns>The sequence for fluent chaining.</returns>
        public static IFluxSequence AppendCallback(this IFluxSequence sequence, Action callback)
        {
            var fluxCallback = FluxFactory.Callback(callback);
            sequence.AddFluxAsNewClip(fluxCallback);
            return sequence;
        }

        /// <summary>
        /// Appends an interval delay to the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to append the interval to.</param>
        /// <param name="duration">The duration of the interval in seconds.</param>
        /// <returns>The sequence for fluent chaining.</returns>
        public static IFluxSequence AppendInterval(this IFluxSequence sequence, float duration)
        {
            var fluxInterval = FluxFactory.Interval(duration);
            sequence.AddFluxAsNewClip(fluxInterval);
            return sequence;
        }

        /// <summary>
        /// Joins a callback action to the last clip in the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to join the callback to.</param>
        /// <param name="callback">The action to execute when the callback is triggered.</param>
        /// <returns>The sequence for fluent chaining.</returns>
        public static IFluxSequence JoinCallback(this IFluxSequence sequence, Action callback)
        {
            var fluxCallback = FluxFactory.Callback(callback);
            sequence.AddFluxToLastClip(fluxCallback);
            return sequence;
        }

        /// <summary>
        /// Joins an interval delay to the last clip in the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to join the interval to.</param>
        /// <param name="duration">The duration of the interval in seconds.</param>
        /// <returns>The sequence for fluent chaining.</returns>
        public static IFluxSequence JoinInterval(this IFluxSequence sequence, float duration)
        {
            var fluxInterval = FluxFactory.Interval(duration);
            sequence.AddFluxToLastClip(fluxInterval);
            return sequence;
        }
    }
}
