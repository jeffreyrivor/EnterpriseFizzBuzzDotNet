using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class ValueLoggingHandler : IIterationHandler
    {
        private readonly ILogger<int> _logger;

        public ValueLoggingHandler(ILogger<int> logger)
        {
            _logger = logger;
        }

        public void Handle(int value)
        {
            _logger.Log(value);
        }
    }
}