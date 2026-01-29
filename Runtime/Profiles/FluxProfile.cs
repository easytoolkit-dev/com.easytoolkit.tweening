using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public interface IFluxProfile
    {
    }

    public class LinearFluxProfile : IFluxProfile
    {
    }

    public enum BezierControlPointRelativeTo
    {
        None,
        StartPoint,
        EndPoint
    }

    public class BezierFluxProfile : IFluxProfile
    {
        public Vector3 ControlPoint;
        public BezierControlPointRelativeTo ControlPointRelativeTo;

        public BezierFluxProfile SetControlPoint(Vector3 point)
        {
            ControlPoint = point;
            return this;
        }

        public BezierFluxProfile SetControlPoint(float x, float y, float z = 0)
        {
            ControlPoint = new Vector3(x, y, z);
            return this;
        }

        public BezierFluxProfile SetControlPointRelative(BezierControlPointRelativeTo relativeTo)
        {
            ControlPointRelativeTo = relativeTo;
            return this;
        }
    }

    public static class Effect
    {
        public static LinearFluxProfile Linear()
        {
            return new LinearFluxProfile();
        }

        public static BezierFluxProfile Bezier()
        {
            return new BezierFluxProfile();
        }
    }
}
