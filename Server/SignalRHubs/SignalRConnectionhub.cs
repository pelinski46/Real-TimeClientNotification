using Microsoft.AspNetCore.SignalR;
namespace Server.SignalRHubs
{
    public class SignalRConnectionHub : Hub
    {
        public override async Task OnConnectedAsync()
        => await Clients
                 .All
                 .SendAsync("AllClientsNotification", $"{Context.ConnectionId} has joined {Environment.NewLine} say 'Hi' to him");

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients
                  .All
                  .SendAsync("AllClientsNotification", $"{Context.ConnectionId} just left");
        }

    }
}
