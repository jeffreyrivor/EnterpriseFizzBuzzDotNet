namespace FizzBuzzDotNet.Abstractions.Interfaces
{
    public interface ILogger<T>
    {
        void Log(T value);
    }
}