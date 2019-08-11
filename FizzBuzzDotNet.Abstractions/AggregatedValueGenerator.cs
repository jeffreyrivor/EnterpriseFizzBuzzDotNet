using System;
using System.Collections.Generic;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet.Abstractions
{
    public class AggregatedValueGenerator<TInput, TGeneratorElement, TOutput>
        : IValueGenerator<TInput, TOutput>
    {
        private IValueGenerator<TInput, IEnumerable<TGeneratorElement>> ValueGenerator { get; }

        private IValuesAggregator<TGeneratorElement, TOutput> ValuesAggregator { get; }

        public AggregatedValueGenerator(
            IValueGenerator<TInput, IEnumerable<TGeneratorElement>> valueGenerator,
            IValuesAggregator<TGeneratorElement, TOutput> valuesAggregator)
        {
            ValueGenerator = valueGenerator ??
                throw new ArgumentNullException(nameof(valueGenerator));

            ValuesAggregator = valuesAggregator ??
                throw new ArgumentNullException(nameof(valuesAggregator));
        }

        public virtual TOutput Execute(TInput input) => ValuesAggregator.Aggregate(ValueGenerator.Execute(input));
    }
}
