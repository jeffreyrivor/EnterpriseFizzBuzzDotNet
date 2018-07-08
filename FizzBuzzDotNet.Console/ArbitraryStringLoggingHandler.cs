using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class ArbitraryStringLoggingHandler : IIterationHandler
    {
        private readonly string _str;
        private readonly ILogger<string> _logger;

        public ArbitraryStringLoggingHandler(string str, ILogger<string> logger)
        {
            _str = str;
            _logger = logger;
        }

        public void Handle(int value)
        {
            _logger.Log(_str);
        }
    }
}