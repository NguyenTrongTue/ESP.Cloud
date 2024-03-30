using Microsoft.AspNetCore.SignalR;

namespace ESP.Cloud.BE.Infrastructure
{
    public class NotificationsHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ReceiveNotification($"Cảm ơn bạn đã kết nối tới ứng dụng của chúng tôi {Context?.User?.Identity?.Name}");

            await base.OnConnectedAsync();
        }

    }

    public interface INotificationClient { Task ReceiveNotification(string message); }
}
