using System;
using System.Collections.Generic;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet.Abstractions
{
    public sealed class ValuesAggregatorDelegate<TInput, TOutput>
        : IValuesAggregator<TInput, TOutput>
    {
        private Func<IEnumerable<TInput>, TOutput> Aggregator { get; }

        public ValuesAggregatorDelegate(
            Func<IEnumerable<TInput>, TOutput> aggregator)
        {
            Aggregator = aggregator ??
                throw new ArgumentNullException(nameof(aggregator));
        }

        public TOutput Aggregate(IEnumerable<TInput> inputs) => Aggregator(inputs);
    }
}
