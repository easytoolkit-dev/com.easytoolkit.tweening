using System;
using EasyToolkit.Core.Mathematics;
using UnityEngine;

namespace EasyToolkit.Fluxion
{
    public class Vector3BezierFluxEvaluator : AbstractFluxEvaluator<Vector3, BezierFluxProfile>
    {
        protected override Vector3 GetRelativeValue(Vector3 value, Vector3 relative)
        {
            return value + relative;
        }

        private Vector3 _controlPoint;

        protected override void OnInit()
        {
            _controlPoint = Context.Effect.ControlPoint;
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

        protected override Vector3 OnProcess(float normalizedTime)
        {
            return MathUtility.CalculateQuadraticBezierPoint(Context.StartValue, _controlPoint, Context.EndValue,
                normalizedTime);
        }
    }
}
