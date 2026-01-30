using System;
using EasyToolkit.Fluxion.Eases;
using EasyToolkit.Fluxion.Profiles;
using Object = UnityEngine.Object;

namespace EasyToolkit.Fluxion.Core
{
    /// <summary>
    /// Interface for the public API of a Flow (Tween) object.
    /// Extends IFlux with specific tweening capabilities.
    /// </summary>
    public interface IFlow : IFlux
    {
        Type ValueType { get; }

        /// <summary>
        /// Gets or sets the Unity Object associated with this Flow.
        /// If the object is destroyed, the Flow will be automatically killed.
        /// </summary>
        Object UnityObject { get; set; }

        /// <summary>
        /// Gets or sets the loop type for the Flow (e.g., Restart, Yoyo).
        /// </summary>
        LoopType LoopType { get; set; }

        /// <summary>
        /// Gets or sets the easing function used for the Flow.
        /// </summary>
        IFlowEase Ease { get; set; }

        /// <summary>
        /// Gets the ease builder for configuring easing via chainable API.
        /// </summary>
        EaseBuilder<IFlow> WithEase { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the Flow is speed-based.
        /// In speed-based mode, duration is calculated based on speed and distance.
        /// </summary>
        bool IsSpeedBased { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Flow uses relative values.
        /// </summary>
        bool IsRelative { get; set; }

        void SetDuration(float duration);

        void SetProfile(IFluxProfile profile);
    }

    /// <summary>
    /// Generic interface for a Flow (Tween) object with typed value.
    /// </summary>
    /// <typeparam name="TValue">The type of value being tweened.</typeparam>
    public interface IFlow<TValue> : IFlow
    {
        /// <summary>
        /// Gets the ease builder for configuring easing via chainable API.
        /// </summary>
        new EaseBuilder<IFlow<TValue>> WithEase { get; }
    }
}
