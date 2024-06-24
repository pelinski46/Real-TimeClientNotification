using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;


namespace Client.SignalConnections
{
    public class SignalRConnection(NavigationManager NavManager)
    {
        public event Action? ConnectionStateChanged;
        public string ConnectionState = string.Empty;
        public readonly HubConnection hubConnection = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri("https://localhost:7082/connect")).Build();
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
        {
            switch (hubConnection.State)
            {
                case HubConnectionState.Connected:
                    Invoke("Connected");
                    break;
                case HubConnectionState.Connecting:
                    Invoke("Connecting...");
                    break;
                case HubConnectionState.Reconnecting:
                    Invoke("Reconnecting...");
                    break;
                case HubConnectionState.Disconnected:
                    Invoke("Disconnected");
                    break;
                default:
                    ConnectionState = "Unknow error occurred";
                    break;

            };

        }
        void Invoke(string message)
        {
            ConnectionState = message;
            ConnectionStateChanged?.Invoke();
        }
    }
}
