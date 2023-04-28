using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzzDotNet.Abstractions
{
    public class MapReduce<TInput, TMap, TOutput>
    {
        protected IEnumerable<(Func<TInput, bool> Predicate, TMap Value)> PureMapFunctions { get; }

        protected Func<IEnumerable<TMap>, TOutput> PureMapValueReducerFunction { get; }

        protected Func<TInput, TOutput> UnmappedDefaultOutputFunction { get; }

        public MapReduce(
            Func<TInput, TOutput> unmappedDefaultOutputFunction,
            Func<IEnumerable<TMap>, TOutput> pureMapValueReducerFunction,
            IEnumerable<(Func<TInput, bool> Predicate, TMap Value)> pureMapFunctions) :
            this(unmappedDefaultOutputFunction, pureMapValueReducerFunction, pureMapFunctions.ToArray())
        { }

        public MapReduce(
            Func<TInput, TOutput> unmappedDefaultOutputFunction,
            Func<IEnumerable<TMap>, TOutput> pureMapValueReducerFunction,
            params (Func<TInput, bool> Predicate, TMap Value)[] pureMapFunctions)
        {
            if (pureMapFunctions != default)
            {
                if (pureMapValueReducerFunction == default)
                {
                    throw new ArgumentNullException(nameof(pureMapValueReducerFunction));
                }
            }
            else
            {
                if (unmappedDefaultOutputFunction == default)
                {
                    throw new ArgumentNullException(nameof(unmappedDefaultOutputFunction));
                }
            }

            PureMapFunctions = pureMapFunctions;
            PureMapValueReducerFunction = pureMapValueReducerFunction;
            UnmappedDefaultOutputFunction = unmappedDefaultOutputFunction ?? (_ => default);
        }

        public virtual TOutput Execute(TInput input)
        {
            var pureMapValues = PureMapFunctions.Where(x => x.Predicate(input)).Select(x => x.Value).ToList();

            return pureMapValues.Count > 0
                ? PureMapValueReducerFunction(pureMapValues)
                : UnmappedDefaultOutputFunction(input);
        }
    }
}
