using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class DivisibleChecker : IConditionChecker
    {
        private readonly int _divisor;

        public DivisibleChecker(int divisor)
        {
            _divisor = divisor;
        }

        public bool CheckCondition(int value)
        {
            return value % _divisor == 0;
        }
    }
}