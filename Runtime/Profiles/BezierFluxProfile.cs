using UnityEngine;

namespace EasyToolkit.Fluxion.Profiles
{
    public class BezierFluxProfile : IFluxProfile
    {
        public Vector3 ControlPoint { get; }
        public BezierControlPointRelativeTo ControlPointRelativeTo { get; }

        public BezierFluxProfile(Vector3 controlPoint, BezierControlPointRelativeTo controlPointRelativeTo)
        {
            ControlPoint = controlPoint;
            ControlPointRelativeTo = controlPointRelativeTo;
        }
    }
}
