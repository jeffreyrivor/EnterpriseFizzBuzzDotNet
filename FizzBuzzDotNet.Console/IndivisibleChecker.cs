using System.Linq;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class IndivisibleChecker : IConditionChecker
    {
        private readonly int[] _divisors;

        public IndivisibleChecker(params int[] divisors)
        {
            _divisors = divisors;
        }

        public bool CheckCondition(int value)
        {
            return _divisors.All(divisor => value % divisor != 0);
        }
    }
}