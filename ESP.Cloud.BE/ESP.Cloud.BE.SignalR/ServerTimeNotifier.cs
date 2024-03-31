using ESP.Cloud.BE.Core.DL;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
namespace ESP.Cloud.BE.SignalR
{
    public class ServerTimeNotifier : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly IHubContext<NotificationsHub, INotificationClient> _context;
        private readonly Func<IBookingDL> _bookingDLFactory;
        public ServerTimeNotifier(IHubContext<NotificationsHub, INotificationClient> context, Func<IBookingDL> bookingDL)
        {
            _context = context;
            _bookingDLFactory = bookingDL;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);
            var bookingDL = _bookingDLFactory();

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var result = await bookingDL.CheckBooking();

                var jsonData = JsonSerializer.Serialize(result);
                await _context.Clients.All.ReceiveNotification($"Data: {DateTime.Now}");
            }
        }
    }
}
