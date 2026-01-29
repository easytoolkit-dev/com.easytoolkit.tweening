using System;
using System.Collections.Generic;
using System.Linq;
using EasyToolkit.Core.Collections;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Reflection;

namespace EasyToolkit.Fluxion.Evaluators
{
    public class FluxEvaluatorFactory : Singleton<FluxEvaluatorFactory>
    {
        private readonly ITypeMatcher _fluxEvaluatorTypeMatcher = TypeMatcherFactory.CreateDefault();

        FluxEvaluatorFactory()
        {
        }

        protected override void OnSingletonInitialize()
        {
            var types = AssemblyUtility.GetTypes(AssemblyCategory.Custom)
                .Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract &&
                            t.IsDerivedFrom<IFluxEvaluator>())
                .ToArray();

            _fluxEvaluatorTypeMatcher.SetTypeMatchCandidates(
                types.Select((type, i) => new TypeMatchCandidate(type, types.Length - i,
                    type.GetGenericArgumentsRelativeTo(typeof(IFluxEvaluator<,>)))));
        }

        private readonly Dictionary<(Type, Type), Type> _fluxEvaluatorTypesByValueType = new();

        private Type GetFluxEvaluatorType(Type valueType, Type effectConfigType)
        {
            var key = (valueType, effectConfigType);
            if (_fluxEvaluatorTypesByValueType.TryGetValue(key, out var evaluatorType))
            {
                return evaluatorType;
            }

            var results = _fluxEvaluatorTypeMatcher.GetMatches(valueType, effectConfigType);
            if (results.IsNotNullOrEmpty())
            {
                evaluatorType = results[0].MatchedType;
            }

            _fluxEvaluatorTypesByValueType[key] = evaluatorType;
            return evaluatorType;
        }

        public IFluxEvaluator GetFluxEvaluator(Type valueType, Type effectConfigType)
        {
            var processor = GetFluxEvaluatorType(valueType, effectConfigType)?.CreateInstance<IFluxEvaluator>();
            if (processor == null || !processor.CanProcess(valueType))
                return null;
            return processor;
        }
    }
}
