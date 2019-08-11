namespace FizzBuzzDotNet.Abstractions.Interfaces
{
    public interface IValueGenerator<TInput, TOutput>
    {
        TOutput Execute(TInput input);
    }
}
