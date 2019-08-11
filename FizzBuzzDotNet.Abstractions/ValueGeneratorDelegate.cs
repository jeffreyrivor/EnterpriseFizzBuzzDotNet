using System;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet.Abstractions
{
    public sealed class ValueGeneratorDelegate<TInput, TOutput>
        : IValueGenerator<TInput, TOutput>
    {
        private Func<TInput, TOutput> Generator { get; }

        public ValueGeneratorDelegate(
            Func<TInput, TOutput> generator)
        {
            Generator = generator ??
                throw new ArgumentNullException(nameof(generator));
        }

        public TOutput Execute(TInput input) => Generator(input);
    }
}
