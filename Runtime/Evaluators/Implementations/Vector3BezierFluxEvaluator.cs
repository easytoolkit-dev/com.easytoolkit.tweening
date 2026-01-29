using System;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public class Vector3BezierFluxEvaluator : FluxEvaluatorBase<Vector3, BezierFluxProfile>
    {
        public override Vector3 GetRelativeValue(Vector3 value, Vector3 relative)
        {
            return value + relative;
        }

        private Vector3 _controlPoint;

        public override void Initialize()
        {
            _controlPoint = Context.Profile.ControlPoint;
            switch (Context.Profile.ControlPointRelativeTo)
            {
                case BezierControlPointRelativeTo.None:
                    break;
                case BezierControlPointRelativeTo.StartPoint:
                    _controlPoint += Context.StartValue;
                    break;
                case BezierControlPointRelativeTo.EndPoint:
                    _controlPoint += Context.EndValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override float GetDistance()
        {
            return MathUtility.EstimateQuadraticBezierLength(Context.StartValue, _controlPoint, Context.EndValue);
        }

        public override Vector3 Process(float normalizedTime)
        {
            return MathUtility.CalculateQuadraticBezierPoint(Context.StartValue, _controlPoint, Context.EndValue,
                normalizedTime);
        }
    }
}
