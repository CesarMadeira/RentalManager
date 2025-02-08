using Microsoft.Extensions.Hosting;

namespace RentalManager.Infra.Worker
{
    public class ConsumeQueueMessagesBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Executado!");
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
