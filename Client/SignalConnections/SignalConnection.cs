using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.SignalConnections
{
    public class SignalConnection(NavigationManager NavManager)
    {
        public event Action? ConnectionStateChanged;
        public string ConnectionSate = string.Empty;
        public readonly HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl(NavManager.ToAbsoluteUri("https://localhost:7082/connect"))
            .Build();
        public async Task StartConnection()
        {
            if (hubConnection.State != HubConnectionState.Connected)
            {
                await hubConnection.StartAsync();
            }
            GetConnectionState();
        }

        public async Task CloseConnection()
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.StopAsync();
            }
            GetConnectionState();
        }

        public void GetConnectionState()


    }
}
