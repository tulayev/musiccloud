using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            var comment = await _mediator.Send(command);

            await Clients.Group(command.TrackId.ToString())
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var trackId = httpContext.Request.Query["trackId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, trackId);
            var result = await _mediator.Send(new List.Query{ TrackId = Guid.Parse(trackId) });
            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }
    }
}