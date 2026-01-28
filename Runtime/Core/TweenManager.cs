using EasyToolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using EasyToolkit.Core.Collections;
using EasyToolkit.Core.Patterns;
using EasyToolkit.Core.Reflection;

namespace EasyToolkit.Tweening
{
    internal class TweenManager : Singleton<TweenManager>
    {
        private readonly ITypeMatcher _tweenerProcessorTypeMatcher = TypeMatcherFactory.CreateDefault();

        TweenManager()
        {
        }

        protected override void OnSingletonInitialize()
        {
            var types = AssemblyUtility.GetTypes(AssemblyCategory.Custom)
                .Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract &&
                            t.IsDerivedFrom<ITweenerProcessor>())
                .ToArray();

            _tweenerProcessorTypeMatcher.SetTypeMatchCandidates(
                types.Select((type, i) => new TypeMatchCandidate(type, types.Length - i,
                    type.GetGenericArgumentsRelativeTo(typeof(AbstractTweenerProcessor<,>)))));
        }

        private readonly Dictionary<(Type, Type), Type> _tweenerProcessorTypesByValueType = new();

        private Type GetTweenerProcessorType(Type valueType, Type effectConfigType)
        {
            var key = (valueType, effectConfigType);
            if (_tweenerProcessorTypesByValueType.TryGetValue(key, out var tweenerType))
            {
                return tweenerType;
            }

            var results = _tweenerProcessorTypeMatcher.GetMatches(valueType, effectConfigType);
            if (results.IsNotNullOrEmpty())
            {
                tweenerType = results[0].MatchedType;
            }

            _tweenerProcessorTypesByValueType[key] = tweenerType;
            return tweenerType;
        }

        public ITweenerProcessor GetTweenerProcessor(Type valueType, Type effectConfigType)
        {
            var processor = GetTweenerProcessorType(valueType, effectConfigType)?.CreateInstance<ITweenerProcessor>();
            if (processor == null || !processor.CanProcess(valueType))
                return null;
            return processor;
        }

        public TweenSequence GetSequence()
        {
            return new TweenSequence();
        }

        public Tweener GetTweener()
        {
            return new Tweener();
        }

        public TweenCallback GetCallback()
        {
            return new TweenCallback();
        }

        public TweenInterval GetInterval()
        {
            return new TweenInterval();
        }
    }
}
