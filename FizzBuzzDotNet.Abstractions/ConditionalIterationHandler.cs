using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet.Abstractions
{
    public class ConditionalIterationHandler : IIterationHandler
    {
        private readonly IConditionChecker _conditionChecker;
        private readonly IIterationHandler _innerHandler;

        public ConditionalIterationHandler(IConditionChecker conditionChecker, IIterationHandler innerHandler)
        {
            _conditionChecker = conditionChecker;
            _innerHandler = innerHandler;
        }

        public void Handle(int value)
        {
            if (_conditionChecker.CheckCondition(value))
            {
                _innerHandler.Handle(value);
            }
        }
    }
}