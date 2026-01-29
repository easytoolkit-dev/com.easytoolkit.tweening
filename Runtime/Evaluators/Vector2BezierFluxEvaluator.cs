using System;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class Vector2BezierFluxEvaluator : AbstractFluxEvaluator<Vector2, BezierFluxProfile>
    {
        protected override Vector2 GetRelativeValue(Vector2 value, Vector2 relative)
        {
            return value + relative;
        }

        private Vector2 _controlPoint;

        protected override void OnInit()
        {
            _controlPoint = Context.Effect.ControlPoint.ToVector2();
            switch (Context.Effect.ControlPointRelativeTo)
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

        protected override float GetDistance()
        {
            return MathUtility.EstimateQuadraticBezierLength(Context.StartValue, _controlPoint, Context.EndValue);
        }

        protected override Vector2 OnProcess(float normalizedTime)
        {
            return MathUtility.CalculateQuadraticBezierPoint(Context.StartValue, _controlPoint, Context.EndValue,
                normalizedTime);
        }
    }
}
