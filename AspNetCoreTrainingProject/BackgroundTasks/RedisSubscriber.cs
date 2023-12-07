using StackExchange.Redis;

namespace AspNetCoreTrainingProject.BackgroundTasks
{
    public class RedisSubscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisSubscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            return subscriber.SubscribeAsync("messages", (_, value) =>
            {
                Console.WriteLine($"The message content was {value}");
            });
        }
    }
}
