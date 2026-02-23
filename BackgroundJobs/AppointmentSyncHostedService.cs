using ClinicManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClinicManagementSystem.BackgroundJobs
{
    public class AppointmentSyncHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AppointmentSyncHostedService> _logger;

        // chỉnh chu kỳ ở đây
        private static readonly TimeSpan Interval = TimeSpan.FromMinutes(10);

        public AppointmentSyncHostedService(
            IServiceScopeFactory scopeFactory,
            ILogger<AppointmentSyncHostedService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // chạy ngay khi app start (catch-up)
            await RunOnceSafe(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(Interval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                await RunOnceSafe(stoppingToken);
            }
        }

        private async Task RunOnceSafe(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();

                await appointmentService.SyncExpiredAppointments();

                _logger.LogInformation("Appointment sync expired done at {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                // không crash app
                _logger.LogError(ex, "Appointment sync expired failed at {Time}", DateTime.Now);
            }
        }

    }
}