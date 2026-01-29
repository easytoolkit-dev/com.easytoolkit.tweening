using System;
using EasyToolkit.Core.Mathematics;
using EasyToolkit.Fluxion.Profiles;
using UnityEngine;

namespace EasyToolkit.Fluxion.Evaluators.Implementations
{
    public class Vector2BezierFluxEvaluator : FluxEvaluatorBase<Vector2, BezierFluxProfile>
    {
        public override Vector2 GetRelativeValue(Vector2 value, Vector2 relative)
        {
            return value + relative;
        }

        private Vector2 _controlPoint;

        public override void Initialize()
        {
            _controlPoint = Context.Profile.ControlPoint.ToVector2();
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

        public override Vector2 Process(float normalizedTime)
        {
            return MathUtility.CalculateQuadraticBezierPoint(Context.StartValue, _controlPoint, Context.EndValue,
                normalizedTime);
        }
    }
}
