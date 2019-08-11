using System.Collections.Generic;

namespace FizzBuzzDotNet.Abstractions.Interfaces
{
    public interface IValuesAggregator<TInput, TOutput>
    {
        TOutput Aggregate(IEnumerable<TInput> inputs);
    }
}
