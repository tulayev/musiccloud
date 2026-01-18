using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var httpContext = Context.GetHttpContext();
            var trackId = httpContext.Request.Query["trackId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, trackId);
        }
    }
}
